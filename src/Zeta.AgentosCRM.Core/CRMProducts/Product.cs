using Zeta.AgentosCRM.CRMProducts;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMProducts
{
    [Table("Products")]
    [Audited]
    public class Product : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(ProductConsts.MaxNameLength, MinimumLength = ProductConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Duration { get; set; }

        [Required]
        [StringLength(ProductConsts.MaxDescriptionLength, MinimumLength = ProductConsts.MinDescriptionLength)]
        public virtual string Description { get; set; }

        public virtual string Note { get; set; }

        public virtual bool RevenueType { get; set; }

        public virtual Months IntakeMonth { get; set; }

        public virtual long PartnerId { get; set; }

        [ForeignKey("PartnerId")]
        public Partner PartnerFk { get; set; }

        public virtual int PartnerTypeId { get; set; }

        [ForeignKey("PartnerTypeId")]
        public PartnerType PartnerTypeFk { get; set; }

        public virtual long BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch BranchFk { get; set; }

    }
}