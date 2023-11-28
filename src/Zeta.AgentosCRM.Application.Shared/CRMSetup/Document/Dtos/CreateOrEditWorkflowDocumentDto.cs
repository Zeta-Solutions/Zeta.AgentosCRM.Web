using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Document.Dtos
{
    public class CreateOrEditWorkflowDocumentDto : EntityDto<int?>
    {

        public string Name { get; set; }

        public int WorkflowId { get; set; }

    }
}