using Zeta.AgentosCRM.CRMProducts;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMProducts
{
    [Table("ProductBranches")]
    [Audited]
    public class ProductBranch : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string Name { get; set; }

        public virtual long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product ProductFk { get; set; }

        public virtual long? BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch BranchFk { get; set; }

    }
}