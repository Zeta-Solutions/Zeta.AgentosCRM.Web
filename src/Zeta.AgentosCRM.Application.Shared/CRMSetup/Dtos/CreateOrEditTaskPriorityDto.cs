using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class CreateOrEditTaskPriorityDto : EntityDto<int?>
    {

        [Required]
        [StringLength(TaskPriorityConsts.MaxAbbrivationLength, MinimumLength = TaskPriorityConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(TaskPriorityConsts.MaxNameLength, MinimumLength = TaskPriorityConsts.MinNameLength)]
        public string Name { get; set; }

    }
}