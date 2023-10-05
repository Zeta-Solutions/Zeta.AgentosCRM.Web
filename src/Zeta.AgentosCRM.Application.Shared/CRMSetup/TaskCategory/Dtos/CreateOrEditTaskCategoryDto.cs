using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.TaskCategory.Dtos
{
    public class CreateOrEditTaskCategoryDto : EntityDto<int?>
    {

        [Required]
        [StringLength(TaskCategoryConsts.MaxAbbrivationLength, MinimumLength = TaskCategoryConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(TaskCategoryConsts.MaxNameLength, MinimumLength = TaskCategoryConsts.MinNameLength)]
        public string Name { get; set; }

    }
}