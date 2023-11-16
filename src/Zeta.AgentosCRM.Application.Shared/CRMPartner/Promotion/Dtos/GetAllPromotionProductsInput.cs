using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMPartner.Promotion.Dtos
{
    public class GetAllPromotionProductsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string PartnerPromotionNameFilter { get; set; }

        public string ProductNameFilter { get; set; }

    }
}