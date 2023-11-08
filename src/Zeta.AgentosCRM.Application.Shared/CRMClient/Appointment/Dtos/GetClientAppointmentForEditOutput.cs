using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Appointment.Dtos
{
    public class GetClientAppointmentForEditOutput
    {
        public CreateOrEditClientAppointmentDto ClientAppointment { get; set; }

        public string ClientDisplayProperty { get; set; }

    }
}