using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMPartner.Promotion.Dtos
{
    public class GetPromotionProductForEditOutput
    {
        public CreateOrEditPromotionProductDto PromotionProduct { get; set; }

        public string PartnerPromotionName { get; set; }

        public string ProductName { get; set; }

    }
}