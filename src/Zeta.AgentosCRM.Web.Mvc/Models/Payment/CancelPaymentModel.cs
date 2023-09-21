using Zeta.AgentosCRM.MultiTenancy.Payments;

namespace Zeta.AgentosCRM.Web.Models.Payment
{
    public class CancelPaymentModel
    {
        public string PaymentId { get; set; }

        public SubscriptionPaymentGatewayType Gateway { get; set; }
    }
}