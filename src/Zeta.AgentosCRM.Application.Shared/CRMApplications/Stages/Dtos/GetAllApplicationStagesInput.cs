using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMApplications.Stages.Dtos
{
    public class GetAllApplicationStagesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string ApplicationNameFilter { get; set; }

    }
}