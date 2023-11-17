using Zeta.AgentosCRM.CRMClient.CheckIn;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.CheckIn.Dtos
{
    public class CreateOrEditCheckInLogDto : EntityDto<long?>
    {

        [Required]
        [StringLength(CheckInLogConsts.MaxTitleLength, MinimumLength = CheckInLogConsts.MinTitleLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(CheckInLogConsts.MaxCheckInPurposeLength, MinimumLength = CheckInLogConsts.MinCheckInPurposeLength)]
        public string CheckInPurpose { get; set; }

        public CheckInStatus CheckInStatus { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public long ClientId { get; set; }

        public long CheckInAssigneeId { get; set; }

    }
}