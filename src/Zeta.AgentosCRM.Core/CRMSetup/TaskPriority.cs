using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup
{
    [Table("TaskPriorities")]
    public class TaskPriority : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(TaskPriorityConsts.MaxAbbrivationLength, MinimumLength = TaskPriorityConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(TaskPriorityConsts.MaxNameLength, MinimumLength = TaskPriorityConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}