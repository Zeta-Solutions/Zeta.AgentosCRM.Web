using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.FeeType.Dtos
{
    public class CreateOrEditFeeTypeDto : EntityDto<int?>
    {

        [Required]
        [StringLength(FeeTypeConsts.MaxAbbrivationLength, MinimumLength = FeeTypeConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(FeeTypeConsts.MaxNameLength, MinimumLength = FeeTypeConsts.MinNameLength)]
        public string Name { get; set; }

    }
}