using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Education.Dtos
{
    public class CreateOrEditEnglisTestScoreDto : EntityDto<long?>
    {

        [Required]
        [StringLength(EnglisTestScoreConsts.MaxNameLength, MinimumLength = EnglisTestScoreConsts.MinNameLength)]
        public string Name { get; set; }

        public double Listenting { get; set; }

        public double Reading { get; set; }

        public double Writing { get; set; }

        public double Speaking { get; set; }

        public double TotalScore { get; set; }

        public long ClientId { get; set; }

    }
}