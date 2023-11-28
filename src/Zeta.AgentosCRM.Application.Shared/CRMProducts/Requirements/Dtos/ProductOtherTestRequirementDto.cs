using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMProducts.Requirements.Dtos
{
    public class ProductOtherTestRequirementDto : EntityDto
    {
        public string Name { get; set; }

        public double TotalScore { get; set; }

        public long ProductId { get; set; }

    }
}