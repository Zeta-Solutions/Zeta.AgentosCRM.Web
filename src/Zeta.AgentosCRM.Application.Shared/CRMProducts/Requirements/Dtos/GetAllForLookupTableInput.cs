using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMProducts.Requirements.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}