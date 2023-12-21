using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMProducts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;

namespace Zeta.AgentosCRM.CRMApplications
{
    [Table("Applications")]
    [Audited]
    public class Application : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        public virtual string Name { get; set; }

        public virtual long ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

        public virtual int WorkflowId { get; set; }

        [ForeignKey("WorkflowId")]
        public Workflow WorkflowFk { get; set; }

        public virtual long? PartnerId { get; set; }

        [ForeignKey("PartnerId")]
        public Partner PartnerFk { get; set; }

        public virtual long? BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch BranchFk { get; set; }

        public virtual long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product ProductFk { get; set; }

        public virtual bool IsDiscontinue { get; set;}

    }
}