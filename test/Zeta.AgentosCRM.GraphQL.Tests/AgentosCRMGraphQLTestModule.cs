using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Zeta.AgentosCRM.Configure;
using Zeta.AgentosCRM.Startup;
using Zeta.AgentosCRM.Test.Base;

namespace Zeta.AgentosCRM.GraphQL.Tests
{
    [DependsOn(
        typeof(AgentosCRMGraphQLModule),
        typeof(AgentosCRMTestBaseModule))]
    public class AgentosCRMGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AgentosCRMGraphQLTestModule).GetAssembly());
        }
    }
}