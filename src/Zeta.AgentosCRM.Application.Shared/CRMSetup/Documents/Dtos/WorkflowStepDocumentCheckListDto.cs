using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class WorkflowStepDocumentCheckListDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsForAllPartners { get; set; }

        public bool IsForAllProducts { get; set; }

        public bool AllowOnClientPortal { get; set; }

        public bool IsMandatory { get; set; }

        public int WorkflowStepId { get; set; }

        public int DocumentTypeId { get; set; }

    }
}