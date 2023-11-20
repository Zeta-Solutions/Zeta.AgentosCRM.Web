using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMClient.Education.Dtos
{
    public class GetAllEnglisTestScoresInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public double? MaxListentingFilter { get; set; }
        public double? MinListentingFilter { get; set; }

        public double? MaxReadingFilter { get; set; }
        public double? MinReadingFilter { get; set; }

        public double? MaxWritingFilter { get; set; }
        public double? MinWritingFilter { get; set; }

        public double? MaxSpeakingFilter { get; set; }
        public double? MinSpeakingFilter { get; set; }

        public double? MaxTotalScoreFilter { get; set; }
        public double? MinTotalScoreFilter { get; set; }

        public string ClientFirstNameFilter { get; set; }

    }
}