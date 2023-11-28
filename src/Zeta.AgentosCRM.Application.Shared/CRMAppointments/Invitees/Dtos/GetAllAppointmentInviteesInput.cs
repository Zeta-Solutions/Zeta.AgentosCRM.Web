using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos
{
    public class GetAllAppointmentInviteesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string AppointmentTitleFilter { get; set; }

        public string UserNameFilter { get; set; }

    }
}