using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMProducts.Fee.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}