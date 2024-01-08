using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.Tenants.Email.Dtos
{
    public class SentEmailDto : EntityDto<long>
    {
        public string Title { get; set; }

        public string Subject { get; set; }

        public string FromEmail { get; set; }

        public string ToEmail { get; set; }

        public string CCEmail { get; set; }

        public string BCCEmail { get; set; }

        public string EmailBody { get; set; }

        public bool IsSent { get; set; }

        public int? EmailTemplateId { get; set; }

        public long? EmailConfigurationId { get; set; }

        public long? ClientId { get; set; }

        public long? ApplicationId { get; set; }

    }
}