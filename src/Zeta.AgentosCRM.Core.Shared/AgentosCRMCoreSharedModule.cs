using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Zeta.AgentosCRM
{
    public class AgentosCRMCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AgentosCRMCoreSharedModule).GetAssembly());
        }
    }
}