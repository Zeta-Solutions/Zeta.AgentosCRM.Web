using Zeta.AgentosCRM.CRMClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.CRMClient.Documents
{
    [Table("ClientAttachments")]
    [Audited]
    public class ClientAttachment : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string Name { get; set; }
        //File

        public virtual Guid? AttachmentId { get; set; } //File, (BinaryObjectId)

        public virtual long? ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

    }
}