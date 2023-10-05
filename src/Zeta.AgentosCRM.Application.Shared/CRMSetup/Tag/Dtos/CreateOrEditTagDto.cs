using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Tag.Dtos
{
    public class CreateOrEditTagDto : EntityDto<int?>
    {

        [Required]
        [StringLength(TagConsts.MaxAbbrivationLength, MinimumLength = TagConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(TagConsts.MaxNameLength, MinimumLength = TagConsts.MinNameLength)]
        public string Name { get; set; }

    }
}