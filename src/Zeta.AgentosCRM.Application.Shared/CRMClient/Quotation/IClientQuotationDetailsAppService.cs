using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMClient.Quotation.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMClient.Quotation
{
    public interface IClientQuotationDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<GetClientQuotationDetailForViewDto>> GetAll(GetAllClientQuotationDetailsInput input);

        Task<GetClientQuotationDetailForViewDto> GetClientQuotationDetailForView(long id);

        Task<GetClientQuotationDetailForEditOutput> GetClientQuotationDetailForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditClientQuotationDetailDto input);

        Task Delete(EntityDto<long> input);

        Task<List<ClientQuotationDetailWorkflowLookupTableDto>> GetAllWorkflowForTableDropdown();

        Task<List<ClientQuotationDetailPartnerLookupTableDto>> GetAllPartnerForTableDropdown();

        Task<List<ClientQuotationDetailBranchLookupTableDto>> GetAllBranchForTableDropdown();

        Task<List<ClientQuotationDetailProductLookupTableDto>> GetAllProductForTableDropdown();

        Task<List<ClientQuotationDetailClientQuotationHeadLookupTableDto>> GetAllClientQuotationHeadForTableDropdown();

    }
}