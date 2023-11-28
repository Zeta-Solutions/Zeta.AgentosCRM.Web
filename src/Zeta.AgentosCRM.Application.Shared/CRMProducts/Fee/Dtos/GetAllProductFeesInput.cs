using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMProducts.Fee.Dtos
{
    public class GetAllProductFeesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public int? MaxInstallmentsFilter { get; set; }
        public int? MinInstallmentsFilter { get; set; }

        public decimal? MaxInstallmentAmountFilter { get; set; }
        public decimal? MinInstallmentAmountFilter { get; set; }

        public decimal? MaxTotalFeeFilter { get; set; }
        public decimal? MinTotalFeeFilter { get; set; }

        public string ClaimTermsFilter { get; set; }

        public decimal? MaxCommissionPerFilter { get; set; }
        public decimal? MinCommissionPerFilter { get; set; }

        public int? AddInQuotationFilter { get; set; }

        public decimal? MaxNetTotalFilter { get; set; }
        public decimal? MinNetTotalFilter { get; set; }

        public string CountryNameFilter { get; set; }

        public string InstallmentTypeNameFilter { get; set; }

        public string FeeTypeNameFilter { get; set; }

        public string ProductNameFilter { get; set; }

    }
}