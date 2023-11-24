using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.Account
{
    [Table("TaxSettings")]
    public class TaxSetting : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(TaxSettingConsts.MaxTaxCodeLength, MinimumLength = TaxSettingConsts.MinTaxCodeLength)]
        public virtual string TaxCode { get; set; }

        public virtual decimal TaxRate { get; set; }

        public virtual bool IsDefault { get; set; }

        public virtual long OrganizationUnitId { get; set; }

        [ForeignKey("OrganizationUnitId")]
        public OrganizationUnit OrganizationUnitFk { get; set; }

    }
}