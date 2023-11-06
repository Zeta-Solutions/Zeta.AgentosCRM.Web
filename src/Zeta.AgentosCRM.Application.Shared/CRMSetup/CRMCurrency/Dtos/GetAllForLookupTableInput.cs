using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.CRMCurrency.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}