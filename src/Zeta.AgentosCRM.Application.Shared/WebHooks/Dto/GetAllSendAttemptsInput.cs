using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.WebHooks.Dto
{
    public class GetAllSendAttemptsInput : PagedInputDto
    {
        public string SubscriptionId { get; set; }
    }
}
