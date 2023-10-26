using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMClient
{
    [Table("Followers")]
    [Audited]
    public class Follower : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual long ClientId { get; set; } 

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

        public virtual long UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserFk { get; set; }

    }
}