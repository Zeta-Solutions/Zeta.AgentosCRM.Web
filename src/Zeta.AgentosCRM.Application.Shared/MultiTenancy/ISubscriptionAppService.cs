using System.Threading.Tasks;
using Abp.Application.Services;

namespace Zeta.AgentosCRM.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
