using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMApplications.Stages.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}