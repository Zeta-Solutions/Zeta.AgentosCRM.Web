using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class GetWorkflowStepDocumentCheckListForEditOutput
    {
        public CreateOrEditWorkflowStepDocumentCheckListDto WorkflowStepDocumentCheckList { get; set; }

        public string WorkflowStepName { get; set; }

        public string DocumentTypeName { get; set; }
        public List<CreateOrEditDocumentCheckListPartnerDto> DocumentCheckListPartner  { get; set; }
        public List<CreateOrEditDocumentCheckListProductDto> DocumentCheckListProduct  { get; set; }

    }
}