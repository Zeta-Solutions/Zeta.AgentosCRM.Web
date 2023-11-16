using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMPartner.Promotion.Dtos
{
    public class PromotionProductDto : EntityDto<long>
    {
        public string Name { get; set; }

        public long PartnerPromotionId { get; set; }

        public long ProductId { get; set; }

    }
}