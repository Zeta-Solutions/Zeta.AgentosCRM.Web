using Microsoft.Extensions.DependencyInjection;
using Zeta.AgentosCRM.HealthChecks;

namespace Zeta.AgentosCRM.Web.HealthCheck
{
    public static class AbpZeroHealthCheck
    {
        public static IHealthChecksBuilder AddAbpZeroHealthCheck(this IServiceCollection services)
        {
            var builder = services.AddHealthChecks();
            builder.AddCheck<AgentosCRMDbContextHealthCheck>("Database Connection");
            builder.AddCheck<AgentosCRMDbContextUsersHealthCheck>("Database Connection with user check");
            builder.AddCheck<CacheHealthCheck>("Cache");

            // add your custom health checks here
            // builder.AddCheck<MyCustomHealthCheck>("my health check");

            return builder;
        }
    }
}
