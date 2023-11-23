using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.Account
{
    [Table("InvoiceTypes")]
    public class InvoiceType : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(InvoiceTypeConsts.MaxAbbrivationLength, MinimumLength = InvoiceTypeConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(InvoiceTypeConsts.MaxNameLength, MinimumLength = InvoiceTypeConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}