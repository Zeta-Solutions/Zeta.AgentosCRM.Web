using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMLeadInquiry.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}