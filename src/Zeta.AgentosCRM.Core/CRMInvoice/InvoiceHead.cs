using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using Zeta.AgentosCRM.CRMSetup.CRMCurrency;
using Zeta.AgentosCRM.CRMSetup.Account;
using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMApplications;

namespace Zeta.AgentosCRM.CRMInvoice
{
    [Table("InvoiceHead")]
    [Audited]
    public class InvoiceHead : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public virtual long? ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }
        public virtual long? PartnerId { get; set; }
        [ForeignKey("PartnerId")]
        public Partner PartnerFk { get; set; }
        public virtual long? ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public Application ApplicationFk { get; set; }
        public virtual string PartnerName { get; set; }
        public virtual string PartnerAddress { get; set; }
        public virtual string PartnerContact { get; set; }
        public virtual string PartnerService { get; set; }
        public virtual string ClientName { get; set; }
        public virtual DateTime ClientDOB { get; set; }
        public virtual string PartnerClientId { get; set; }
        public virtual string Product { get; set; }
        public virtual string Branch { get; set; }
        public virtual string Workflow { get; set; }
        public virtual DateTime InvoiceDate { get; set; }
        public virtual DateTime InvoiceDueDate { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual decimal? DiscountGivenToClient { get; set; }
        public virtual decimal? TotalFee { get; set; }
        public virtual decimal? CommissionClaimed { get; set; }
        public virtual decimal? Tax { get; set; }
        public virtual decimal? NetFeePaidToPartner { get; set; }
        public virtual int? ManualPaymentDetail { get; set; }

        [ForeignKey("ManualPaymentDetails")]
        public ManualPaymentDetail ManualPaymentDetailFk { get; set; }
        public virtual decimal? NetFeeReceived { get; set; }
        public virtual decimal? NetIncome { get; set; }
        public virtual int? CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public CRMCurrency CurrencyFk { get; set; }
        public virtual decimal? TotalPayables { get; set; }
        public virtual decimal? TotalIncome { get; set; }
        public virtual decimal? TotalAmount { get; set; }
        public virtual decimal? TotalAmountInclTax { get; set; }
        public virtual decimal? TotalPaid { get; set; }
        public virtual decimal? TotalDue { get; set; }
        public virtual string InvoiceNo { get; set; }
        public virtual bool? Status { get; set; }
        public virtual bool? IsInvoiceNetOrGross { get; set; }
        public virtual string ApplicationName { get; set; }
        public virtual string ClientAssignee { get; set; }
        public virtual string ApplicationOwner { get; set; }
        
        public virtual int? TotalDetailCount { get; set; }
    }
}
