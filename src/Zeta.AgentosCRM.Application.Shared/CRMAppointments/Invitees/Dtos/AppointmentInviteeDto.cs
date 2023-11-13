using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos
{
    public class AppointmentInviteeDto : EntityDto<long>
    {

        public long AppointmentId { get; set; }

    }
}