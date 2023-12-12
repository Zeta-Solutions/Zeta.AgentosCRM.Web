using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMClient.Quotation.Dtos
{
    public class GetAllClientQuotationDetailsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string DescriptionFilter { get; set; }

        public decimal? MaxServiceFeeFilter { get; set; }
        public decimal? MinServiceFeeFilter { get; set; }

        public decimal? MaxDiscountFilter { get; set; }
        public decimal? MinDiscountFilter { get; set; }

        public decimal? MaxNetFeeFilter { get; set; }
        public decimal? MinNetFeeFilter { get; set; }

        public decimal? MaxExchangeRateFilter { get; set; }
        public decimal? MinExchangeRateFilter { get; set; }

        public decimal? MaxTotalAmountFilter { get; set; }
        public decimal? MinTotalAmountFilter { get; set; }

        public string WorkflowNameFilter { get; set; }

        public string PartnerPartnerNameFilter { get; set; }

        public string BranchNameFilter { get; set; }

        public string ProductNameFilter { get; set; }

        public string ClientQuotationHeadClientNameFilter { get; set; }

    }
}