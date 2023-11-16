using System.Collections.Generic;
using Zeta.AgentosCRM.CRMPartner.Contact;
using Zeta.AgentosCRM.CRMPartner.Contact.Dtos;
using Zeta.AgentosCRM.CRMPartner.Contract.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Partners
{
    public class CreateOrEditPartnerContractModalViewModel
    {
        public CreateOrEditPartnerContractDto PartnerContract { get; set; }


        public List<PartnerContractRegionLookupTableDto> PartnerContractRegionList { get; set; }
        public List<PartnerContractAgentLookupTableDto> PartnerContractAgentList { get; set; }
        public string RegionName { get; set; }
        public string AgentName { get; set; }

        public bool IsEditMode => PartnerContract.Id.HasValue;

        public int PartnerId { get; set; }
    }

}
