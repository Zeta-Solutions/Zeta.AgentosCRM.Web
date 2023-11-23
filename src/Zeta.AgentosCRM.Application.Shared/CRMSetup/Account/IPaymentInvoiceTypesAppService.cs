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
    public interface IPaymentInvoiceTypesAppService : IApplicationService
    {
        Task<PagedResultDto<GetPaymentInvoiceTypeForViewDto>> GetAll(GetAllPaymentInvoiceTypesInput input);

        Task<GetPaymentInvoiceTypeForViewDto> GetPaymentInvoiceTypeForView(int id);

        Task<GetPaymentInvoiceTypeForEditOutput> GetPaymentInvoiceTypeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditPaymentInvoiceTypeDto input);

        Task Delete(EntityDto input);

        Task<List<PaymentInvoiceTypeInvoiceTypeLookupTableDto>> GetAllInvoiceTypeForTableDropdown();

        Task<List<PaymentInvoiceTypeManualPaymentDetailLookupTableDto>> GetAllManualPaymentDetailForTableDropdown();

    }
}