using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMSetup.Dtos;

namespace Zeta.AgentosCRM.CRMPartner.Promotion.Dtos
{
    public class GetPartnerPromotionForEditOutput
    {
        public CreateOrEditPartnerPromotionDto PartnerPromotion { get; set; }

        public string PartnerPartnerName { get; set; }

        public string AttachmentFileName { get; set; }

        public List<CreateOrEditPromotionProductDto> PromotionProduct { get; set; }
    }
}