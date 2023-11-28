using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class CreateOrEditDocumentCheckListPartnerDto : EntityDto<int?>
    {

        public string Name { get; set; }

        public long PartnerId { get; set; }

        public int WorkflowStepDocumentCheckListId { get; set; }

    }
}