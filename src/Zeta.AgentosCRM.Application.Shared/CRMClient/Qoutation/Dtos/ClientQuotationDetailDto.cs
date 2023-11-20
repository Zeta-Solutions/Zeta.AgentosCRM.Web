using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.Qoutation.Dtos
{
    public class ClientQuotationDetailDto : EntityDto<long>
    {
        public string Description { get; set; }

        public decimal ServiceFee { get; set; }

        public decimal Discount { get; set; }

        public decimal NetFee { get; set; }

        public decimal ExchangeRate { get; set; }

        public decimal TotalAmount { get; set; }

        public int WorkflowId { get; set; }

        public long PartnerId { get; set; }

        public long BranchId { get; set; }

        public long ProductId { get; set; }

        public long QuotationHeadId { get; set; }

    }
}