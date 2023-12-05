using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class GetAllWorkflowStepDocumentCheckListsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string DescriptionFilter { get; set; }

        public int? IsForAllPartnersFilter { get; set; }

        public int? IsForAllProductsFilter { get; set; }

        public int? AllowOnClientPortalFilter { get; set; }

        public string WorkflowStepNameFilter { get; set; }

        public string DocumentTypeNameFilter { get; set; }

        public int? WorkflowIdFilter { get; set; }
    }
}