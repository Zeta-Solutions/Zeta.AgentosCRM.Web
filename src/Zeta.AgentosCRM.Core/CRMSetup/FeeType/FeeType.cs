using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.FeeType
{
    [Table("FeeTypes")]
    public class FeeType : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(FeeTypeConsts.MaxAbbrivationLength, MinimumLength = FeeTypeConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(FeeTypeConsts.MaxNameLength, MinimumLength = FeeTypeConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}