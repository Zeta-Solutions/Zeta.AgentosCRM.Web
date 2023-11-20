using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Email.Dtos
{
    public class EmailTemplateDto : EntityDto
    {
        public string Title { get; set; }

        public string EmailSubject { get; set; }

        public string EmailBody { get; set; }

    }
}