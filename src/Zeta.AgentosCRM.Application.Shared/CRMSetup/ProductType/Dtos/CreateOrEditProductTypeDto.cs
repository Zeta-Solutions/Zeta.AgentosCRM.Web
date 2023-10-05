using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.ProductType.Dtos
{
    public class CreateOrEditProductTypeDto : EntityDto<int?>
    {

        [Required]
        [StringLength(ProductTypeConsts.MaxAbbrivationLength, MinimumLength = ProductTypeConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(ProductTypeConsts.MaxNameLength, MinimumLength = ProductTypeConsts.MinNameLength)]
        public string Name { get; set; }

        public int MasterCategoryId { get; set; }

    }
}