using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.Document.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMSetup.Document;

namespace Zeta.AgentosCRM.CRMSetup.Documents
{
    [AbpAuthorize(AppPermissions.Pages_WorkflowDocuments)]
    public class WorkflowDocumentsAppService : AgentosCRMAppServiceBase, IWorkflowDocumentsAppService
    {
        private readonly IRepository<WorkflowDocument> _workflowDocumentRepository;
        private readonly IRepository<Workflow, int> _lookup_workflowRepository;

        public WorkflowDocumentsAppService(IRepository<WorkflowDocument> workflowDocumentRepository, IRepository<Workflow, int> lookup_workflowRepository)
        {
            _workflowDocumentRepository = workflowDocumentRepository;
            _lookup_workflowRepository = lookup_workflowRepository;

        }

        public async Task<PagedResultDto<GetWorkflowDocumentForViewDto>> GetAll(GetAllWorkflowDocumentsInput input)
        {

            var filteredWorkflowDocuments = _workflowDocumentRepository.GetAll()
                        .Include(e => e.WorkflowFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkflowNameFilter), e => e.WorkflowFk != null && e.WorkflowFk.Name == input.WorkflowNameFilter);

            var pagedAndFilteredWorkflowDocuments = filteredWorkflowDocuments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var workflowDocuments = from o in pagedAndFilteredWorkflowDocuments
                                    join o1 in _lookup_workflowRepository.GetAll() on o.WorkflowId equals o1.Id into j1
                                    from s1 in j1.DefaultIfEmpty()

                                    select new
                                    {

                                        o.Name,
                                        o.Id,
                                        WorkflowName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                                    };

            var totalCount = await filteredWorkflowDocuments.CountAsync();

            var dbList = await workflowDocuments.ToListAsync();
            var results = new List<GetWorkflowDocumentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetWorkflowDocumentForViewDto()
                {
                    WorkflowDocument = new WorkflowDocumentDto
                    {

                        Name = o.Name,
                        Id = o.Id,
                    },
                    WorkflowName = o.WorkflowName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetWorkflowDocumentForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetWorkflowDocumentForViewDto> GetWorkflowDocumentForView(int id)
        {
            var workflowDocument = await _workflowDocumentRepository.GetAsync(id);

            var output = new GetWorkflowDocumentForViewDto { WorkflowDocument = ObjectMapper.Map<WorkflowDocumentDto>(workflowDocument) };

            if (output.WorkflowDocument.WorkflowId != null)
            {
                var _lookupWorkflow = await _lookup_workflowRepository.FirstOrDefaultAsync(output.WorkflowDocument.WorkflowId);
                output.WorkflowName = _lookupWorkflow?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_WorkflowDocuments_Edit)]
        public async Task<GetWorkflowDocumentForEditOutput> GetWorkflowDocumentForEdit(EntityDto input)
        {
            var workflowDocument = await _workflowDocumentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetWorkflowDocumentForEditOutput { WorkflowDocument = ObjectMapper.Map<CreateOrEditWorkflowDocumentDto>(workflowDocument) };

            if (output.WorkflowDocument.WorkflowId != null)
            {
                var _lookupWorkflow = await _lookup_workflowRepository.FirstOrDefaultAsync(output.WorkflowDocument.WorkflowId);
                output.WorkflowName = _lookupWorkflow?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditWorkflowDocumentDto input)
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

        [AbpAuthorize(AppPermissions.Pages_WorkflowDocuments_Create)]
        protected virtual async Task Create(CreateOrEditWorkflowDocumentDto input)
        {
            var workflowDocument = ObjectMapper.Map<WorkflowDocument>(input);

            if (AbpSession.TenantId != null)
            {
                workflowDocument.TenantId = (int)AbpSession.TenantId;
            }

            await _workflowDocumentRepository.InsertAsync(workflowDocument);

        }

        [AbpAuthorize(AppPermissions.Pages_WorkflowDocuments_Edit)]
        protected virtual async Task Update(CreateOrEditWorkflowDocumentDto input)
        {
            var workflowDocument = await _workflowDocumentRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, workflowDocument);

        }

        [AbpAuthorize(AppPermissions.Pages_WorkflowDocuments_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _workflowDocumentRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_WorkflowDocuments)]
        public async Task<List<WorkflowDocumentWorkflowLookupTableDto>> GetAllWorkflowForTableDropdown()
        {
            return await _lookup_workflowRepository.GetAll()
                .Select(workflow => new WorkflowDocumentWorkflowLookupTableDto
                {
                    Id = workflow.Id,
                    DisplayName = workflow == null || workflow.Name == null ? "" : workflow.Name.ToString()
                }).ToListAsync();
        }

    }
}