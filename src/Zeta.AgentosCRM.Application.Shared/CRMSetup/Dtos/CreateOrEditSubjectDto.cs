using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class CreateOrEditSubjectDto : EntityDto<int?>
    {

        [Required]
        [StringLength(SubjectAreaConsts.MaxAbbrivationLength, MinimumLength = SubjectAreaConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(SubjectAreaConsts.MaxNameLength, MinimumLength = SubjectAreaConsts.MinNameLength)]
        public string Name { get; set; }

        public int SubjectAreaId { get; set; }

    }
}