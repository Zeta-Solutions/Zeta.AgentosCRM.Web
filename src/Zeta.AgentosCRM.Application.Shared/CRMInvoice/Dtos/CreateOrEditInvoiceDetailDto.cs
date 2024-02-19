using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos
{
    public class CreateOrEditInvoiceDetailDto : EntityDto<long?>
    {
        public int TenantId { get; set; }
        public string Description { get; set; }
        public decimal? TotalFee { get; set; }
        public decimal? CommissionPercent { get; set; }
        public decimal? CommissionAmount { get; set; }
        public int? Tax { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public int? IncomeType { get; set; }
        public decimal? Amount { get; set; }
        public long InvoiceHeadId { get; set; }
    }
}
