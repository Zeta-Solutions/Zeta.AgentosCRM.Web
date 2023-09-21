using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Zeta.AgentosCRM.EntityFrameworkCore;

namespace Zeta.AgentosCRM.HealthChecks
{
    public class AgentosCRMDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public AgentosCRMDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("AgentosCRMDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("AgentosCRMDbContext could not connect to database"));
        }
    }
}
