using Zeta.AgentosCRM.CRMSetup.TaskCategory;
using Zeta.AgentosCRM.Authorization.Users;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMApplications;
using Zeta.AgentosCRM.CRMApplications.Stages;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.TaskManagement.Exporting;
using Zeta.AgentosCRM.TaskManagement.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.TaskManagement
{
    [AbpAuthorize(AppPermissions.Pages_CRMTasks)]
    public class CRMTasksAppService : AgentosCRMAppServiceBase, ICRMTasksAppService
    {
        private readonly IRepository<CRMTask, long> _crmTaskRepository;
        private readonly ICRMTasksExcelExporter _crmTasksExcelExporter;
        private readonly IRepository<TaskCategory, int> _lookup_taskCategoryRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<TaskPriority, int> _lookup_taskPriorityRepository;
        private readonly IRepository<Client, long> _lookup_clientRepository;
        private readonly IRepository<Partner, long> _lookup_partnerRepository;
        private readonly IRepository<Application, long> _lookup_applicationRepository;
        private readonly IRepository<ApplicationStage, long> _lookup_applicationStageRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public CRMTasksAppService(IRepository<CRMTask, long> crmTaskRepository, ICRMTasksExcelExporter crmTasksExcelExporter, IRepository<TaskCategory, int> lookup_taskCategoryRepository, IRepository<User, long> lookup_userRepository, IRepository<TaskPriority, int> lookup_taskPriorityRepository, IRepository<Client, long> lookup_clientRepository, IRepository<Partner, long> lookup_partnerRepository, IRepository<Application, long> lookup_applicationRepository, IRepository<ApplicationStage, long> lookup_applicationStageRepository, ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager)
        {
            _crmTaskRepository = crmTaskRepository;
            _crmTasksExcelExporter = crmTasksExcelExporter;
            _lookup_taskCategoryRepository = lookup_taskCategoryRepository;
            _lookup_userRepository = lookup_userRepository;
            _lookup_taskPriorityRepository = lookup_taskPriorityRepository;
            _lookup_clientRepository = lookup_clientRepository;
            _lookup_partnerRepository = lookup_partnerRepository;
            _lookup_applicationRepository = lookup_applicationRepository;
            _lookup_applicationStageRepository = lookup_applicationStageRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;

        }

        public async Task<PagedResultDto<GetCRMTaskForViewDto>> GetAll(GetAllCRMTasksInput input)
        {

            var filteredCRMTasks = _crmTaskRepository.GetAll()
                        .Include(e => e.TaskCategoryFk)
                        .Include(e => e.AssigneeFk)
                        .Include(e => e.TaskPriorityFk)
                        .Include(e => e.ClientFk)
                        .Include(e => e.PartnerFk)
                        .Include(e => e.ApplicationFk)
                        .Include(e => e.ApplicationStageFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Title.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.Contains(input.TitleFilter))
                        .WhereIf(input.MinDueDateFilter != null, e => e.DueDate >= input.MinDueDateFilter)
                        .WhereIf(input.MaxDueDateFilter != null, e => e.DueDate <= input.MaxDueDateFilter)
                        .WhereIf(input.MinDueTimeFilter != null, e => e.DueTime >= input.MinDueTimeFilter)
                        .WhereIf(input.MaxDueTimeFilter != null, e => e.DueTime <= input.MaxDueTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(input.MinRelatedToFilter != null, e => e.RelatedTo >= input.MinRelatedToFilter)
                        .WhereIf(input.MaxRelatedToFilter != null, e => e.RelatedTo <= input.MaxRelatedToFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TaskCategoryNameFilter), e => e.TaskCategoryFk != null && e.TaskCategoryFk.Name == input.TaskCategoryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.AssigneeFk != null && e.AssigneeFk.Name == input.UserNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TaskPriorityNameFilter), e => e.TaskPriorityFk != null && e.TaskPriorityFk.Name == input.TaskPriorityNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientDisplayPropertyFilter), e => string.Format("{0} {1}", e.ClientFk == null || e.ClientFk.FirstName == null ? "" : e.ClientFk.FirstName.ToString()
, e.ClientFk == null || e.ClientFk.LastName == null ? "" : e.ClientFk.LastName.ToString()
) == input.ClientDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ApplicationNameFilter), e => e.ApplicationFk != null && e.ApplicationFk.Name == input.ApplicationNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ApplicationStageNameFilter), e => e.ApplicationStageFk != null && e.ApplicationStageFk.Name == input.ApplicationStageNameFilter)
                      .WhereIf(input.PartnerIdFilter.HasValue, e => false || e.PartnerId == input.PartnerIdFilter.Value);
            var pagedAndFilteredCRMTasks = filteredCRMTasks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var crmTasks = from o in pagedAndFilteredCRMTasks
                           join o1 in _lookup_taskCategoryRepository.GetAll() on o.TaskCategoryId equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           join o2 in _lookup_userRepository.GetAll() on o.AssigneeId equals o2.Id into j2
                           from s2 in j2.DefaultIfEmpty()

                           join o3 in _lookup_taskPriorityRepository.GetAll() on o.TaskPriorityId equals o3.Id into j3
                           from s3 in j3.DefaultIfEmpty()

                           join o4 in _lookup_clientRepository.GetAll() on o.ClientId equals o4.Id into j4
                           from s4 in j4.DefaultIfEmpty()

                           join o5 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o5.Id into j5
                           from s5 in j5.DefaultIfEmpty()

                           join o6 in _lookup_applicationRepository.GetAll() on o.ApplicationId equals o6.Id into j6
                           from s6 in j6.DefaultIfEmpty()

                           join o7 in _lookup_applicationStageRepository.GetAll() on o.ApplicationStageId equals o7.Id into j7
                           from s7 in j7.DefaultIfEmpty()

                           select new
                           {

                               o.Title,
                               o.DueDate,
                               o.DueTime,
                               o.Description,
                               o.Attachment,
                               o.RelatedTo,
                               o.InternalId,
                               Id = o.Id,
                               TaskCategoryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                               UserName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                               TaskPriorityName = s3 == null || s3.Name == null ? "" : s3.Name.ToString(),
                               ClientDisplayProperty = string.Format("{0} {1}", s4 == null || s4.FirstName == null ? "" : s4.FirstName.ToString()
           , s4 == null || s4.LastName == null ? "" : s4.LastName.ToString()
           ),
                               PartnerPartnerName = s5 == null || s5.PartnerName == null ? "" : s5.PartnerName.ToString(),
                               ApplicationName = s6 == null || s6.Name == null ? "" : s6.Name.ToString(),
                               ApplicationStageName = s7 == null || s7.Name == null ? "" : s7.Name.ToString()
                           };

            var totalCount = await filteredCRMTasks.CountAsync();

            var dbList = await crmTasks.ToListAsync();
            var results = new List<GetCRMTaskForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCRMTaskForViewDto()
                {
                    CRMTask = new CRMTaskDto
                    {

                        Title = o.Title,
                        DueDate = o.DueDate,
                        DueTime = o.DueTime,
                        Description = o.Description,
                        Attachment = o.Attachment,
                        RelatedTo = o.RelatedTo,
                        InternalId = o.InternalId,
                        Id = o.Id,
                    },
                    TaskCategoryName = o.TaskCategoryName,
                    UserName = o.UserName,
                    TaskPriorityName = o.TaskPriorityName,
                    ClientDisplayProperty = o.ClientDisplayProperty,
                    PartnerPartnerName = o.PartnerPartnerName,
                    ApplicationName = o.ApplicationName,
                    ApplicationStageName = o.ApplicationStageName
                };
                res.CRMTask.AttachmentFileName = await GetBinaryFileName(o.Attachment);

                results.Add(res);
            }

            return new PagedResultDto<GetCRMTaskForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetCRMTaskForViewDto> GetCRMTaskForView(long id)
        {
            var crmTask = await _crmTaskRepository.GetAsync(id);

            var output = new GetCRMTaskForViewDto { CRMTask = ObjectMapper.Map<CRMTaskDto>(crmTask) };

            if (output.CRMTask.TaskCategoryId != null)
            {
                var _lookupTaskCategory = await _lookup_taskCategoryRepository.FirstOrDefaultAsync((int)output.CRMTask.TaskCategoryId);
                output.TaskCategoryName = _lookupTaskCategory?.Name?.ToString();
            }

            if (output.CRMTask.AssigneeId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.CRMTask.AssigneeId);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            if (output.CRMTask.TaskPriorityId != null)
            {
                var _lookupTaskPriority = await _lookup_taskPriorityRepository.FirstOrDefaultAsync((int)output.CRMTask.TaskPriorityId);
                output.TaskPriorityName = _lookupTaskPriority?.Name?.ToString();
            }

            if (output.CRMTask.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.CRMTask.ClientId);
                output.ClientDisplayProperty = string.Format("{0} {1}", _lookupClient.FirstName, _lookupClient.LastName);
            }

            if (output.CRMTask.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.CRMTask.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.CRMTask.ApplicationId != null)
            {
                var _lookupApplication = await _lookup_applicationRepository.FirstOrDefaultAsync((long)output.CRMTask.ApplicationId);
                output.ApplicationName = _lookupApplication?.Name?.ToString();
            }

            if (output.CRMTask.ApplicationStageId != null)
            {
                var _lookupApplicationStage = await _lookup_applicationStageRepository.FirstOrDefaultAsync((long)output.CRMTask.ApplicationStageId);
                output.ApplicationStageName = _lookupApplicationStage?.Name?.ToString();
            }

            output.CRMTask.AttachmentFileName = await GetBinaryFileName(crmTask.Attachment);

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CRMTasks_Edit)]
        public async Task<GetCRMTaskForEditOutput> GetCRMTaskForEdit(EntityDto<long> input)
        {
            var crmTask = await _crmTaskRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCRMTaskForEditOutput { CRMTask = ObjectMapper.Map<CreateOrEditCRMTaskDto>(crmTask) };

            if (output.CRMTask.TaskCategoryId != null)
            {
                var _lookupTaskCategory = await _lookup_taskCategoryRepository.FirstOrDefaultAsync((int)output.CRMTask.TaskCategoryId);
                output.TaskCategoryName = _lookupTaskCategory?.Name?.ToString();
            }

            if (output.CRMTask.AssigneeId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.CRMTask.AssigneeId);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            if (output.CRMTask.TaskPriorityId != null)
            {
                var _lookupTaskPriority = await _lookup_taskPriorityRepository.FirstOrDefaultAsync((int)output.CRMTask.TaskPriorityId);
                output.TaskPriorityName = _lookupTaskPriority?.Name?.ToString();
            }

            if (output.CRMTask.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.CRMTask.ClientId);
                output.ClientDisplayProperty = string.Format("{0} {1}", _lookupClient.FirstName, _lookupClient.LastName);
            }

            if (output.CRMTask.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.CRMTask.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.CRMTask.ApplicationId != null)
            {
                var _lookupApplication = await _lookup_applicationRepository.FirstOrDefaultAsync((long)output.CRMTask.ApplicationId);
                output.ApplicationName = _lookupApplication?.Name?.ToString();
            }

            if (output.CRMTask.ApplicationStageId != null)
            {
                var _lookupApplicationStage = await _lookup_applicationStageRepository.FirstOrDefaultAsync((long)output.CRMTask.ApplicationStageId);
                output.ApplicationStageName = _lookupApplicationStage?.Name?.ToString();
            }

            output.AttachmentFileName = await GetBinaryFileName(crmTask.Attachment);

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCRMTaskDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CRMTasks_Create)]
        protected virtual async Task Create(CreateOrEditCRMTaskDto input)
        {
            var crmTask = ObjectMapper.Map<CRMTask>(input);

            if (AbpSession.TenantId != null)
            {
                crmTask.TenantId = (int)AbpSession.TenantId;
            }

            await _crmTaskRepository.InsertAsync(crmTask);
            crmTask.Attachment = await GetBinaryObjectFromCache(input.AttachmentToken);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMTasks_Edit)]
        protected virtual async Task Update(CreateOrEditCRMTaskDto input)
        {
            var crmTask = await _crmTaskRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, crmTask);
            crmTask.Attachment = await GetBinaryObjectFromCache(input.AttachmentToken);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMTasks_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _crmTaskRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetCRMTasksToExcel(GetAllCRMTasksForExcelInput input)
        {

            var filteredCRMTasks = _crmTaskRepository.GetAll()
                        .Include(e => e.TaskCategoryFk)
                        .Include(e => e.AssigneeFk)
                        .Include(e => e.TaskPriorityFk)
                        .Include(e => e.ClientFk)
                        .Include(e => e.PartnerFk)
                        .Include(e => e.ApplicationFk)
                        .Include(e => e.ApplicationStageFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Title.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.Contains(input.TitleFilter))
                        .WhereIf(input.MinDueDateFilter != null, e => e.DueDate >= input.MinDueDateFilter)
                        .WhereIf(input.MaxDueDateFilter != null, e => e.DueDate <= input.MaxDueDateFilter)
                        .WhereIf(input.MinDueTimeFilter != null, e => e.DueTime >= input.MinDueTimeFilter)
                        .WhereIf(input.MaxDueTimeFilter != null, e => e.DueTime <= input.MaxDueTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(input.MinRelatedToFilter != null, e => e.RelatedTo >= input.MinRelatedToFilter)
                        .WhereIf(input.MaxRelatedToFilter != null, e => e.RelatedTo <= input.MaxRelatedToFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TaskCategoryNameFilter), e => e.TaskCategoryFk != null && e.TaskCategoryFk.Name == input.TaskCategoryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.AssigneeFk != null && e.AssigneeFk.Name == input.UserNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TaskPriorityNameFilter), e => e.TaskPriorityFk != null && e.TaskPriorityFk.Name == input.TaskPriorityNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientDisplayPropertyFilter), e => string.Format("{0} {1}", e.ClientFk == null || e.ClientFk.FirstName == null ? "" : e.ClientFk.FirstName.ToString()
, e.ClientFk == null || e.ClientFk.LastName == null ? "" : e.ClientFk.LastName.ToString()
) == input.ClientDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ApplicationNameFilter), e => e.ApplicationFk != null && e.ApplicationFk.Name == input.ApplicationNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ApplicationStageNameFilter), e => e.ApplicationStageFk != null && e.ApplicationStageFk.Name == input.ApplicationStageNameFilter);

            var query = (from o in filteredCRMTasks
                         join o1 in _lookup_taskCategoryRepository.GetAll() on o.TaskCategoryId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_userRepository.GetAll() on o.AssigneeId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _lookup_taskPriorityRepository.GetAll() on o.TaskPriorityId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         join o4 in _lookup_clientRepository.GetAll() on o.ClientId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()

                         join o5 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o5.Id into j5
                         from s5 in j5.DefaultIfEmpty()

                         join o6 in _lookup_applicationRepository.GetAll() on o.ApplicationId equals o6.Id into j6
                         from s6 in j6.DefaultIfEmpty()

                         join o7 in _lookup_applicationStageRepository.GetAll() on o.ApplicationStageId equals o7.Id into j7
                         from s7 in j7.DefaultIfEmpty()

                         select new GetCRMTaskForViewDto()
                         {
                             CRMTask = new CRMTaskDto
                             {
                                 Title = o.Title,
                                 DueDate = o.DueDate,
                                 DueTime = o.DueTime,
                                 Description = o.Description,
                                 Attachment = o.Attachment,
                                 RelatedTo = o.RelatedTo,
                                 InternalId = o.InternalId,
                                 Id = o.Id
                             },
                             TaskCategoryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                             UserName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                             TaskPriorityName = s3 == null || s3.Name == null ? "" : s3.Name.ToString(),
                             ClientDisplayProperty = string.Format("{0} {1}", s4 == null || s4.FirstName == null ? "" : s4.FirstName.ToString()
, s4 == null || s4.LastName == null ? "" : s4.LastName.ToString()
),
                             PartnerPartnerName = s5 == null || s5.PartnerName == null ? "" : s5.PartnerName.ToString(),
                             ApplicationName = s6 == null || s6.Name == null ? "" : s6.Name.ToString(),
                             ApplicationStageName = s7 == null || s7.Name == null ? "" : s7.Name.ToString()
                         });

            var crmTaskListDtos = await query.ToListAsync();

            return _crmTasksExcelExporter.ExportToFile(crmTaskListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_CRMTasks)]
        public async Task<List<CRMTaskTaskCategoryLookupTableDto>> GetAllTaskCategoryForTableDropdown()
        {
            return await _lookup_taskCategoryRepository.GetAll()
                .Select(taskCategory => new CRMTaskTaskCategoryLookupTableDto
                {
                    Id = taskCategory.Id,
                    DisplayName = taskCategory == null || taskCategory.Name == null ? "" : taskCategory.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CRMTasks)]
        public async Task<List<CRMTaskUserLookupTableDto>> GetAllUserForTableDropdown()
        {
            return await _lookup_userRepository.GetAll()
                .Select(user => new CRMTaskUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user == null || user.Name == null ? "" : user.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CRMTasks)]
        public async Task<List<CRMTaskTaskPriorityLookupTableDto>> GetAllTaskPriorityForTableDropdown()
        {
            return await _lookup_taskPriorityRepository.GetAll()
                .Select(taskPriority => new CRMTaskTaskPriorityLookupTableDto
                {
                    Id = taskPriority.Id,
                    DisplayName = taskPriority == null || taskPriority.Name == null ? "" : taskPriority.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CRMTasks)]
        public async Task<List<CRMTaskClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new CRMTaskClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = string.Format("{0} {1}", client.FirstName, client.LastName)
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CRMTasks)]
        public async Task<List<CRMTaskPartnerLookupTableDto>> GetAllPartnerForTableDropdown()
        {
            return await _lookup_partnerRepository.GetAll()
                .Select(partner => new CRMTaskPartnerLookupTableDto
                {
                    Id = partner.Id,
                    DisplayName = partner == null || partner.PartnerName == null ? "" : partner.PartnerName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CRMTasks)]
        public async Task<List<CRMTaskApplicationLookupTableDto>> GetAllApplicationForTableDropdown()
        {
            return await _lookup_applicationRepository.GetAll()
                .Select(application => new CRMTaskApplicationLookupTableDto
                {
                    Id = application.Id,
                    DisplayName = application == null || application.Name == null ? "" : application.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CRMTasks)]
        public async Task<List<CRMTaskApplicationStageLookupTableDto>> GetAllApplicationStageForTableDropdown()
        {
            return await _lookup_applicationStageRepository.GetAll()
                .Select(applicationStage => new CRMTaskApplicationStageLookupTableDto
                {
                    Id = applicationStage.Id,
                    DisplayName = applicationStage == null || applicationStage.Name == null ? "" : applicationStage.Name.ToString()
                }).ToListAsync();
        }

        private async Task<Guid?> GetBinaryObjectFromCache(string fileToken)
        {
            if (fileToken.IsNullOrWhiteSpace())
            {
                return null;
            }

            var fileCache = _tempFileCacheManager.GetFileInfo(fileToken);

            if (fileCache == null)
            {
                throw new UserFriendlyException("There is no such file with the token: " + fileToken);
            }

            var storedFile = new BinaryObject(AbpSession.TenantId, fileCache.File, fileCache.FileName);
            await _binaryObjectManager.SaveAsync(storedFile);

            return storedFile.Id;
        }

        private async Task<string> GetBinaryFileName(Guid? fileId)
        {
            if (!fileId.HasValue)
            {
                return null;
            }

            var file = await _binaryObjectManager.GetOrNullAsync(fileId.Value);
            return file?.Description;
        }

        [AbpAuthorize(AppPermissions.Pages_CRMTasks_Edit)]
        public async Task RemoveAttachmentFile(EntityDto<long> input)
        {
            var crmTask = await _crmTaskRepository.FirstOrDefaultAsync(input.Id);
            if (crmTask == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!crmTask.Attachment.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(crmTask.Attachment.Value);
            crmTask.Attachment = null;
        }

    }
}