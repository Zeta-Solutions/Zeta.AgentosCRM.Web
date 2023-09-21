using Abp.Application.Services;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Logging.Dto;

namespace Zeta.AgentosCRM.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
