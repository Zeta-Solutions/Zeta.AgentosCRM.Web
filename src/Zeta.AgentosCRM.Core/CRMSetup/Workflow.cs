using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup
{
    [Table("Workflows")]
    public class Workflow : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(WorkflowConsts.MaxNameLength, MinimumLength = WorkflowConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual bool IsForAllOffices { get; set; }

    }
}