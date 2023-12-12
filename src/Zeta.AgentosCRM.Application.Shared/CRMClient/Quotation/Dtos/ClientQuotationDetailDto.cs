using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.Quotation.Dtos
{
    public class ClientQuotationDetailDto : EntityDto<long>
    {
        public string Description { get; set; }

        public decimal ServiceFee { get; set; }

        public decimal Discount { get; set; }

        public decimal NetFee { get; set; }

        public decimal ExchangeRate { get; set; }

        public decimal TotalAmount { get; set; }

        public string WorkflowName { get; set; }
        public int WorkflowId { get; set; }

        public string PartnerName { get; set; }
        public long PartnerId { get; set; }

        public string BranchName { get; set; }
        public long BranchId { get; set; }

        public string ProductName { get; set; }
        public long ProductId { get; set; }

        public long QuotationHeadId { get; set; }

    }
}