using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMProducts.Fee.Dtos
{
    public class ProductFeeDto : EntityDto
    {
        public string Name { get; set; }

        public string ClaimTerms { get; set; }

        public decimal CommissionPer { get; set; }

        public decimal NetTotal { get; set; }

        public int CountryId { get; set; }

        public int? InstallmentTypeId { get; set; }

    }
}