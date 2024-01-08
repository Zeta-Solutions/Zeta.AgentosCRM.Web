using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.Tenants.Email.Dtos
{
    public class GetSentEmailForEditOutput
    {
        public CreateOrEditSentEmailDto SentEmail { get; set; }

        public string EmailTemplateTitle { get; set; }

        public string EmailConfigurationName { get; set; }

        public string ClientFirstName { get; set; }

        public string ApplicationName { get; set; }

    }
}