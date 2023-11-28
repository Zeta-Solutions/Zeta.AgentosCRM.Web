using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Document.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}