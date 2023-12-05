using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMPartner.Promotion.Dtos;
using Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos;

namespace Zeta.AgentosCRM.CRMAppointments.Dtos
{
    public class GetAppointmentForEditOutput
    {
        public CreateOrEditAppointmentDto Appointment { get; set; }

        public string ClientDisplayProperty { get; set; }

        public string PartnerPartnerName { get; set; }

        public string UserName { get; set; }

        public List<CreateOrEditAppointmentInviteeDto> Appointmentinvitees { get; set; }
    }
}