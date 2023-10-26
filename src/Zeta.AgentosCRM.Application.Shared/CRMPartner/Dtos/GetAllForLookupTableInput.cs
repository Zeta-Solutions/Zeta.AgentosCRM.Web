using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMPartner.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}