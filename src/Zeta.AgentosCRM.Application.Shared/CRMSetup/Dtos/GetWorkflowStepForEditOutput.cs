using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class GetWorkflowStepForEditOutput
    {
        public CreateOrEditWorkflowStepDto WorkflowStep { get; set; }

        public string WorkflowName { get; set; }

    }
}