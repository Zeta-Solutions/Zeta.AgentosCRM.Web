using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Documents.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup.Documents
{
    public interface IWorkflowDocumentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetWorkflowDocumentForViewDto>> GetAll(GetAllWorkflowDocumentsInput input);

        Task<GetWorkflowDocumentForViewDto> GetWorkflowDocumentForView(int id);

        Task<GetWorkflowDocumentForEditOutput> GetWorkflowDocumentForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditWorkflowDocumentDto input);

        Task Delete(EntityDto input);

        Task<List<WorkflowDocumentWorkflowLookupTableDto>> GetAllWorkflowForTableDropdown();

    }
}