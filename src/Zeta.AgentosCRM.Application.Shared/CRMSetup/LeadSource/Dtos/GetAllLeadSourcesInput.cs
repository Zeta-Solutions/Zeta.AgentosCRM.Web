using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.LeadSource.Dtos
{
    public class GetAllLeadSourcesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string AbbrivationFilter { get; set; }

        public string NameFilter { get; set; }

    }
}