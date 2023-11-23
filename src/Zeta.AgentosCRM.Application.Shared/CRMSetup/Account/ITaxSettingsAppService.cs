using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Account.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup.Account
{
    public interface ITaxSettingsAppService : IApplicationService
    {
        Task<PagedResultDto<GetTaxSettingForViewDto>> GetAll(GetAllTaxSettingsInput input);

        Task<GetTaxSettingForViewDto> GetTaxSettingForView(int id);

        Task<GetTaxSettingForEditOutput> GetTaxSettingForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditTaxSettingDto input);

        Task Delete(EntityDto input);

        Task<List<TaxSettingOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown();

    }
}