using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMLead.Dtos
{
    public class GetLeadHeadForEditOutput 
    {
        public CreateOrEditLeadHeadDto LeadHead { get; set; }
        public string LeadLeadSourceName { get; set; }

        public string LeadOrganizationUnitName { get; set; }

        public List<CreateOrEditLeadDetailDto> LeadDetail { get; set; }
    }
}
