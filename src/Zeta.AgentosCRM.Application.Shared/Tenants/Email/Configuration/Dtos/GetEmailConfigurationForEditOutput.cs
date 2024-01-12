using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.Tenants.Email.Configuration.Dtos
{
    public class GetEmailConfigurationForEditOutput
    {
        public CreateOrEditEmailConfigurationDto EmailConfiguration { get; set; }

    }
}