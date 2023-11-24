using Zeta.AgentosCRM.CRMAgent.Contacts.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Agents
{
    public class CreateOrEditAgentContactsViewModel
    {
        public CreateOrEditAgentContactDto AgentContact { get; set; }
        public bool IsEditMode => AgentContact.Id.HasValue;
    }
}
