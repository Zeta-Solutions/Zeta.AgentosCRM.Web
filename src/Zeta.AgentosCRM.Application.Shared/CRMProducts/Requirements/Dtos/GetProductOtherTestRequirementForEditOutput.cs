using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMProducts.Requirements.Dtos
{
    public class GetProductOtherTestRequirementForEditOutput
    {
        public CreateOrEditProductOtherTestRequirementDto ProductOtherTestRequirement { get; set; }

        public string ProductName { get; set; }

    }
}