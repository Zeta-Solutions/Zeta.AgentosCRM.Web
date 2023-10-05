using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Countries.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}