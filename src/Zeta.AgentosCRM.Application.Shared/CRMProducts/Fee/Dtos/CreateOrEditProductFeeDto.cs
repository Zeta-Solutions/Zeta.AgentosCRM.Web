using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMProducts.Fee.Dtos
{
    public class CreateOrEditProductFeeDto : EntityDto<int?>
    {

        [Required]
        [StringLength(ProductFeeConsts.MaxNameLength, MinimumLength = ProductFeeConsts.MinNameLength)]
        public string Name { get; set; }

        public int Installments { get; set; }

        public decimal InstallmentAmount { get; set; }

        public decimal TotalFee { get; set; }

        public string ClaimTerms { get; set; }

        public decimal CommissionPer { get; set; }

        public bool AddInQuotation { get; set; }

        public decimal NetTotal { get; set; }

        public int CountryId { get; set; }

        public int? InstallmentTypeId { get; set; }

        public int? FeeTypeId { get; set; }

        public long ProductId { get; set; }

    }
}