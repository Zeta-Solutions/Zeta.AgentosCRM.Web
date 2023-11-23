using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Account.Dtos
{
    public class CreateOrEditTaxSettingDto : EntityDto<int?>
    {

        [Required]
        [StringLength(TaxSettingConsts.MaxTaxCodeLength, MinimumLength = TaxSettingConsts.MinTaxCodeLength)]
        public string TaxCode { get; set; }

        public decimal TaxRate { get; set; }

        public bool IsDefault { get; set; }

        public long OrganizationUnitId { get; set; }

    }
}