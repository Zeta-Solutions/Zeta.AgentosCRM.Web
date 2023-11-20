using Zeta.AgentosCRM.CRMSetup;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup
{
    [Table("WorkflowSteps")]
    public class WorkflowStep : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Range(WorkflowStepConsts.MinSrlNoValue, WorkflowStepConsts.MaxSrlNoValue)]
        public virtual int SrlNo { get; set; }

        [Required]
        [StringLength(WorkflowStepConsts.MaxAbbrivationLength, MinimumLength = WorkflowStepConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(WorkflowStepConsts.MaxNameLength, MinimumLength = WorkflowStepConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual int WorkflowId { get; set; }

        [ForeignKey("WorkflowId")]
        public Workflow WorkflowFk { get; set; }

        public virtual bool IsPartnerClientIdRequired { get; set; }

        public virtual bool IsStartEndDateRequired { get; set; }

        public virtual bool IsNoteRequired { get; set; }

        public virtual bool IsApplicationIntakeRequired { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual bool IsWinStage { get; set; }

    }
}