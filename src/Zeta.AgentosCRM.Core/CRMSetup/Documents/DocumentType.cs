using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.Documents
{
    [Table("DocumentTypes")]
    public class DocumentType : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [StringLength(DocumentTypeConsts.MaxAbbrivationLength, MinimumLength = DocumentTypeConsts.MinAbbrivationLength)]
        public virtual string Abbrivation { get; set; }

        [Required]
        [StringLength(DocumentTypeConsts.MaxNameLength, MinimumLength = DocumentTypeConsts.MinNameLength)]
        public virtual string Name { get; set; }

    }
}