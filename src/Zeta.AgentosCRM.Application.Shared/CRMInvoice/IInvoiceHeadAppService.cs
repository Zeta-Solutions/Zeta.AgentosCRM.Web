using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMInvoice.Dtos;

namespace Zeta.AgentosCRM.CRMInvoice
{
    public interface IInvoiceHeadAppService : IApplicationService
    {
        Task<PagedResultDto<GetInvoiceHeadForViewDto>> GetAll(GetAllInvoiceHeadInput input);

        Task<GetInvoiceHeadForViewDto> GetInvoiceHeadForView(long id);

        Task<GetInvoiceHeadForEditOutput> GetInvoiceHeadForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditInvoiceHeadDto input);

        Task Delete(EntityDto<long> input);

        Task<List<InvoicePaymentLookupTableDto>> GetAllPaymentForTableDropdown();

        Task<List<InvoiceCurrencyLookupTableDto>> GetAllCurrencyForTableDropdown();
    }
}
