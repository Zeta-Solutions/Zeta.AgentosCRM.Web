using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.CheckIn.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}