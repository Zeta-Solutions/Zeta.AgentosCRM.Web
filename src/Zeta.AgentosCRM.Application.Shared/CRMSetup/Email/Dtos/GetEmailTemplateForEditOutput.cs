using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Email.Dtos
{
    public class GetEmailTemplateForEditOutput
    {
        public CreateOrEditEmailTemplateDto EmailTemplate { get; set; }

    }
}