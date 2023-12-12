using Zeta.AgentosCRM.CRMApplications;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMApplications.Stages.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMSetup;

namespace Zeta.AgentosCRM.CRMApplications.Stages
{
    [AbpAuthorize(AppPermissions.Pages_ApplicationStages)]
    public class ApplicationStagesAppService : AgentosCRMAppServiceBase, IApplicationStagesAppService
    {
        private readonly IRepository<ApplicationStage, long> _applicationStageRepository;
        private readonly IRepository<Application, long> _lookup_applicationRepository;
        private readonly IRepository<WorkflowStep, int> _lookup_workflowStepRepository;

        public ApplicationStagesAppService(IRepository<ApplicationStage, long> applicationStageRepository, IRepository<Application, long> lookup_applicationRepository, IRepository<WorkflowStep, int> lookup_workflowStepRepository)
        {
            _applicationStageRepository = applicationStageRepository;
            _lookup_applicationRepository = lookup_applicationRepository;
            _lookup_workflowStepRepository = lookup_workflowStepRepository;
        }

        public async Task<PagedResultDto<GetApplicationStageForViewDto>> GetAll(GetAllApplicationStagesInput input)
        {

            var filteredApplicationStages = _applicationStageRepository.GetAll()
                        .Include(e => e.ApplicationFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ApplicationNameFilter), e => e.ApplicationFk != null && e.ApplicationFk.Name == input.ApplicationNameFilter);

            var pagedAndFilteredApplicationStages = filteredApplicationStages
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var applicationStages = from o in pagedAndFilteredApplicationStages
                                    join o1 in _lookup_applicationRepository.GetAll() on o.ApplicationId equals o1.Id into j1
                                    from s1 in j1.DefaultIfEmpty()

                                    select new
                                    {

                                        o.Name,
                                        Id = o.Id,
                                        ApplicationName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                                    };

            var totalCount = await filteredApplicationStages.CountAsync();

            var dbList = await applicationStages.ToListAsync();
            var results = new List<GetApplicationStageForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetApplicationStageForViewDto()
                {
                    ApplicationStage = new ApplicationStageDto
                    {

                        Name = o.Name,
                        Id = o.Id,
                    },
                    ApplicationName = o.ApplicationName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetApplicationStageForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetApplicationStageForViewDto> GetApplicationStageForView(long id)
        {
            var applicationStage = await _applicationStageRepository.GetAsync(id);

            var output = new GetApplicationStageForViewDto { ApplicationStage = ObjectMapper.Map<ApplicationStageDto>(applicationStage) };

            if (output.ApplicationStage.ApplicationId != null)
            {
                var _lookupApplication = await _lookup_applicationRepository.FirstOrDefaultAsync((long)output.ApplicationStage.ApplicationId);
                output.ApplicationName = _lookupApplication?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ApplicationStages_Edit)]
        public async Task<GetApplicationStageForEditOutput> GetApplicationStageForEdit(EntityDto<long> input)
        {
            var applicationStage = await _applicationStageRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetApplicationStageForEditOutput { ApplicationStage = ObjectMapper.Map<CreateOrEditApplicationStageDto>(applicationStage) };

            if (output.ApplicationStage.ApplicationId != null)
            {
                var _lookupApplication = await _lookup_applicationRepository.FirstOrDefaultAsync((long)output.ApplicationStage.ApplicationId);
                output.ApplicationName = _lookupApplication?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditApplicationStageDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ApplicationStages_Create)]
        protected virtual async Task Create(CreateOrEditApplicationStageDto input)
        {
            var applicationStage = ObjectMapper.Map<ApplicationStage>(input);

            if (AbpSession.TenantId != null)
            {
                applicationStage.TenantId = (int)AbpSession.TenantId;
            }

            await _applicationStageRepository.InsertAsync(applicationStage);

        }

        [AbpAuthorize(AppPermissions.Pages_ApplicationStages_Edit)]
        protected virtual async Task Update(CreateOrEditApplicationStageDto input)
        {
            var applicationStage = await _applicationStageRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, applicationStage);

        }

        [AbpAuthorize(AppPermissions.Pages_ApplicationStages_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _applicationStageRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_ApplicationStages)]
        public async Task<List<ApplicationStageApplicationLookupTableDto>> GetAllApplicationForTableDropdown()
        {
            return await _lookup_applicationRepository.GetAll()
                .Select(application => new ApplicationStageApplicationLookupTableDto
                {
                    Id = application.Id,
                    DisplayName = application == null || application.Name == null ? "" : application.Name.ToString()
                }).ToListAsync();
        }
        
        [AbpAuthorize(AppPermissions.Pages_ApplicationStages)]
        public async Task<List<ApplicationStageWorkflowStepLookupTableDto>> GetAllWorkflowStepForTableDropdown()
        {
            return await _lookup_workflowStepRepository.GetAll()
                .Select(workflowStep => new ApplicationStageWorkflowStepLookupTableDto
                {
                    Id = workflowStep.Id,
                    DisplayName = workflowStep == null || workflowStep.Name == null ? "" : workflowStep.Name.ToString()
                }).ToListAsync();
        }

    }
}