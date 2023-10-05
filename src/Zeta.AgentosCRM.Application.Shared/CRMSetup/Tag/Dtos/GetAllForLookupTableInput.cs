using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Tag.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}