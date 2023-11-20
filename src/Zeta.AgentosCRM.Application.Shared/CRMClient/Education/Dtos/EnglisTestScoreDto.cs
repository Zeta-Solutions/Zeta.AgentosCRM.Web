using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.Education.Dtos
{
    public class EnglisTestScoreDto : EntityDto<long>
    {
        public string Name { get; set; }

        public double Listenting { get; set; }

        public double Reading { get; set; }

        public double Writing { get; set; }

        public double Speaking { get; set; }

        public double TotalScore { get; set; }

        public long ClientId { get; set; }

    }
}