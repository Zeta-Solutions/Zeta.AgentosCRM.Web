using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.ServiceCategory.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}