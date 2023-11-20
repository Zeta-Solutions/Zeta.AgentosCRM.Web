using Zeta.AgentosCRM.CRMClient.CheckIn;
using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMClient.CheckIn
{
    [Table("CheckInLogs")]
    [Audited]
    public class CheckInLog : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(CheckInLogConsts.MaxTitleLength, MinimumLength = CheckInLogConsts.MinTitleLength)]
        public virtual string Title { get; set; }

        [Required]
        [StringLength(CheckInLogConsts.MaxCheckInPurposeLength, MinimumLength = CheckInLogConsts.MinCheckInPurposeLength)]
        public virtual string CheckInPurpose { get; set; }

        public virtual CheckInStatus CheckInStatus { get; set; }

        public virtual DateTime CheckInDate { get; set; }

        public virtual DateTime StartTime { get; set; }

        public virtual DateTime EndTime { get; set; }

        public virtual long ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

        public virtual long CheckInAssigneeId { get; set; }

        [ForeignKey("CheckInAssigneeId")]
        public User CheckInAssigneeFk { get; set; }

    }
}