using Abp.Dependency;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using Zeta.AgentosCRM.Configuration;

namespace Zeta.AgentosCRM.Test.Base.Configuration
{
    public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public TestAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(
                typeof(AgentosCRMTestBaseModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }
    }
}
