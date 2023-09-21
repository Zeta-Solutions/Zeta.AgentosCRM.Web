using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class CreateOrEditWorkflowStepDto : EntityDto<int?>
    {

        [Range(WorkflowStepConsts.MinSrlNoValue, WorkflowStepConsts.MaxSrlNoValue)]
        public int SrlNo { get; set; }

        [Required]
        [StringLength(WorkflowStepConsts.MaxAbbrivationLength, MinimumLength = WorkflowStepConsts.MinAbbrivationLength)]
        public string Abbrivation { get; set; }

        [Required]
        [StringLength(WorkflowStepConsts.MaxNameLength, MinimumLength = WorkflowStepConsts.MinNameLength)]
        public string Name { get; set; }

        public int WorkflowId { get; set; }

    }
}