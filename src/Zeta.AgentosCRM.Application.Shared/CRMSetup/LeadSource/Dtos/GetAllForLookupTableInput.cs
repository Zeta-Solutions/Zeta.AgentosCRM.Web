using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.LeadSource.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}