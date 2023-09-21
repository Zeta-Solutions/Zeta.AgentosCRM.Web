using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Zeta.AgentosCRM.Authorization;

namespace Zeta.AgentosCRM
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(AgentosCRMApplicationSharedModule),
        typeof(AgentosCRMCoreModule)
        )]
    public class AgentosCRMApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AgentosCRMApplicationModule).GetAssembly());
        }
    }
}