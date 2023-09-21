using System.Threading.Tasks;
using Abp.Application.Services;
using Zeta.AgentosCRM.Install.Dto;

namespace Zeta.AgentosCRM.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}