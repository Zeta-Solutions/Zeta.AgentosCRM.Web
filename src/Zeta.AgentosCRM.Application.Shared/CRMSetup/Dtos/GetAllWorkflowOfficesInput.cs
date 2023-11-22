using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class GetAllWorkflowOfficesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string OrganizationUnitDisplayNameFilter { get; set; }

        public string WorkflowNameFilter { get; set; }

    }
}