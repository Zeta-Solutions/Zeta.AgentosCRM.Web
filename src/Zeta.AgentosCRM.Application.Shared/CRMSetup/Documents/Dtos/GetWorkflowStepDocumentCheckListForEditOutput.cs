using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class GetWorkflowStepDocumentCheckListForEditOutput
    {
        public CreateOrEditWorkflowStepDocumentCheckListDto WorkflowStepDocumentCheckList { get; set; }

        public string WorkflowStepName { get; set; }

        public string DocumentTypeName { get; set; }

    }
}