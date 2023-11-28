using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class DocumentCheckListPartnerDto : EntityDto
    {
        public string Name { get; set; }

        public long PartnerId { get; set; }

        public int WorkflowStepDocumentCheckListId { get; set; }

    }
}