using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMProducts.Fee.Dtos
{
    public class GetProductFeeDetailForEditOutput
    {
        public CreateOrEditProductFeeDetailDto ProductFeeDetail { get; set; }

        public string FeeTypeName { get; set; }

        public string ProductFeeName { get; set; }

    }
}