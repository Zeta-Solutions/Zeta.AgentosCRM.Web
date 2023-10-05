using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.InstallmentType
{
    [Table("InstallmentTypes")]
    public class InstallmentType : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(InstallmentTypeConsts.MaxAbbrivationLength, MinimumLength = InstallmentTypeConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(InstallmentTypeConsts.MaxNameLength, MinimumLength = InstallmentTypeConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}