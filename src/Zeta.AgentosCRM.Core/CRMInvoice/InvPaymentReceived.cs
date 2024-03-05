using Abp.Auditing;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Zeta.AgentosCRM.CRMAgent;

namespace Zeta.AgentosCRM.CRMInvoice
{
    [Table("InvPaymentReceived")]
    [Audited]
    public class InvPaymentReceived : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public virtual long? InvoiceHeadId { get; set; }
        public virtual long? PaymentsReceived { get; set; }
        public virtual DateTime? PaymentsReceivedDate { get; set; }
        public virtual bool? MarkInvoicePaid { get; set; }
        public virtual int? PaymentMethodId { get; set; }
        [Required]
        [StringLength(InvPaymentReceivedConst.MaxNoteLength, MinimumLength = InvPaymentReceivedConst.MinNoteLength)]
        public virtual string AddNotes { get; set; }
        public virtual Guid? AttachmentId { get; set; }
    }
}
