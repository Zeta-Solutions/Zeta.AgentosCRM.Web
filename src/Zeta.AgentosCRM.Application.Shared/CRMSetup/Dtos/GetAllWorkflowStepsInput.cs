using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class GetAllWorkflowStepsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int? MaxSrlNoFilter { get; set; }
        public int? MinSrlNoFilter { get; set; }

        public string AbbrivationFilter { get; set; }

        public string NameFilter { get; set; }

        public string WorkflowNameFilter { get; set; }

        public int? WorkflowIdFilter { get; set; }
    }
}