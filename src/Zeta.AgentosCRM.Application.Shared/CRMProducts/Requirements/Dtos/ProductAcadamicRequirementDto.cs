using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMProducts.Requirements.Dtos
{
    public class ProductAcadamicRequirementDto : EntityDto
    {
        public string Title { get; set; }

        public double AcadamicScore { get; set; }

        public bool IsGPA { get; set; }

        public int DegreeLevelId { get; set; }

        public long ProductId { get; set; }

    }
}