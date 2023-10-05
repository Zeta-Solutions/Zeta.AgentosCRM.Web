using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.InstallmentType.Dtos
{
    public class CreateOrEditInstallmentTypeDto : EntityDto<int?>
    {

        [Required]
        [StringLength(InstallmentTypeConsts.MaxAbbrivationLength, MinimumLength = InstallmentTypeConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(InstallmentTypeConsts.MaxNameLength, MinimumLength = InstallmentTypeConsts.MinNameLength)]
        public string Name { get; set; }

    }
}