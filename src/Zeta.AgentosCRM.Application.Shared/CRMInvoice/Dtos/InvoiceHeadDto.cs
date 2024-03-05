using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos
{
    public class InvoiceHeadDto : EntityDto<long>
    {
        public int TenantId { get; set; }
        public long? ClientId { get; set; }
        public long? PartnerId { get; set; }
        public long? ApplicationId { get; set; }
        public string PartnerName { get; set; }
        public string PartnerAddress { get; set; }
        public string PartnerContact { get; set; }
        public string PartnerService { get; set; }
        public string ClientName { get; set; }
        public DateTime ClientDOB { get; set; }
        public string PartnerClientId { get; set; }
        public string Product { get; set; }
        public string Branch { get; set; }
        public string Workflow { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceDueDate { get; set; }
        public decimal? DiscountGivenToClient { get; set; }
        public decimal? TotalFee { get; set; }
        public decimal? CommissionClaimed { get; set; }
        public decimal? Tax { get; set; }
        public decimal? NetFeePaidToPartner { get; set; }
        public int ManualPaymentDetail { get; set; }

        public decimal? NetFeeReceived { get; set; }
        public decimal? NetIncome { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? TotalPayables { get; set; }
        public decimal? TotalIncome { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalAmountInclTax { get; set; }
        public decimal? TotalPaid { get; set; }
        public decimal? TotalDue { get; set; }
        public string InvoiceNo { get; set; }
        public bool? Status { get; set; }
        public bool? IsInvoiceNetOrGross { get; set; }
        public string ApplicationName { get; set; }
        public string ClientAssignee { get; set; }
        public string ApplicationOwner { get; set; }

        public int? TotalDetailCount { get; set; }
        public  int? InvoiceType { get; set; }
        public  int? InvoiceCreatedDate { get; set; }
        public  DateTime? InvoiceCreatedDateDet { get; set; }
        public  int? TotalRevenue { get; set; }
        public  string ClientEmail { get; set; }
    }
}
