using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMApplications.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}