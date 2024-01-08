using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.Tenants.Email.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}