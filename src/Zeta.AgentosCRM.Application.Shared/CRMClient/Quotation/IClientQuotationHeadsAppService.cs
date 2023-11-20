using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMClient.Quotation.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMClient.Quotation
{
    public interface IClientQuotationHeadsAppService : IApplicationService
    {
        Task<PagedResultDto<GetClientQuotationHeadForViewDto>> GetAll(GetAllClientQuotationHeadsInput input);

        Task<GetClientQuotationHeadForViewDto> GetClientQuotationHeadForView(long id);

        Task<GetClientQuotationHeadForEditOutput> GetClientQuotationHeadForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditClientQuotationHeadDto input);

        Task Delete(EntityDto<long> input);

        Task<List<ClientQuotationHeadClientLookupTableDto>> GetAllClientForTableDropdown();

        Task<List<ClientQuotationHeadCRMCurrencyLookupTableDto>> GetAllCRMCurrencyForTableDropdown();

    }
}