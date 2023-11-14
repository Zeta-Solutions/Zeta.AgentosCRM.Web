using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos
{
    public class CreateOrEditAppointmentInviteeDto : EntityDto<long?>
    {

        public long AppointmentId { get; set; }

    }
}