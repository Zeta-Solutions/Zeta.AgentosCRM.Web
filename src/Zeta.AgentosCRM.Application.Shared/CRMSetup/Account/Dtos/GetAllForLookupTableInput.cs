using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}