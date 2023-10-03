using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup
{
    [Table("DegreeLevels")]
    public class DegreeLevel : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(DegreeLevelConsts.MaxAbbrivationLength, MinimumLength = DegreeLevelConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(DegreeLevelConsts.MaxNameLength, MinimumLength = DegreeLevelConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}