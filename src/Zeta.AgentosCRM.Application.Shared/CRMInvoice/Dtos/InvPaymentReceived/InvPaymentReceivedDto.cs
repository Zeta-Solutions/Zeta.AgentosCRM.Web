using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos.InvPaymentReceived
{
    public class InvPaymentReceivedDto : EntityDto<long>
    {
        public int TenantId { get; set; }
        public  long? InvoiceHeadId { get; set; }
        public  long? PaymentsReceived { get; set; }
        public  DateTime? PaymentsReceivedDate { get; set; }
        public  bool? MarkInvoicePaid { get; set; }
        public  int? PaymentMethodId { get; set; }
        public  string AddNotes { get; set; }
        public  Guid? AttachmentId { get; set; }
    }
}
