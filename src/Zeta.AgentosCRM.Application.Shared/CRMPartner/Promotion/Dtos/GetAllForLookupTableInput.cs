using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMPartner.Promotion.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}