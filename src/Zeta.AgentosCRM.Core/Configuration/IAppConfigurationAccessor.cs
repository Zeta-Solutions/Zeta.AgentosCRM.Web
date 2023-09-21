using Microsoft.Extensions.Configuration;

namespace Zeta.AgentosCRM.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
