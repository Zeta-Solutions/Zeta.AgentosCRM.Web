using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMAgent.Contacts.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}