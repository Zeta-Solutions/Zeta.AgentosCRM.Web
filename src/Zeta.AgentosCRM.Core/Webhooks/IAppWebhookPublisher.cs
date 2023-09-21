using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization.Users;

namespace Zeta.AgentosCRM.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
