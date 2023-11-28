using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Documents.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup.Documents
{
    public interface IWorkflowStepDocumentCheckListsAppService : IApplicationService
    {
        Task<PagedResultDto<GetWorkflowStepDocumentCheckListForViewDto>> GetAll(GetAllWorkflowStepDocumentCheckListsInput input);

        Task<GetWorkflowStepDocumentCheckListForViewDto> GetWorkflowStepDocumentCheckListForView(int id);

        Task<GetWorkflowStepDocumentCheckListForEditOutput> GetWorkflowStepDocumentCheckListForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditWorkflowStepDocumentCheckListDto input);

        Task Delete(EntityDto input);

        Task<List<WorkflowStepDocumentCheckListWorkflowStepLookupTableDto>> GetAllWorkflowStepForTableDropdown();

        Task<List<WorkflowStepDocumentCheckListDocumentTypeLookupTableDto>> GetAllDocumentTypeForTableDropdown();

    }
}