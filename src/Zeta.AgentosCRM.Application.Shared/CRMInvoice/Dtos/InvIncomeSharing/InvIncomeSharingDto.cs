using Abp.Application.Services.Dto;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos.InvIncomeSharing
{
    public class InvIncomeSharingDto : EntityDto<long>
    {
        public int TenantId { get; set; }
        public  long? InvoiceHeadId { get; set; }
        public  long? PaymentsReceivedId { get; set; }
        public  decimal? IncomeSharing { get; set; }
        public  bool? IsTax { get; set; }
        public  int? Tax { get; set; }
        public  decimal? TaxAmount { get; set; }
        public  decimal? TotalIncludingTax { get; set; }
        public  long? OrganizationUnitId { get; set; }
    }
}
