using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.TaskManagement.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}