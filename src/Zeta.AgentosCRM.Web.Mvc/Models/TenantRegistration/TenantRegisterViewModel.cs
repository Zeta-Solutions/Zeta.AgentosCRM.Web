using Zeta.AgentosCRM.Editions;
using Zeta.AgentosCRM.Editions.Dto;
using Zeta.AgentosCRM.MultiTenancy.Payments;
using Zeta.AgentosCRM.Security;
using Zeta.AgentosCRM.MultiTenancy.Payments.Dto;

namespace Zeta.AgentosCRM.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
