using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMAppointments.Dtos
{
    public class AppointmentDto : EntityDto<long>
    {
        public bool RelatedTo { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime AppointmentDate { get; set; }

        public DateTime AppointmentTime { get; set; }

        public string TimeZone { get; set; }

        public long? ClientId { get; set; }

        public long? PartnerId { get; set; }

        public long AddedById { get; set; }

    }
}