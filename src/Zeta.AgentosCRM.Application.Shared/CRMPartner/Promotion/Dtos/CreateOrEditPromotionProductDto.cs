using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMPartner.Promotion.Dtos
{
    public class CreateOrEditPromotionProductDto : EntityDto<long?>
    {

        public string Name { get; set; }

        public long PartnerPromotionId { get; set; }


        public long ProductId { get; set; }

    }
}