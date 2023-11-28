using Zeta.AgentosCRM.CRMAppointments;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Zeta.AgentosCRM.Authorization.Users;

namespace Zeta.AgentosCRM.CRMAppointments.Invitees
{
    [Table("AppointmentInvitees")]
    [Audited]
    public class AppointmentInvitee : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual long AppointmentId { get; set; }

        [ForeignKey("AppointmentId")]
        public Appointment AppointmentFk { get; set; }

        public virtual long UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserFk { get; set; }

    }
}