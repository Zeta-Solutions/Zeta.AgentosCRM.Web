using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Account.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Account
{
    public interface IInvoiceTypesAppService : IApplicationService
    {
        Task<PagedResultDto<GetInvoiceTypeForViewDto>> GetAll(GetAllInvoiceTypesInput input);

        Task<GetInvoiceTypeForViewDto> GetInvoiceTypeForView(int id);

        Task<GetInvoiceTypeForEditOutput> GetInvoiceTypeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditInvoiceTypeDto input);

        Task Delete(EntityDto input);

    }
}