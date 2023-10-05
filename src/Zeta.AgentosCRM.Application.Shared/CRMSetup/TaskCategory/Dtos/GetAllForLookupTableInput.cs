using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.TaskCategory.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}