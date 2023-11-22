using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.CRMSetup.Email
{
    [Table("EmailTemplates")]
    public class EmailTemplate : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(EmailTemplateConsts.MaxTitleLength, MinimumLength = EmailTemplateConsts.MinTitleLength)]
        public virtual string Title { get; set; }

        public virtual string EmailSubject { get; set; }

        public virtual string EmailBody { get; set; }

    }
}