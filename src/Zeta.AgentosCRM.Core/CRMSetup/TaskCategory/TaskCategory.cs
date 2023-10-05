using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.TaskCategory
{
    [Table("TaskCategories")]
    public class TaskCategory : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(TaskCategoryConsts.MaxAbbrivationLength, MinimumLength = TaskCategoryConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(TaskCategoryConsts.MaxNameLength, MinimumLength = TaskCategoryConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}