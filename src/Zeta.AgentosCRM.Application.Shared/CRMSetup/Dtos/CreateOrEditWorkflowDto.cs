using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class CreateOrEditWorkflowDto : EntityDto<int?>
    {

        [Required]
        [StringLength(WorkflowConsts.MaxNameLength, MinimumLength = WorkflowConsts.MinNameLength)]
        public string Name { get; set; }

    }
}