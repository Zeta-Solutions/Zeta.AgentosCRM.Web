using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos
{
    public class GetAppointmentInviteeForEditOutput
    {
        public CreateOrEditAppointmentInviteeDto AppointmentInvitee { get; set; }

        public string AppointmentTitle { get; set; }

    }
}