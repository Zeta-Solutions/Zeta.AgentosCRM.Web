using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Account.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup.Account
{
    public interface IInvoiceAddressesAppService : IApplicationService
    {
        Task<PagedResultDto<GetInvoiceAddressForViewDto>> GetAll(GetAllInvoiceAddressesInput input);

        Task<GetInvoiceAddressForViewDto> GetInvoiceAddressForView(int id);

        Task<GetInvoiceAddressForEditOutput> GetInvoiceAddressForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditInvoiceAddressDto input);

        Task Delete(EntityDto input);

        Task<List<InvoiceAddressCountryLookupTableDto>> GetAllCountryForTableDropdown();

        Task<List<InvoiceAddressOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown();

    }
}