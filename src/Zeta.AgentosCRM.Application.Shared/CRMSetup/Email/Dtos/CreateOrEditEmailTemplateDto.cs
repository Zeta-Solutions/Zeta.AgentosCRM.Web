using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Email.Dtos
{
    public class CreateOrEditEmailTemplateDto : EntityDto<int?>
    {

        [Required]
        [StringLength(EmailTemplateConsts.MaxTitleLength, MinimumLength = EmailTemplateConsts.MinTitleLength)]
        public string Title { get; set; }

        public string EmailSubject { get; set; }

        public string EmailBody { get; set; }

    }
}