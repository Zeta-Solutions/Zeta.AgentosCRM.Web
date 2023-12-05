using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class GetWorkflowForEditOutput
    {
        public CreateOrEditWorkflowDto Workflow { get; set; } 
        public List<CreateOrEditWorkflowStepDto> WorkflowStep { get; set; }
        public List<CreateOrEditWorkflowOfficeDto> WorkflowOffice { get; set; }

    }
}