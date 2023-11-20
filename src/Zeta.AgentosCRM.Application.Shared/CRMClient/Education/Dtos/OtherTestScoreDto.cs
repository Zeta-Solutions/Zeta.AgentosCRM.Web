using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.Education.Dtos
{
    public class OtherTestScoreDto : EntityDto
    {
        public string Name { get; set; }

        public double TotalScore { get; set; }

        public long ClientId { get; set; }

    }
}