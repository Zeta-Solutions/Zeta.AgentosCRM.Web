using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class CreateOrEditWorkflowStepDocumentCheckListDto : EntityDto<int?>
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsForAllPartners { get; set; }

        public bool IsForAllProducts { get; set; }

        public bool AllowOnClientPortal { get; set; }

        public int WorkflowStepId { get; set; }

        public int DocumentTypeId { get; set; }

    }
}