using Zeta.AgentosCRM.CRMSetup;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup
{
    [AbpAuthorize(AppPermissions.Pages_CRMSetup_WorkflowSteps)]
    public class WorkflowStepsAppService : AgentosCRMAppServiceBase, IWorkflowStepsAppService
    {
        private readonly IRepository<WorkflowStep> _workflowStepRepository;
        private readonly IRepository<Workflow, int> _lookup_workflowRepository;

        public WorkflowStepsAppService(IRepository<WorkflowStep> workflowStepRepository, IRepository<Workflow, int> lookup_workflowRepository)
        {
            _workflowStepRepository = workflowStepRepository;
            _lookup_workflowRepository = lookup_workflowRepository;

        }

        public async Task<PagedResultDto<GetWorkflowStepForViewDto>> GetAll(GetAllWorkflowStepsInput input)
        {

            var filteredWorkflowSteps = _workflowStepRepository.GetAll()
                        .Include(e => e.WorkflowFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(input.MinSrlNoFilter != null, e => e.SrlNo >= input.MinSrlNoFilter)
                        .WhereIf(input.MaxSrlNoFilter != null, e => e.SrlNo <= input.MaxSrlNoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkflowNameFilter), e => e.WorkflowFk != null && e.WorkflowFk.Name == input.WorkflowNameFilter)
                        .WhereIf(input.WorkflowIdFilter.HasValue, e => false || e.WorkflowId == input.WorkflowIdFilter.Value);

            var pagedAndFilteredWorkflowSteps = filteredWorkflowSteps
                .OrderBy(input.Sorting ?? "srlNo asc")
                //.ThenBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var workflowSteps = from o in pagedAndFilteredWorkflowSteps
                                join o1 in _lookup_workflowRepository.GetAll() on o.WorkflowId equals o1.Id into j1
                                from s1 in j1.DefaultIfEmpty()

                                select new
                                { 
                                    o.SrlNo,
                                    o.Abbrivation,
                                    o.Name,
                                    Id = o.Id,
                                    o.IsPartnerClientIdRequired,
                                    o.IsActive,
                                    o.IsApplicationIntakeRequired,
                                    o.IsStartEndDateRequired,
                                    o.IsNoteRequired,
                                    o.IsWinStage,
                                    WorkflowName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                                };

            var totalCount = await filteredWorkflowSteps.CountAsync();

            var dbList = await workflowSteps.ToListAsync();
            var results = new List<GetWorkflowStepForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetWorkflowStepForViewDto()
                {
                    WorkflowStep = new WorkflowStepDto
                    {

                        SrlNo = o.SrlNo,
                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                        IsPartnerClientIdRequired = o.IsPartnerClientIdRequired,
                        IsActive=o.IsActive,
                        IsApplicationIntakeRequired = o.IsApplicationIntakeRequired,
                        IsStartEndDateRequired = o.IsStartEndDateRequired,
                        IsNoteRequired = o.IsNoteRequired,
                        IsWinStage = o.IsWinStage,
                    },
                    WorkflowName = o.WorkflowName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetWorkflowStepForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetWorkflowStepForViewDto> GetWorkflowStepForView(int id)
        {
            var workflowStep = await _workflowStepRepository.GetAsync(id);

            var output = new GetWorkflowStepForViewDto { WorkflowStep = ObjectMapper.Map<WorkflowStepDto>(workflowStep) };

            if (output.WorkflowStep.WorkflowId != null)
            {
                var _lookupWorkflow = await _lookup_workflowRepository.FirstOrDefaultAsync((int)output.WorkflowStep.WorkflowId);
                output.WorkflowName = _lookupWorkflow?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_WorkflowSteps_Edit)]
        public async Task<GetWorkflowStepForEditOutput> GetWorkflowStepForEdit(EntityDto input)
        {
            var workflowStep = await _workflowStepRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetWorkflowStepForEditOutput { WorkflowStep = ObjectMapper.Map<CreateOrEditWorkflowStepDto>(workflowStep) };

            if (output.WorkflowStep.WorkflowId != null)
            {
                var _lookupWorkflow = await _lookup_workflowRepository.FirstOrDefaultAsync((int)output.WorkflowStep.WorkflowId);
                output.WorkflowName = _lookupWorkflow?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditWorkflowStepDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_WorkflowSteps_Create)]
        protected virtual async Task Create(CreateOrEditWorkflowStepDto input)
        {
            var workflowStep = ObjectMapper.Map<WorkflowStep>(input);

            if (AbpSession.TenantId != null)
            {
                workflowStep.TenantId = (int)AbpSession.TenantId;
            }

            await _workflowStepRepository.InsertAsync(workflowStep);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_WorkflowSteps_Edit)]
        protected virtual async Task Update(CreateOrEditWorkflowStepDto input)
        {
            var workflowStep = await _workflowStepRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, workflowStep);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_WorkflowSteps_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _workflowStepRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_WorkflowSteps)]
        public async Task<PagedResultDto<WorkflowStepWorkflowLookupTableDto>> GetAllWorkflowForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_workflowRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Name != null && e.Name.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var workflowList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<WorkflowStepWorkflowLookupTableDto>();
            foreach (var workflow in workflowList)
            {
                lookupTableDtoList.Add(new WorkflowStepWorkflowLookupTableDto
                {
                    Id = workflow.Id,
                    DisplayName = workflow.Name?.ToString()
                });
            }

            return new PagedResultDto<WorkflowStepWorkflowLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}