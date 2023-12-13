using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMProducts.Fee.Dtos
{
    public class GetAllProductFeesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string ClaimTermsFilter { get; set; }

        public decimal? MaxCommissionPerFilter { get; set; }
        public decimal? MinCommissionPerFilter { get; set; }

        public decimal? MaxNetTotalFilter { get; set; }
        public decimal? MinNetTotalFilter { get; set; }

        public string CountryNameFilter { get; set; }

        public string InstallmentTypeNameFilter { get; set; }

        public int? ProductIdFilter { get; set; }

    }
}