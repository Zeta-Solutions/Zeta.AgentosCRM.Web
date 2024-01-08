using Zeta.AgentosCRM.CRMSetup.Email;
using Zeta.AgentosCRM.Tenants.Email.Configuration;
using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMApplications;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Zeta.AgentosCRM.Tenants.Email
{
    [Table("SentEmails")]
    [Audited]
    public class SentEmail : CreationAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string Title { get; set; }

        public virtual string Subject { get; set; }

        public virtual string FromEmail { get; set; }

        public virtual string ToEmail { get; set; }

        public virtual string CCEmail { get; set; }

        public virtual string BCCEmail { get; set; }

        public virtual string EmailBody { get; set; }

        public virtual bool IsSent { get; set; }

        public virtual int? EmailTemplateId { get; set; }

        [ForeignKey("EmailTemplateId")]
        public EmailTemplate EmailTemplateFk { get; set; }

        public virtual long? EmailConfigurationId { get; set; }

        [ForeignKey("EmailConfigurationId")]
        public EmailConfiguration EmailConfigurationFk { get; set; }

        public virtual long? ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client ClientFk { get; set; }

        public virtual long? ApplicationId { get; set; }

        [ForeignKey("ApplicationId")]
        public Application ApplicationFk { get; set; }

    }
}