using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.AttachmentTest.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}