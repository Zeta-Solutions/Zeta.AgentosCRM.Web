using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMSetup.Dtos;

namespace Zeta.AgentosCRM.CRMPartner.Promotion.Dtos
{
    public class CreateOrEditPartnerPromotionDto : EntityDto<long?>
    {

        [Required]
        [StringLength(PartnerPromotionConsts.MaxNameLength, MinimumLength = PartnerPromotionConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PartnerPromotionConsts.MaxDescriptionLength, MinimumLength = PartnerPromotionConsts.MinDescriptionLength)]
        public string Description { get; set; }

        public Guid? Attachment { get; set; }

        public string AttachmentToken { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool ApplyTo { get; set; }

        public long PartnerId { get; set; }
     

        public List<CreateOrEditPromotionProductDto> Steps { get; set; }

    }
}