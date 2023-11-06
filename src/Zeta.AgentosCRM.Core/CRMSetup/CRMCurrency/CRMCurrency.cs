using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.CRMCurrency
{
    [Table("CRMCurrencies")]
    public class CRMCurrency : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(CRMCurrencyConsts.MaxAbbrivationLength, MinimumLength = CRMCurrencyConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(CRMCurrencyConsts.MaxNameLength, MinimumLength = CRMCurrencyConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}