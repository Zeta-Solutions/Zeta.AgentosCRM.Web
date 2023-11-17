using Zeta.AgentosCRM.CRMClient.CheckIn;

using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMClient.CheckIn.Dtos
{
    public class CheckInLogDto : EntityDto<long>
    {
        public string Title { get; set; }

        public string CheckInPurpose { get; set; }

        public CheckInStatus CheckInStatus { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public long ClientId { get; set; }

        public long CheckInAssigneeId { get; set; }

    }
}