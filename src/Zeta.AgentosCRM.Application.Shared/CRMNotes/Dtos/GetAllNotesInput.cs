using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMNotes.Dtos
{
    public class GetAllNotesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string TitleFilter { get; set; }

        public string DescriptionFilter { get; set; }

        public string ClientDisplayPropertyFilter { get; set; }

        public string CRMAgentAgentNameFilter { get; set; }

        public string PartnerPartnerNameFilter { get; set; }

        public int? PartnerIdFilter { get; set; }
        public int? AgentIdFilter { get; set; }
        public int? ClientIdFilter { get; set; }
        public int? ApplicationIdFilter { get; set; }
        public int? ApplicationstageIdFilter { get; set; }

    }
}