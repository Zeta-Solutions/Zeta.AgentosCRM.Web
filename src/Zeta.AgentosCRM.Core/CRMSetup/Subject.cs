using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup
{
    [Table("Subjects")]
    public class Subject : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(SubjectConsts.MaxAbbrivationLength, MinimumLength = SubjectConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(SubjectConsts.MaxNameLength, MinimumLength = SubjectConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}