using Zeta.AgentosCRM.CRMProducts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMProducts.Requirements
{
    [Table("ProductEnglishRequirements")]
    [Audited]
    public class ProductEnglishRequirement : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        public virtual string Name { get; set; }

        public virtual double Listening { get; set; }

        public virtual double Reading { get; set; }

        public virtual double Writing { get; set; }

        public virtual double Speaking { get; set; }

        public virtual double TotalScore { get; set; }

        public virtual long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product ProductFk { get; set; }

    }
}