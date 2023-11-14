using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}