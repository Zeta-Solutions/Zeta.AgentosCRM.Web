using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class WorkflowDocumentDto : EntityDto
    {
        public string Name { get; set; }

        public int WorkflowId { get; set; }

    }
}