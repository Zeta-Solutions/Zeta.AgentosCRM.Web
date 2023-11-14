using Zeta.AgentosCRM.CRMSetup.TaskCategory;
using Zeta.AgentosCRM.Authorization.Users;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMApplications;
using Zeta.AgentosCRM.CRMApplications.Stages;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.TaskManagement
{
    [Table("CRMTasks")]
    [Audited]
    public class CRMTask : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(CRMTaskConsts.MaxTitleLength, MinimumLength = CRMTaskConsts.MinTitleLength)]
        public virtual string Title { get; set; }

        public virtual DateTime DueDate { get; set; }

        public virtual DateTime DueTime { get; set; }

        [Required]
        [StringLength(CRMTaskConsts.MaxDescriptionLength, MinimumLength = CRMTaskConsts.MinDescriptionLength)]
        public virtual string Description { get; set; }
        //File

        public virtual Guid? Attachment { get; set; } //File, (BinaryObjectId)

        public virtual int RelatedTo { get; set; }

        public virtual int InternalId { get; set; }

        public virtual int TaskCategoryId { get; set; }

        [ForeignKey("TaskCategoryId")]
        public TaskCategory TaskCategoryFk { get; set; }

        public virtual long AssigneeId { get; set; }

        [ForeignKey("AssigneeId")]
        public User AssigneeFk { get; set; }

        public virtual int TaskPriorityId { get; set; }

        [ForeignKey("TaskPriorityId")]
        public TaskPriority TaskPriorityFk { get; set; }

        public virtual long? ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

        public virtual long? PartnerId { get; set; }

        [ForeignKey("PartnerId")]
        public Partner PartnerFk { get; set; }

        public virtual long? ApplicationId { get; set; }

        [ForeignKey("ApplicationId")]
        public Application ApplicationFk { get; set; }

        public virtual long? ApplicationStageId { get; set; }

        [ForeignKey("ApplicationStageId")]
        public ApplicationStage ApplicationStageFk { get; set; }

    }
}