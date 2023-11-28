using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Document.Dtos
{
    public class WorkflowDocumentDto : EntityDto
    {
        public string Name { get; set; }

        public int WorkflowId { get; set; }

    }
}