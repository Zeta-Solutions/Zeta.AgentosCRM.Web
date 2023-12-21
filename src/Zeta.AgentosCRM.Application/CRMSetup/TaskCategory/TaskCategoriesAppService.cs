using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.TaskCategory.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup.TaskCategory
{
    [AbpAuthorize(AppPermissions.Pages_CRMSetup_TaskCategories)]
    public class TaskCategoriesAppService : AgentosCRMAppServiceBase, ITaskCategoriesAppService
    {
        private readonly IRepository<TaskCategory> _taskCategoryRepository;

        public TaskCategoriesAppService(IRepository<TaskCategory> taskCategoryRepository)
        {
            _taskCategoryRepository = taskCategoryRepository;

        }

        public async Task<PagedResultDto<GetTaskCategoryForViewDto>> GetAll(GetAllTaskCategoriesInput input)
        {

            var filteredTaskCategories = _taskCategoryRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var pagedAndFilteredTaskCategories = filteredTaskCategories
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var taskCategories = from o in pagedAndFilteredTaskCategories
                                 select new
                                 {

                                     o.Abbrivation,
                                     o.Name,
                                     Id = o.Id
                                 };

            var totalCount = await filteredTaskCategories.CountAsync();

            var dbList = await taskCategories.ToListAsync();
            var results = new List<GetTaskCategoryForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTaskCategoryForViewDto()
                {
                    TaskCategory = new TaskCategoryDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetTaskCategoryForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetTaskCategoryForViewDto> GetTaskCategoryForView(int id)
        {
            var taskCategory = await _taskCategoryRepository.GetAsync(id);

            var output = new GetTaskCategoryForViewDto { TaskCategory = ObjectMapper.Map<TaskCategoryDto>(taskCategory) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_TaskCategories_Edit)]
        public async Task<GetTaskCategoryForEditOutput> GetTaskCategoryForEdit(EntityDto input)
        {
            var taskCategory = await _taskCategoryRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTaskCategoryForEditOutput { TaskCategory = ObjectMapper.Map<CreateOrEditTaskCategoryDto>(taskCategory) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditTaskCategoryDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_TaskCategories_Create)]
        protected virtual async Task Create(CreateOrEditTaskCategoryDto input)
        {
            var taskCategory = ObjectMapper.Map<TaskCategory>(input);

            if (AbpSession.TenantId != null)
            {
                taskCategory.TenantId = (int)AbpSession.TenantId;
            }

            await _taskCategoryRepository.InsertAsync(taskCategory);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_TaskCategories_Edit)]
        protected virtual async Task Update(CreateOrEditTaskCategoryDto input)
        {
            var taskCategory = await _taskCategoryRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, taskCategory);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_TaskCategories_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _taskCategoryRepository.DeleteAsync(input.Id);
        }

    }
}