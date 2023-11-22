using Zeta.AgentosCRM.CRMAgent;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMAgent.Contacts
{
    [Table("AgentContacts")]
    [Audited]
    public class AgentContact : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(AgentContactConsts.MaxNameLength, MinimumLength = AgentContactConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual string PhoneNo { get; set; }

        public virtual string PhoneCode { get; set; }

        [Required]
        [StringLength(AgentContactConsts.MaxEmailLength, MinimumLength = AgentContactConsts.MinEmailLength)]
        public virtual string Email { get; set; }

        public virtual bool IsPrimary { get; set; }

        public virtual long AgentId { get; set; }

        [ForeignKey("AgentId")]
        public Agent AgentFk { get; set; }

    }
}