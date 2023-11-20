using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;
using Zeta.AgentosCRM.CRMProducts;
using Zeta.AgentosCRM.CRMClient.Quotation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMClient.Qoutation
{
    [Table("ClientQuotationDetails")]
    [Audited]
    public class ClientQuotationDetail : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        public virtual string Description { get; set; }

        public virtual decimal ServiceFee { get; set; }

        public virtual decimal Discount { get; set; }

        public virtual decimal NetFee { get; set; }

        public virtual decimal ExchangeRate { get; set; }

        public virtual decimal TotalAmount { get; set; }

        public virtual int WorkflowId { get; set; }

        [ForeignKey("WorkflowId")]
        public Workflow WorkflowFk { get; set; }

        public virtual long PartnerId { get; set; }

        [ForeignKey("PartnerId")]
        public Partner PartnerFk { get; set; }

        public virtual long BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch BranchFk { get; set; }

        public virtual long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product ProductFk { get; set; }

        public virtual long QuotationHeadId { get; set; }

        [ForeignKey("QuotationHeadId")]
        public ClientQuotationHead QuotationHeadFk { get; set; }

    }
}