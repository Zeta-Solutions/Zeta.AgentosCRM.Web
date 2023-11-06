using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.CRMCurrency.Dtos
{
    public class CreateOrEditCRMCurrencyDto : EntityDto<int?>
    {

        [Required]
        [StringLength(CRMCurrencyConsts.MaxAbbrivationLength, MinimumLength = CRMCurrencyConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(CRMCurrencyConsts.MaxNameLength, MinimumLength = CRMCurrencyConsts.MinNameLength)]
        public string Name { get; set; }

    }
}