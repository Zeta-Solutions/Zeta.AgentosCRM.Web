using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMAppointments.Dtos
{
    public class CreateOrEditAppointmentDto : EntityDto<long?>
    {

        public bool RelatedTo { get; set; }

        [Required]
        [StringLength(AppointmentConsts.MaxTitleLength, MinimumLength = AppointmentConsts.MinTitleLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(AppointmentConsts.MaxDescriptionLength, MinimumLength = AppointmentConsts.MinDescriptionLength)]
        public string Description { get; set; }

        public DateTime AppointmentDate { get; set; }

        public DateTime AppointmentTime { get; set; }

        public string TimeZone { get; set; }

        public long? ClientId { get; set; }

        public long? PartnerId { get; set; }

        public long AddedById { get; set; }

    }
}