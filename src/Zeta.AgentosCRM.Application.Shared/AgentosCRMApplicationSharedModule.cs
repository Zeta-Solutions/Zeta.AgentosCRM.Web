using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Zeta.AgentosCRM
{
    [DependsOn(typeof(AgentosCRMCoreSharedModule))]
    public class AgentosCRMApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AgentosCRMApplicationSharedModule).GetAssembly());
        }
    }
}