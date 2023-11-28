using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMProducts.Requirements.Dtos
{
    public class GetProductAcadamicRequirementForEditOutput
    {
        public CreateOrEditProductAcadamicRequirementDto ProductAcadamicRequirement { get; set; }

        public string DegreeLevelName { get; set; }

        public string ProductName { get; set; }

    }
}