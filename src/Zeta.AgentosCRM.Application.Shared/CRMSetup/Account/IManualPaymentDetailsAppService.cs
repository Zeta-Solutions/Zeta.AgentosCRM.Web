using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Account.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup.Account
{
    public interface IManualPaymentDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<GetManualPaymentDetailForViewDto>> GetAll(GetAllManualPaymentDetailsInput input);

        Task<GetManualPaymentDetailForViewDto> GetManualPaymentDetailForView(int id);

        Task<GetManualPaymentDetailForEditOutput> GetManualPaymentDetailForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditManualPaymentDetailDto input);

        Task Delete(EntityDto input);

        Task<List<ManualPaymentDetailOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown();

    }
}