using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class GetDocumentCheckListProductForEditOutput
    {
        public CreateOrEditDocumentCheckListProductDto DocumentCheckListProduct { get; set; }

        public string ProductName { get; set; }

        public string WorkflowStepDocumentCheckListName { get; set; }

    }
}