using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.Notifications.Dto
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}