using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class CreateOrEditSubjectAreaDto : EntityDto<int?>
    {

        [Required]
        [StringLength(SubjectConsts.MaxAbbrivationLength, MinimumLength = SubjectConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(SubjectConsts.MaxNameLength, MinimumLength = SubjectConsts.MinNameLength)]
        public string Name { get; set; }

    }
}