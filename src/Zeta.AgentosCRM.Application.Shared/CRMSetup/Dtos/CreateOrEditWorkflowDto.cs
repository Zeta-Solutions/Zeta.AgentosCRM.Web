using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class CreateOrEditWorkflowDto : EntityDto<int?>
    {

        [Required]
        [StringLength(WorkflowConsts.MaxNameLength, MinimumLength = WorkflowConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        public List<CreateOrEditWorkflowStepDto> Steps { get; set; }
        public List<CreateOrEditWorkflowOfficeDto> OfficeSteps { get; set; }

        public bool IsForAllOffices { get; set; }

    }
}