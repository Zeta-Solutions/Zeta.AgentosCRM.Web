using System.Collections.Generic;
using Zeta.AgentosCRM.CRMPartner.Contact.Dtos;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Partners
{
    public class CreateOrEditPartnerContactModalViewModel
    {
        public CreateOrEditPartnerContactDto PartnerContact { get; set; }

        public List<PartnerContactBranchLookupTableDto> PartnerContactBranchList { get; set; }
        public string BranchName { get; set; }
        public bool IsEditMode => PartnerContact.Id.HasValue;

        public int PartnerId { get; set; }
    }
}
