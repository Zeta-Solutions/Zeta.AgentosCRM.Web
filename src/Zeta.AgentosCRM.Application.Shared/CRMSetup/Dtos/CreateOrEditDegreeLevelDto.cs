using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class CreateOrEditDegreeLevelDto : EntityDto<int?>
    {

        [Required]
        [StringLength(DegreeLevelConsts.MaxAbbrivationLength, MinimumLength = DegreeLevelConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(DegreeLevelConsts.MaxNameLength, MinimumLength = DegreeLevelConsts.MinNameLength)]
        public string Name { get; set; }

    }
}