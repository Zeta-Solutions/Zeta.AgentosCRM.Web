using Zeta.AgentosCRM.CRMClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMClient.Appointment
{
    [Table("ClientAppointments")]
    [Audited]
    public class ClientAppointment : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        public virtual string TimeZone { get; set; }

        public virtual DateTime AppointmentDate { get; set; }

        public virtual DateTime AppointmentTime { get; set; }

        [Required]
        public virtual string Title { get; set; }

        [Required]
        public virtual string Description { get; set; }

        public virtual long ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

    }
}