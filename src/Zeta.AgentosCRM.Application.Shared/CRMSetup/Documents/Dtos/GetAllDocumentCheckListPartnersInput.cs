using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class GetAllDocumentCheckListPartnersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string PartnerPartnerNameFilter { get; set; }

        public string WorkflowStepDocumentCheckListNameFilter { get; set; }

    }
}