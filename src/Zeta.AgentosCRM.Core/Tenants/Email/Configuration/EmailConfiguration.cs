using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Zeta.AgentosCRM.Tenants.Email.Configuration
{
    [Table("EmailConfigurations")]
    public class EmailConfiguration : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(EmailConfigurationConsts.MaxNameLength, MinimumLength = EmailConfigurationConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(EmailConfigurationConsts.MaxSenderEmailLength, MinimumLength = EmailConfigurationConsts.MinSenderEmailLength)]
        public virtual string SenderEmail { get; set; }

        [Required]
        [StringLength(EmailConfigurationConsts.MaxSmtpServerLength, MinimumLength = EmailConfigurationConsts.MinSmtpServerLength)]
        public virtual string SmtpServer { get; set; }

        public virtual int SmtpPort { get; set; }

        public virtual string SenderPassword { get; set; }

        public virtual string UserName { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual bool IsEnableSsl { get; set; }

        public virtual string Protocol { get; set; }

    }
}