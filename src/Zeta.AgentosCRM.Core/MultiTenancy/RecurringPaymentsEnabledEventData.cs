using Abp.Events.Bus;

namespace Zeta.AgentosCRM.MultiTenancy
{
    public class RecurringPaymentsEnabledEventData : EventData
    {
        public int TenantId { get; set; }
    }
}