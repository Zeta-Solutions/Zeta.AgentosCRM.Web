using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Zeta.AgentosCRM.Startup
{
    [DependsOn(typeof(AgentosCRMCoreModule))]
    public class AgentosCRMGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AgentosCRMGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}