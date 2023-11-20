using Zeta.AgentosCRM.CRMClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMClient.Education
{
    [Table("EnglisTestScores")]
    [Audited]
    public class EnglisTestScore : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(EnglisTestScoreConsts.MaxNameLength, MinimumLength = EnglisTestScoreConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual double Listenting { get; set; }

        public virtual double Reading { get; set; }

        public virtual double Writing { get; set; }

        public virtual double Speaking { get; set; }

        public virtual double TotalScore { get; set; }

        public virtual long ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

    }
}