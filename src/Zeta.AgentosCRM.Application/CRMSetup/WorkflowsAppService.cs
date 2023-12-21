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
using Microsoft.AspNetCore.Identity;
using Zeta.AgentosCRM.Authorization.Users.Dto;
using Castle.Windsor.Diagnostics.Extensions;
using System.Collections;
using Zeta.AgentosCRM.CRMPartner.Promotion.Dtos;
using Zeta.AgentosCRM.CRMPartner.Promotion;

namespace Zeta.AgentosCRM.CRMSetup
{
    [AbpAuthorize(AppPermissions.Pages_CRMSetup_Workflows)]
    public class WorkflowsAppService : AgentosCRMAppServiceBase, IWorkflowsAppService
    {
        private readonly IRepository<Workflow> _workflowRepository;
        private readonly IRepository<WorkflowStep> _workflowStepRepository;
        private readonly IRepository<WorkflowOffice> _workflowOfficeRepository;

        public WorkflowsAppService(IRepository<Workflow> workflowRepository, IRepository<WorkflowStep> workflowStepRepository, IRepository<WorkflowOffice> workflowOfficeRepository)
        {
            _workflowRepository = workflowRepository;
            _workflowStepRepository = workflowStepRepository;
            _workflowOfficeRepository = workflowOfficeRepository;
        }

        public async Task<PagedResultDto<GetWorkflowForViewDto>> GetAll(GetAllWorkflowsInput input)
        {

            var filteredWorkflows = _workflowRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var pagedAndFilteredWorkflows = filteredWorkflows
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var workflows = from o in pagedAndFilteredWorkflows
                            select new
                            {

                                o.Name,
                                Id = o.Id
                            };

            var totalCount = await filteredWorkflows.CountAsync();

            var dbList = await workflows.ToListAsync();
            var results = new List<GetWorkflowForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetWorkflowForViewDto()
                {
                    Workflow = new WorkflowDto
                    {

                        Name = o.Name,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetWorkflowForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetWorkflowForViewDto> GetWorkflowForView(int id)
        {
            var workflow = await _workflowRepository.GetAsync(id);

            var output = new GetWorkflowForViewDto { Workflow = ObjectMapper.Map<WorkflowDto>(workflow) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_Workflows_Edit)]
        public async Task<GetWorkflowForEditOutput> GetWorkflowForEdit(EntityDto input)
        {
            var workflow = await _workflowRepository.FirstOrDefaultAsync(input.Id);
            var workflowOffice = await _workflowOfficeRepository.GetAllListAsync(p => p.WorkflowId == input.Id);

            var allWorkflowSteps = await _workflowStepRepository.GetAllListAsync();

            var sortedListDescending = allWorkflowSteps.OrderBy(i => i.SrlNo).ToList();

            var output = new GetWorkflowForEditOutput 
            { 
                Workflow = ObjectMapper.Map<CreateOrEditWorkflowDto>(workflow),
                WorkflowStep = ObjectMapper.Map<List<CreateOrEditWorkflowStepDto>>(sortedListDescending),
               WorkflowOffice = ObjectMapper.Map<List<CreateOrEditWorkflowOfficeDto>>(workflowOffice)

            };
             
            return output;
        }

        public async Task CreateOrEdit(CreateOrEditWorkflowDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_Workflows_Create)]
        protected virtual async Task Create(CreateOrEditWorkflowDto input)
        { 
            var workflow = ObjectMapper.Map<Workflow>(input);
            
            if (AbpSession.TenantId != null)
            {
                workflow.TenantId = (int)AbpSession.TenantId;
            }

            var workflowId = _workflowRepository.InsertAndGetIdAsync(workflow).Result;

            foreach (var step in input.Steps)
            {
                step.WorkflowId = workflowId;
                var stepEntity = ObjectMapper.Map<WorkflowStep>(step);
                await _workflowStepRepository.InsertAsync(stepEntity);
            }

            foreach (var Officestep in input.OfficeSteps)
            {
                Officestep.WorkflowId = workflowId;
                var stepEntityoffice = ObjectMapper.Map<WorkflowOffice>(Officestep);
                await _workflowOfficeRepository.InsertAsync(stepEntityoffice);
            }
            CurrentUnitOfWork.SaveChanges();
            //await _workflowRepository.InsertAsync(workflow); 
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_Workflows_Edit)]
        protected virtual async Task Update(CreateOrEditWorkflowDto input)
        {
            var workflow = await _workflowRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, workflow);

            
            foreach (var step in input.Steps)
            {
                if (step.Id == 0)
                { 
                    var stepEntity = ObjectMapper.Map<WorkflowStep>(step);
                    await _workflowStepRepository.InsertAsync(stepEntity);
                }
                else
                {
                    var workflowStep = await _workflowStepRepository.FirstOrDefaultAsync((int)step.Id);
                    ObjectMapper.Map(step, workflowStep);
                }
                 
            }
            var WorkFlowOfficeid = await _workflowOfficeRepository.GetAllListAsync(p => p.WorkflowId == input.Id);
           
            foreach (var item in WorkFlowOfficeid)
            {
                await _workflowOfficeRepository.DeleteAsync(item.Id);
            }
            foreach (var WorkFlowOffice in input.OfficeSteps)
            {
                WorkFlowOffice.WorkflowId = workflow.Id;
                var stepEntity = ObjectMapper.Map<WorkflowOffice>(WorkFlowOffice);
                await _workflowOfficeRepository.InsertAsync(stepEntity);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_Workflows_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _workflowRepository.DeleteAsync(input.Id);
        }

    }
}