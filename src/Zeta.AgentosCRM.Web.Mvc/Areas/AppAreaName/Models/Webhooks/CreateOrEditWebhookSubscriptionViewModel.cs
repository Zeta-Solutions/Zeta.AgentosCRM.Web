using Abp.Application.Services.Dto;
using Abp.Webhooks;
using Zeta.AgentosCRM.WebHooks.Dto;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Webhooks
{
    public class CreateOrEditWebhookSubscriptionViewModel
    {
        public WebhookSubscription WebhookSubscription { get; set; }

        public ListResultDto<GetAllAvailableWebhooksOutput> AvailableWebhookEvents { get; set; }
    }
}
