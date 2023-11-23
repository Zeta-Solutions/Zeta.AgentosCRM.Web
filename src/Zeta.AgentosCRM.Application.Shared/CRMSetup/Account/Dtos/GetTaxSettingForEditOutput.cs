using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class GetTaxSettingForEditOutput
    {
        public CreateOrEditTaxSettingDto TaxSetting { get; set; }

        public string OrganizationUnitDisplayName { get; set; }

    }
}