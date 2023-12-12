using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Document.Dtos
{
    public class GetAllWorkflowDocumentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string WorkflowNameFilter { get; set; }
        public int? WorkflowIdFilter { get; set; }
        public int? ID { get; set; }

    }
}