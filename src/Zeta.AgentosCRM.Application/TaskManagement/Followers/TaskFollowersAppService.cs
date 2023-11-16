using Zeta.AgentosCRM.Authorization.Users;
using Zeta.AgentosCRM.TaskManagement;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.TaskManagement.Followers.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.TaskManagement.Followers
{
    [AbpAuthorize(AppPermissions.Pages_TaskFollowers)]
    public class TaskFollowersAppService : AgentosCRMAppServiceBase, ITaskFollowersAppService
    {
        private readonly IRepository<TaskFollower, long> _taskFollowerRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<CRMTask, long> _lookup_crmTaskRepository;

        public TaskFollowersAppService(IRepository<TaskFollower, long> taskFollowerRepository, IRepository<User, long> lookup_userRepository, IRepository<CRMTask, long> lookup_crmTaskRepository)
        {
            _taskFollowerRepository = taskFollowerRepository;
            _lookup_userRepository = lookup_userRepository;
            _lookup_crmTaskRepository = lookup_crmTaskRepository;

        }

        public async Task<PagedResultDto<GetTaskFollowerForViewDto>> GetAll(GetAllTaskFollowersInput input)
        {

            var filteredTaskFollowers = _taskFollowerRepository.GetAll()
                        .Include(e => e.UserFk)
                        .Include(e => e.CRMTaskFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserFk != null && e.UserFk.Name == input.UserNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CRMTaskTitleFilter), e => e.CRMTaskFk != null && e.CRMTaskFk.Title == input.CRMTaskTitleFilter);

            var pagedAndFilteredTaskFollowers = filteredTaskFollowers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var taskFollowers = from o in pagedAndFilteredTaskFollowers
                                join o1 in _lookup_userRepository.GetAll() on o.UserId equals o1.Id into j1
                                from s1 in j1.DefaultIfEmpty()

                                join o2 in _lookup_crmTaskRepository.GetAll() on o.CRMTaskId equals o2.Id into j2
                                from s2 in j2.DefaultIfEmpty()

                                select new
                                {

                                    o.Name,
                                    Id = o.Id,
                                    UserName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                    CRMTaskTitle = s2 == null || s2.Title == null ? "" : s2.Title.ToString()
                                };

            var totalCount = await filteredTaskFollowers.CountAsync();

            var dbList = await taskFollowers.ToListAsync();
            var results = new List<GetTaskFollowerForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTaskFollowerForViewDto()
                {
                    TaskFollower = new TaskFollowerDto
                    {

                        Name = o.Name,
                        Id = o.Id,
                    },
                    UserName = o.UserName,
                    CRMTaskTitle = o.CRMTaskTitle
                };

                results.Add(res);
            }

            return new PagedResultDto<GetTaskFollowerForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetTaskFollowerForViewDto> GetTaskFollowerForView(long id)
        {
            var taskFollower = await _taskFollowerRepository.GetAsync(id);

            var output = new GetTaskFollowerForViewDto { TaskFollower = ObjectMapper.Map<TaskFollowerDto>(taskFollower) };

            if (output.TaskFollower.UserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.TaskFollower.UserId);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            if (output.TaskFollower.CRMTaskId != null)
            {
                var _lookupCRMTask = await _lookup_crmTaskRepository.FirstOrDefaultAsync((long)output.TaskFollower.CRMTaskId);
                output.CRMTaskTitle = _lookupCRMTask?.Title?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_TaskFollowers_Edit)]
        public async Task<GetTaskFollowerForEditOutput> GetTaskFollowerForEdit(EntityDto<long> input)
        {
            var taskFollower = await _taskFollowerRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTaskFollowerForEditOutput { TaskFollower = ObjectMapper.Map<CreateOrEditTaskFollowerDto>(taskFollower) };

            if (output.TaskFollower.UserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.TaskFollower.UserId);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            if (output.TaskFollower.CRMTaskId != null)
            {
                var _lookupCRMTask = await _lookup_crmTaskRepository.FirstOrDefaultAsync((long)output.TaskFollower.CRMTaskId);
                output.CRMTaskTitle = _lookupCRMTask?.Title?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditTaskFollowerDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_TaskFollowers_Create)]
        protected virtual async Task Create(CreateOrEditTaskFollowerDto input)
        {
            var taskFollower = ObjectMapper.Map<TaskFollower>(input);

            if (AbpSession.TenantId != null)
            {
                taskFollower.TenantId = (int)AbpSession.TenantId;
            }

            await _taskFollowerRepository.InsertAsync(taskFollower);

        }

        [AbpAuthorize(AppPermissions.Pages_TaskFollowers_Edit)]
        protected virtual async Task Update(CreateOrEditTaskFollowerDto input)
        {
            var taskFollower = await _taskFollowerRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, taskFollower);

        }

        [AbpAuthorize(AppPermissions.Pages_TaskFollowers_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _taskFollowerRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_TaskFollowers)]
        public async Task<List<TaskFollowerUserLookupTableDto>> GetAllUserForTableDropdown()
        {
            return await _lookup_userRepository.GetAll()
                .Select(user => new TaskFollowerUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user == null || user.Name == null ? "" : user.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_TaskFollowers)]
        public async Task<List<TaskFollowerCRMTaskLookupTableDto>> GetAllCRMTaskForTableDropdown()
        {
            return await _lookup_crmTaskRepository.GetAll()
                .Select(crmTask => new TaskFollowerCRMTaskLookupTableDto
                {
                    Id = crmTask.Id,
                    DisplayName = crmTask == null || crmTask.Title == null ? "" : crmTask.Title.ToString()
                }).ToListAsync();
        }

    }
}