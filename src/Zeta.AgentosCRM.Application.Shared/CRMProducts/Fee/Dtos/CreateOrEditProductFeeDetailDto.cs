using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMProducts.Fee.Dtos
{
    public class CreateOrEditProductFeeDetailDto : EntityDto<long?>
    {

        public decimal InstallmentAmount { get; set; }

        public int Installments { get; set; }

        public decimal TotalFee { get; set; }

        public string ClaimTerms { get; set; }

        public double CommissionPer { get; set; }

        public bool IsPayable { get; set; }

        public bool AddInQuotation { get; set; }

        public int FeeTypeId { get; set; }

        public int ProductFeeId { get; set; }

    }
}