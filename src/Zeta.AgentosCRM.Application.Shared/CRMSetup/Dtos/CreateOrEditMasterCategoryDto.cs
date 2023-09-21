using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class CreateOrEditMasterCategoryDto : EntityDto<int?>
    {

        [Required]
        [StringLength(MasterCategoryConsts.MaxAbbrivationLength, MinimumLength = MasterCategoryConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(MasterCategoryConsts.MaxNameLength, MinimumLength = MasterCategoryConsts.MinNameLength)]
        public string Name { get; set; }

    }
}