using System.Threading.Tasks;
using Abp.Application.Services;
using Zeta.AgentosCRM.Configuration.Tenants.Dto;

namespace Zeta.AgentosCRM.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearDarkLogo();
        
        Task ClearLightLogo();

        Task ClearCustomCss();
    }
}
