using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}