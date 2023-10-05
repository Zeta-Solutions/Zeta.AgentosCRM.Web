using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Regions.Dtos
{
    public class CreateOrEditRegionDto : EntityDto<int?>
    {

        [Required]
        [StringLength(RegionConsts.MaxAbbrivationLength, MinimumLength = RegionConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(RegionConsts.MaxNameLength, MinimumLength = RegionConsts.MinNameLength)]
        public string Name { get; set; }

    }
}