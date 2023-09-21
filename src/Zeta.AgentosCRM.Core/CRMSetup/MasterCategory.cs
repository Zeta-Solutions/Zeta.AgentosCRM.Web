using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup
{
    [Table("MasterCategories")]
    public class MasterCategory : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(MasterCategoryConsts.MaxAbbrivationLength, MinimumLength = MasterCategoryConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(MasterCategoryConsts.MaxNameLength, MinimumLength = MasterCategoryConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}