﻿using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMProducts.Fee.Dtos
{
    public class GetProductFeeForEditOutput
    {
        public CreateOrEditProductFeeDto ProductFee { get; set; }

        public string CountryName { get; set; }

        public string InstallmentTypeName { get; set; }

    }
}