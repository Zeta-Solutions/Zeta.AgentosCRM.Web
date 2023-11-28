using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class GetDocumentCheckListPartnerForEditOutput
    {
        public CreateOrEditDocumentCheckListPartnerDto DocumentCheckListPartner { get; set; }

        public string PartnerPartnerName { get; set; }

        public string WorkflowStepDocumentCheckListName { get; set; }

    }
}