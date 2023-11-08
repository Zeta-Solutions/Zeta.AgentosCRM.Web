using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMPartner.Contact.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}