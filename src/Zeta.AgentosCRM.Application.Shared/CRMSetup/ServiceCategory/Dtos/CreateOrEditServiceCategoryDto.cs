using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.ServiceCategory.Dtos
{
    public class CreateOrEditServiceCategoryDto : EntityDto<int?>
    {

        [Required]
        [StringLength(ServiceCategoryConsts.MaxAbbrivationLength, MinimumLength = ServiceCategoryConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(ServiceCategoryConsts.MaxNameLength, MinimumLength = ServiceCategoryConsts.MinNameLength)]
        public string Name { get; set; }

    }
}