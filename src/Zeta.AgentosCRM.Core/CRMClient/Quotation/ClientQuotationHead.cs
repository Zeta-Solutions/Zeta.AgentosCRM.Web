using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMSetup.CRMCurrency;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMClient.Quotation
{
    [Table("ClientQuotationHeads")]
    [Audited]
    public class ClientQuotationHead : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual DateTime DueDate { get; set; }

        public virtual string ClientEmail { get; set; }

        public virtual string ClientName { get; set; }

        public virtual decimal TotalAmount { get; set; }

        public virtual int ProductCount { get; set; }

        public virtual long ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

        public virtual int CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public CRMCurrency CurrencyFk { get; set; }

    }
}