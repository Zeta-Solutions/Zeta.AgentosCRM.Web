using System.Threading.Tasks;
using Abp.Webhooks;

namespace Zeta.AgentosCRM.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
