using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMAppointments
{
    [Table("Appointments")]
    [Audited]
    public class Appointment : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual bool RelatedTo { get; set; }

        [Required]
        [StringLength(AppointmentConsts.MaxTitleLength, MinimumLength = AppointmentConsts.MinTitleLength)]
        public virtual string Title { get; set; }

        [Required]
        [StringLength(AppointmentConsts.MaxDescriptionLength, MinimumLength = AppointmentConsts.MinDescriptionLength)]
        public virtual string Description { get; set; }

        public virtual DateTime AppointmentDate { get; set; }

        public virtual DateTime AppointmentTime { get; set; }

        public virtual string TimeZone { get; set; }

        public virtual long? ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

        public virtual long? PartnerId { get; set; }

        [ForeignKey("PartnerId")]
        public Partner PartnerFk { get; set; }

        public virtual long AddedById { get; set; }

        [ForeignKey("AddedById")]
        public User AddedByFk { get; set; }

    }
}