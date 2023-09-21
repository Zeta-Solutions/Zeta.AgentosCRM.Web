using System.Threading.Tasks;
using Abp.Application.Services;
using Zeta.AgentosCRM.MultiTenancy.Payments.Dto;
using Zeta.AgentosCRM.MultiTenancy.Payments.Stripe.Dto;

namespace Zeta.AgentosCRM.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}