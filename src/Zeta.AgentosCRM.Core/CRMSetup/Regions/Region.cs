using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.Regions
{
    [Table("Regions")]
    public class Region : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(RegionConsts.MaxAbbrivationLength, MinimumLength = RegionConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(RegionConsts.MaxNameLength, MinimumLength = RegionConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}