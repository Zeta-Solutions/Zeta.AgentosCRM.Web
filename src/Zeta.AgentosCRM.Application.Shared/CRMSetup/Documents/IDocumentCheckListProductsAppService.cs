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
    public interface IDocumentCheckListProductsAppService : IApplicationService
    {
        Task<PagedResultDto<GetDocumentCheckListProductForViewDto>> GetAll(GetAllDocumentCheckListProductsInput input);

        Task<GetDocumentCheckListProductForViewDto> GetDocumentCheckListProductForView(int id);

        Task<GetDocumentCheckListProductForEditOutput> GetDocumentCheckListProductForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditDocumentCheckListProductDto input);

        Task Delete(EntityDto input);

        Task<List<DocumentCheckListProductProductLookupTableDto>> GetAllProductForTableDropdown();

        Task<List<DocumentCheckListProductWorkflowStepDocumentCheckListLookupTableDto>> GetAllWorkflowStepDocumentCheckListForTableDropdown();

    }
}