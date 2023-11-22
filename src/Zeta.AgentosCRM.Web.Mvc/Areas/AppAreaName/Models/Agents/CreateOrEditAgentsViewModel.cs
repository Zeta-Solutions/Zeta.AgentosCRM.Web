using System.Collections.Generic;
using Zeta.AgentosCRM.CRMAgent.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Agents
{
    public class CreateOrEditAgentsViewModel
    {
        public CreateOrEditAgentDto Agent { get; set; }

        public List <AgentCountryLookupTableDto> AgentCountryList { get; set; }
        public string CountryName { get; set; }
        public bool IsEditMode => Agent.Id.HasValue;
    }
}
