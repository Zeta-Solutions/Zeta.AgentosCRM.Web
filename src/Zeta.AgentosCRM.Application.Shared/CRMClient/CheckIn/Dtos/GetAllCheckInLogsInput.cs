using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMClient.CheckIn.Dtos
{
    public class GetAllCheckInLogsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string TitleFilter { get; set; }

        public string CheckInPurposeFilter { get; set; }

        public int? CheckInStatusFilter { get; set; }

        public DateTime? MaxCheckInDateFilter { get; set; }
        public DateTime? MinCheckInDateFilter { get; set; }

        public DateTime? MaxStartTimeFilter { get; set; }
        public DateTime? MinStartTimeFilter { get; set; }

        public DateTime? MaxEndTimeFilter { get; set; }
        public DateTime? MinEndTimeFilter { get; set; }

        public string ClientDisplayPropertyFilter { get; set; }

        public string UserNameFilter { get; set; }
        public long? ClientIdFilter { get; set; }

    }
}