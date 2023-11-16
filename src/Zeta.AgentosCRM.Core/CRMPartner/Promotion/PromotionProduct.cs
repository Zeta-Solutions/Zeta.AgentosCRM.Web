using Zeta.AgentosCRM.CRMPartner.Promotion;
using Zeta.AgentosCRM.CRMProducts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMPartner.Promotion
{
    [Table("PromotionProducts")]
    [Audited]
    public class PromotionProduct : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string Name { get; set; }

        public virtual long PartnerPromotionId { get; set; }

        [ForeignKey("PartnerPromotionId")]
        public PartnerPromotion PartnerPromotionFk { get; set; }

        public virtual long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product ProductFk { get; set; }

    }
}