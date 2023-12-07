using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMProducts.Fee.Dtos
{
    public class GetAllProductFeeDetailsForExcelInput
    {
        public string Filter { get; set; }

        public decimal? MaxInstallmentAmountFilter { get; set; }
        public decimal? MinInstallmentAmountFilter { get; set; }

        public int? MaxInstallmentsFilter { get; set; }
        public int? MinInstallmentsFilter { get; set; }

        public decimal? MaxTotalFeeFilter { get; set; }
        public decimal? MinTotalFeeFilter { get; set; }

        public string ClaimTermsFilter { get; set; }

        public double? MaxCommissionPerFilter { get; set; }
        public double? MinCommissionPerFilter { get; set; }

        public int? IsPayableFilter { get; set; }

        public int? AddInQuotationFilter { get; set; }

        public string FeeTypeNameFilter { get; set; }

        public string ProductFeeNameFilter { get; set; }

    }
}