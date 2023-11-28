using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMProducts.Requirements.Dtos
{
    public class CreateOrEditProductEnglishRequirementDto : EntityDto<int?>
    {

        [Required]
        public string Name { get; set; }

        public double Listening { get; set; }

        public double Reading { get; set; }

        public double Writing { get; set; }

        public double Speaking { get; set; }

        public double TotalScore { get; set; }

        public long ProductId { get; set; }

    }
}