using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMPartner.Promotion
{
    [Table("PartnerPromotions")]
    [Audited]
    public class PartnerPromotion : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(PartnerPromotionConsts.MaxNameLength, MinimumLength = PartnerPromotionConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(PartnerPromotionConsts.MaxDescriptionLength, MinimumLength = PartnerPromotionConsts.MinDescriptionLength)]
        public virtual string Description { get; set; }
        //File

        public virtual Guid? Attachment { get; set; } //File, (BinaryObjectId)

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime ExpiryDate { get; set; }

        public virtual bool ApplyTo { get; set; }

        public virtual long PartnerId { get; set; }

        [ForeignKey("PartnerId")]
        public Partner PartnerFk { get; set; }

    }
}