using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Zeta.AgentosCRM
{
    public class AgentosCRMClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AgentosCRMClientModule).GetAssembly());
        }
    }
}
