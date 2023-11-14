using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMPartner.Promotion.Dtos
{
    public class PartnerPromotionDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? Attachment { get; set; }

        public string AttachmentFileName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool ApplyTo { get; set; }

        public long PartnerId { get; set; }

    }
}