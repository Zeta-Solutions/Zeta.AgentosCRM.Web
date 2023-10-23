using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMSetup.Tag;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMClient
{
    [Table("ClientTags")]
    [Audited]
    public class ClientTag : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual long? ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

        public virtual int TagId { get; set; }

        [ForeignKey("TagId")]
        public Tag TagFk { get; set; }

    }
}