using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMPartner.Contract.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}