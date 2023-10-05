using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.ProductType.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}