using Zeta.AgentosCRM.CRMSetup;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup
{
    [Table("SubjectAreas")]
    public class SubjectArea : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(SubjectAreaConsts.MaxAbbrivationLength, MinimumLength = SubjectAreaConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(SubjectAreaConsts.MaxNameLength, MinimumLength = SubjectAreaConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual int SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public Subject SubjectFk { get; set; }

    }
}