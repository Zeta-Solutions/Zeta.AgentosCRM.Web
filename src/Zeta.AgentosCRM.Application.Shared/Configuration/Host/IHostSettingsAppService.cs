using System.Threading.Tasks;
using Abp.Application.Services;
using Zeta.AgentosCRM.Configuration.Host.Dto;

namespace Zeta.AgentosCRM.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
