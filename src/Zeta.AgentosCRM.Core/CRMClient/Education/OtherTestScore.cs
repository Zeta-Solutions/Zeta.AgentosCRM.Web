using Zeta.AgentosCRM.CRMClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMClient.Education
{
    [Table("OtherTestScores")]
    [Audited]
    public class OtherTestScore : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(OtherTestScoreConsts.MaxNameLength, MinimumLength = OtherTestScoreConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual double TotalScore { get; set; }

        public virtual long ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

    }
}