using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMClient.Education.Dtos
{
    public class GetAllClientEducationsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string DegreeTitleFilter { get; set; }

        public string InstitutionFilter { get; set; }

        public DateTime? MaxCourseStartDateFilter { get; set; }
        public DateTime? MinCourseStartDateFilter { get; set; }

        public DateTime? MaxCourseEndDateFilter { get; set; }
        public DateTime? MinCourseEndDateFilter { get; set; }

        public double? MaxAcadmicScoreFilter { get; set; }
        public double? MinAcadmicScoreFilter { get; set; }

        public string DegreeLevelNameFilter { get; set; }

        public string SubjectNameFilter { get; set; }

        public string SubjectAreaNameFilter { get; set; }
        public long? ClientIdFilter { get; set; }

    }
}