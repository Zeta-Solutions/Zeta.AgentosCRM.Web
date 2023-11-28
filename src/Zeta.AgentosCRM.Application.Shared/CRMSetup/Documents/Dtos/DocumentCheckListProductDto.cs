using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class DocumentCheckListProductDto : EntityDto
    {
        public string Name { get; set; }

        public long ProductId { get; set; }

        public int WorkflowStepDocumentCheckListId { get; set; }

    }
}