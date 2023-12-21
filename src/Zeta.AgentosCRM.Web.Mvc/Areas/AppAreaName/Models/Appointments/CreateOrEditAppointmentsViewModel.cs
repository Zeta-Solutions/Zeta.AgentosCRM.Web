using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMAppointments.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Appointments
{
    public class CreateOrEditAppointmentsViewModel
    {
        public CreateOrEditAppointmentDto Appointment { get; set; }
        public List<AppointmentUserLookupTableDto> AppointmentInviteesList { get; set; }

        public List<ComboboxItemDto> TimezoneItems { get; set; }
        public bool IsEditMode => Appointment.Id.HasValue;
    }
}
