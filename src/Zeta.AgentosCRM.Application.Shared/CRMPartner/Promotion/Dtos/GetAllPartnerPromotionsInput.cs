using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMPartner.Promotion.Dtos
{
    public class GetAllPartnerPromotionsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string DescriptionFilter { get; set; }

        public DateTime? MaxStartDateFilter { get; set; }
        public DateTime? MinStartDateFilter { get; set; }

        public DateTime? MaxExpiryDateFilter { get; set; }
        public DateTime? MinExpiryDateFilter { get; set; }

        public int? ApplyToFilter { get; set; }

        public string PartnerPartnerNameFilter { get; set; }

    }
}