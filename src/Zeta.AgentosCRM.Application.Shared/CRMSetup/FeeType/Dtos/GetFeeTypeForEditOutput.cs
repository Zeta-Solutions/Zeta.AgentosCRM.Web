using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.FeeType.Dtos
{
    public class GetFeeTypeForEditOutput
    {
        public CreateOrEditFeeTypeDto FeeType { get; set; }

    }
}