using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMApplications.Stages.Dtos
{
    public class CreateOrEditApplicationStageDto : EntityDto<long?>
    {

        [Required]
        [StringLength(ApplicationStageConsts.MaxNameLength, MinimumLength = ApplicationStageConsts.MinNameLength)]
        public string Name { get; set; }

        public long ApplicationId { get; set; }

        public int WorkflowStepId { get; set; }

        public bool IsCurrent { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsActive { get; set; }

    }
}