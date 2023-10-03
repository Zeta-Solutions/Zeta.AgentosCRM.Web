using Zeta.AgentosCRM.CRMSetup;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.ProductType
{
    [Table("ProductTypes")]
    public class ProductType : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(ProductTypeConsts.MaxAbbrivaionLength, MinimumLength = ProductTypeConsts.MinAbbrivaionLength)]
        public virtual string Abbrivaion { get; set; }

        [Required]
        [StringLength(ProductTypeConsts.MaxNameLength, MinimumLength = ProductTypeConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual int MasterCategoryId { get; set; }

        [ForeignKey("MasterCategoryId")]
        public MasterCategory MasterCategoryFk { get; set; }

    }
}