using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMProducts.Fee.Dtos
{
    public class CreateOrEditProductFeeDto : EntityDto<int?>
    {

        [Required]
        [StringLength(ProductFeeConsts.MaxNameLength, MinimumLength = ProductFeeConsts.MinNameLength)]
        public string Name { get; set; }

        public string ClaimTerms { get; set; }

        public decimal CommissionPer { get; set; }

        public decimal NetTotal { get; set; }

        public int CountryId { get; set; }

        public int? InstallmentTypeId { get; set; }

        public List<CreateOrEditProductFeeDetailDto> FeeDetails { get; set; }

    }
}