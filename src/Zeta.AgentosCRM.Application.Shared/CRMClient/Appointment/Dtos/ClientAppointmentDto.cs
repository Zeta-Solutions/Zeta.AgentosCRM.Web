using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.Appointment.Dtos
{
    public class ClientAppointmentDto : EntityDto<long>
    {
        public string TimeZone { get; set; }

        public DateTime AppointmentDate { get; set; }

        public DateTime AppointmentTime { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long ClientId { get; set; }

    }
}