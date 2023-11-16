using Zeta.AgentosCRM.Authorization.Users;
using Zeta.AgentosCRM.TaskManagement;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.TaskManagement.Followers
{
    [Table("TaskFollowers")]
    [Audited]
    public class TaskFollower : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string Name { get; set; }

        public virtual long UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserFk { get; set; }

        public virtual long CRMTaskId { get; set; }

        [ForeignKey("CRMTaskId")]
        public CRMTask CRMTaskFk { get; set; }

    }
}