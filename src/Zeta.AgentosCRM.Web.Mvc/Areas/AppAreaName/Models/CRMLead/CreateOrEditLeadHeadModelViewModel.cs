using System.Collections.Generic;
using Zeta.AgentosCRM.CRMLead.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.CRMLead
{
    public class CreateOrEditLeadHeadModelViewModel
    {
        public CreateOrEditLeadHeadDto LeadHead { get; set; }

        public string OrganizationalUnitName { get; set; }
        public string LeadSourceName { get; set; }

        public List<LeadOrganizationalUnitLookupTableDto> LeadOrganizationalList { get; set; }
        public List<LeadLeadSourceLookupTableDto> LeadLeadSourceList { get; set; }
        public bool IsEditMode => LeadHead.Id.HasValue;
    }
}
