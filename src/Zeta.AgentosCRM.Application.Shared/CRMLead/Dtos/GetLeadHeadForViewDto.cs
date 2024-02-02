using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMLead.Dtos
{
    public class GetLeadHeadForViewDto
    {
        public LeadHeadDto LeadHead { get; set; }
        public string LeadLeadSourceName { get; set; }

        public string LeadOrganizationUnitName { get; set; }
    }
}
