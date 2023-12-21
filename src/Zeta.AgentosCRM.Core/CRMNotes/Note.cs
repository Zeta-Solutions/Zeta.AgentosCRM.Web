using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMPartner;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Zeta.AgentosCRM.CRMAgent;
using Zeta.AgentosCRM.CRMApplications;
using Zeta.AgentosCRM.CRMApplications.Stages;

namespace Zeta.AgentosCRM.CRMNotes
{
    [Table("Notes")]
    [Audited]
    public class Note : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(NoteConsts.MaxTitleLength, MinimumLength = NoteConsts.MinTitleLength)]
        public virtual string Title { get; set; }

        [Required]
        [StringLength(NoteConsts.MaxDescriptionLength, MinimumLength = NoteConsts.MinDescriptionLength)]
        public virtual string Description { get; set; }

        public virtual long? ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

        public virtual long? PartnerId { get; set; }

        [ForeignKey("PartnerId")]
        public Partner PartnerFk { get; set; }

        public virtual long? AgentId { get; set; }

        [ForeignKey("AgentId")]
        public Agent AgentFk { get; set; }

        public virtual long? ApplicationId { get; set; }

        [ForeignKey("ApplicationId")]
        public Application ApplicationFk { get; set; }

        public virtual long? ApplicationStageId { get; set; }

        [ForeignKey("ApplicationStageId")]
        public ApplicationStage ApplicationStageFk { get; set; }

    }
}