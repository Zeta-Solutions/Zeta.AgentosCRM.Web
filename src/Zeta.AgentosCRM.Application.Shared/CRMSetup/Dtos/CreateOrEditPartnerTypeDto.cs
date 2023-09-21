using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class CreateOrEditPartnerTypeDto : EntityDto<int?>
    {

        [Required]
        [StringLength(PartnerTypeConsts.MaxAbbrivationLength, MinimumLength = PartnerTypeConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(PartnerTypeConsts.MaxNameLength, MinimumLength = PartnerTypeConsts.MinNameLength)]
        public string Name { get; set; }

        public int MasterCategoryId { get; set; }

    }
}