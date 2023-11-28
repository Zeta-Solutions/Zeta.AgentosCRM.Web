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
    public interface IDocumentCheckListPartnersAppService : IApplicationService
    {
        Task<PagedResultDto<GetDocumentCheckListPartnerForViewDto>> GetAll(GetAllDocumentCheckListPartnersInput input);

        Task<GetDocumentCheckListPartnerForViewDto> GetDocumentCheckListPartnerForView(int id);

        Task<GetDocumentCheckListPartnerForEditOutput> GetDocumentCheckListPartnerForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditDocumentCheckListPartnerDto input);

        Task Delete(EntityDto input);

        Task<List<DocumentCheckListPartnerPartnerLookupTableDto>> GetAllPartnerForTableDropdown();

        Task<List<DocumentCheckListPartnerWorkflowStepDocumentCheckListLookupTableDto>> GetAllWorkflowStepDocumentCheckListForTableDropdown();

    }
}