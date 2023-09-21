using System.Collections.Generic;
using Zeta.AgentosCRM.Editions;
using Zeta.AgentosCRM.Editions.Dto;
using Zeta.AgentosCRM.MultiTenancy.Payments;
using Zeta.AgentosCRM.MultiTenancy.Payments.Dto;

namespace Zeta.AgentosCRM.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
