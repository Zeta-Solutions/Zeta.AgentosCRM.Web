using Zeta.AgentosCRM.CRMSetup;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup
{
    [Table("PartnerTypes")]
    public class PartnerType : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(PartnerTypeConsts.MaxAbbrivationLength, MinimumLength = PartnerTypeConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(PartnerTypeConsts.MaxNameLength, MinimumLength = PartnerTypeConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual int MasterCategoryId { get; set; }

        [ForeignKey("MasterCategoryId")]
        public MasterCategory MasterCategoryFk { get; set; }

    }
}