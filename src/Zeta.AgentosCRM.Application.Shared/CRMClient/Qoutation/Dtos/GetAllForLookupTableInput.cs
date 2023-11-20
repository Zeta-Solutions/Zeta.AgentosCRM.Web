using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.Qoutation.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}