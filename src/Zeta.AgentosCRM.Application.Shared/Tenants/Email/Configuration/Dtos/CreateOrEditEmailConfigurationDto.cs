using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.Tenants.Email.Configuration.Dtos
{
    public class CreateOrEditEmailConfigurationDto : EntityDto<long?>
    {

        [Required]
        [StringLength(EmailConfigurationConsts.MaxNameLength, MinimumLength = EmailConfigurationConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(EmailConfigurationConsts.MaxSenderEmailLength, MinimumLength = EmailConfigurationConsts.MinSenderEmailLength)]
        public string SenderEmail { get; set; }

        [Required]
        [StringLength(EmailConfigurationConsts.MaxSmtpServerLength, MinimumLength = EmailConfigurationConsts.MinSmtpServerLength)]
        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public string SenderPassword { get; set; }

        public string UserName { get; set; }

        public bool IsActive { get; set; }

        public bool IsEnableSsl { get; set; }

        public string Protocol { get; set; }

    }
}