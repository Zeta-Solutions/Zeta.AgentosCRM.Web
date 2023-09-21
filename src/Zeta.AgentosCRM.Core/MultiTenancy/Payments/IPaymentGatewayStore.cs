using System.Collections.Generic;

namespace Zeta.AgentosCRM.MultiTenancy.Payments
{
    public interface IPaymentGatewayStore
    {
        List<PaymentGatewayModel> GetActiveGateways();
    }
}
