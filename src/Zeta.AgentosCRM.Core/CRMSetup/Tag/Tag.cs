using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.Tag
{
    [Table("Tags")]
    public class Tag : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(TagConsts.MaxAbbrivationLength, MinimumLength = TagConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(TagConsts.MaxNameLength, MinimumLength = TagConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}