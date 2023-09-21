using System.Collections.Generic;
using Zeta.AgentosCRM.Editions.Dto;
using Zeta.AgentosCRM.MultiTenancy.Payments;

namespace Zeta.AgentosCRM.Web.Models.Payment
{
    public class UpgradeEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public PaymentPeriodType PaymentPeriodType { get; set; }

        public SubscriptionPaymentType SubscriptionPaymentType { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}