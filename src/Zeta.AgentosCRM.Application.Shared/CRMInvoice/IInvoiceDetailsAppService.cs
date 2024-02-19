using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMInvoice.Dtos;

namespace Zeta.AgentosCRM.CRMInvoice
{
    public interface IInvoiceDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<GetInvoiceDetailForViewDto>> GetAll(GetAllInvoiceDetailsInput input);

        Task<GetInvoiceDetailForViewDto> GetInvoiceDetailForView(long id);

        Task<GetInvoiceDetailForEditOutput> GetInvoiceDetailForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditInvoiceDetailDto input);

        Task Delete(EntityDto<long> input);
        Task<List<InvoiceTaxLookupTableDto>> GetAllTaxForTableDropdown();

        Task<List<InvoiceIncomeTypeLookupTableDto>> GetAllIncomeTypeForTableDropdown();
    }
}
