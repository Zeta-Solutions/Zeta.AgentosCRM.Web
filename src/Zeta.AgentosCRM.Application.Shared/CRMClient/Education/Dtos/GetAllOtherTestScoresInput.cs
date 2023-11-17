using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMClient.Education.Dtos
{
    public class GetAllOtherTestScoresInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public double? MaxTotalScoreFilter { get; set; }
        public double? MinTotalScoreFilter { get; set; }

        public string ClientFirstNameFilter { get; set; }

    }
}