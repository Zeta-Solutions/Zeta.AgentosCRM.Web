using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.TaskManagement.Followers.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}