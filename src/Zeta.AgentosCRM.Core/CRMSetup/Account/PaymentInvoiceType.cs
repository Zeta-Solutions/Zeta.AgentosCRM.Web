using Zeta.AgentosCRM.CRMSetup.Account;
using Zeta.AgentosCRM.CRMSetup.Account;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.Account
{
    [Table("PaymentInvoiceTypes")]
    public class PaymentInvoiceType : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string Name { get; set; }

        public virtual int? InvoiceTypeId { get; set; }

        [ForeignKey("InvoiceTypeId")]
        public InvoiceType InvoiceTypeFk { get; set; }

        public virtual int? ManualPaymentDetailId { get; set; }

        [ForeignKey("ManualPaymentDetailId")]
        public ManualPaymentDetail ManualPaymentDetailFk { get; set; }

    }
}