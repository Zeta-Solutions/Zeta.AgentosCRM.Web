using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMProducts.Requirements.Dtos
{
    public class GetAllProductEnglishRequirementsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public double? MaxListeningFilter { get; set; }
        public double? MinListeningFilter { get; set; }

        public double? MaxReadingFilter { get; set; }
        public double? MinReadingFilter { get; set; }

        public double? MaxWritingFilter { get; set; }
        public double? MinWritingFilter { get; set; }

        public double? MaxSpeakingFilter { get; set; }
        public double? MinSpeakingFilter { get; set; }

        public double? MaxTotalScoreFilter { get; set; }
        public double? MinTotalScoreFilter { get; set; }

        public string ProductNameFilter { get; set; }

        public int? ProductIdFilter { get; set; }
    }
}