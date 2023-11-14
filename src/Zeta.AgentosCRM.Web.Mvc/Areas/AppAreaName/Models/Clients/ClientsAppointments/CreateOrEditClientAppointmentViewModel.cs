using System.Collections.Generic;
using Zeta.AgentosCRM.CRMClient.Appointment;
using Zeta.AgentosCRM.CRMClient.Appointment.Dtos;
using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.CRMSetup.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients.ClientsAppointments
{
    public class CreateOrEditClientAppointmentViewModel
    {
        public CreateOrEditClientAppointmentDto ClientAppointment { get; set; }
        public List<ClientAppointmentClientLookupTableDto> ClientAppointmentInviteesList { get; set; }


        public bool IsEditMode => ClientAppointment.Id.HasValue;
    }
}
