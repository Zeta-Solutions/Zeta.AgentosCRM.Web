using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Education.Dtos
{
    public class CreateOrEditOtherTestScoreDto : EntityDto<int?>
    {

        [Required]
        [StringLength(OtherTestScoreConsts.MaxNameLength, MinimumLength = OtherTestScoreConsts.MinNameLength)]
        public string Name { get; set; }

        public double TotalScore { get; set; }

        public long ClientId { get; set; }

    }
}