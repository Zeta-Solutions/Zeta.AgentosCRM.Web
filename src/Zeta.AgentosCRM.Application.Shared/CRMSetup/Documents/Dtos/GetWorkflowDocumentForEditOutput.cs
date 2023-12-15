using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class GetWorkflowDocumentForEditOutput
    {
        public CreateOrEditWorkflowDocumentDto WorkflowDocument { get; set; }

        public string WorkflowName { get; set; }

    }
}