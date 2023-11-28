using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Documents;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.Documents
{
    [Table("WorkflowStepDocumentCheckLists")]
    public class WorkflowStepDocumentCheckList : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Description { get; set; }

        public virtual bool IsForAllPartners { get; set; }

        public virtual bool IsForAllProducts { get; set; }

        public virtual bool AllowOnClientPortal { get; set; }

        public virtual int WorkflowStepId { get; set; }

        [ForeignKey("WorkflowStepId")]
        public WorkflowStep WorkflowStepFk { get; set; }

        public virtual int DocumentTypeId { get; set; }

        [ForeignKey("DocumentTypeId")]
        public DocumentType DocumentTypeFk { get; set; }

    }
}