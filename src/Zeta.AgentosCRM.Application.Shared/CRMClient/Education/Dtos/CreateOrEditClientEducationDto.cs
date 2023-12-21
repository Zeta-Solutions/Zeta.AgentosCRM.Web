using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMClient.Education.Dtos
{
    public class CreateOrEditClientEducationDto : EntityDto<long?>
    {

        [Required]
        [StringLength(ClientEducationConsts.MaxDegreeTitleLength, MinimumLength = ClientEducationConsts.MinDegreeTitleLength)]
        public string DegreeTitle { get; set; }

        [Required]
        [StringLength(ClientEducationConsts.MaxInstitutionLength, MinimumLength = ClientEducationConsts.MinInstitutionLength)]
        public string Institution { get; set; }

        public DateTime CourseStartDate { get; set; }

        public DateTime CourseEndDate { get; set; }

        public double AcadmicScore { get; set; }

        public int DegreeLevelId { get; set; }

        public int SubjectId { get; set; }

        public int SubjectAreaId { get; set; }

        public long ClientId { get; set; }

        public bool IsGpa { get; set; }

    }
}