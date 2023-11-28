using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class CreateOrEditDocumentCheckListProductDto : EntityDto<int?>
    {

        public string Name { get; set; }

        public long ProductId { get; set; }

        public int WorkflowStepDocumentCheckListId { get; set; }

    }
}