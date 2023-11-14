using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Appointment.Dtos
{
    public class CreateOrEditClientAppointmentDto : EntityDto<long?>
    {

        [Required]
        public string TimeZone { get; set; }



        public DateTime AppointmentDate { get; set; }

        public DateTime AppointmentTime { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public long ClientId { get; set; }

    }
}