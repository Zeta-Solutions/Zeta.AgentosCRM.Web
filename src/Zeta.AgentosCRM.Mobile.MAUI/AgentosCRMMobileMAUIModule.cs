using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Zeta.AgentosCRM.ApiClient;
using Zeta.AgentosCRM.Mobile.MAUI.Core.ApiClient;

namespace Zeta.AgentosCRM
{
    [DependsOn(typeof(AgentosCRMClientModule), typeof(AbpAutoMapperModule))]

    public class AgentosCRMMobileMAUIModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

            Configuration.ReplaceService<IApplicationContext, MAUIApplicationContext>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AgentosCRMMobileMAUIModule).GetAssembly());
        }
    }
}