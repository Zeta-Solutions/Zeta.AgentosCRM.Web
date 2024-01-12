using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.Tenants.Email.Configuration.Dtos
{
    public class EmailConfigurationDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string SenderEmail { get; set; }

        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public string SenderPassword { get; set; }

        public string UserName { get; set; }

        public bool IsActive { get; set; }

        public bool IsEnableSsl { get; set; }

        public string Protocol { get; set; }

    }
}