using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMPartner.Promotion.Dtos
{
    public class GetPartnerPromotionForEditOutput
    {
        public CreateOrEditPartnerPromotionDto PartnerPromotion { get; set; }

        public string PartnerPartnerName { get; set; }

        public string AttachmentFileName { get; set; }

    }
}