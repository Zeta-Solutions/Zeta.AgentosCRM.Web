using Zeta.AgentosCRM.CRMProducts;
using Zeta.AgentosCRM.CRMSetup.Documents;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.Documents
{
    [Table("DocumentCheckListProducts")]
    public class DocumentCheckListProduct : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string Name { get; set; }

        public virtual long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product ProductFk { get; set; }

        public virtual int WorkflowStepDocumentCheckListId { get; set; }

        [ForeignKey("WorkflowStepDocumentCheckListId")]
        public WorkflowStepDocumentCheckList WorkflowStepDocumentCheckListFk { get; set; }

    }
}