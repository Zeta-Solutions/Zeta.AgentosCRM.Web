using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.Education.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}