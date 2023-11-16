using System.Collections.Generic;
using Zeta.AgentosCRM.CRMPartner.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Partners
{
    public class CreateOrEditPartnerViewModel
    {
        public CreateOrEditPartnerDto Partner { get; set; }
     
        public string BinaryObjectDescription { get; set; }
        
        public string Currency { get; set;}
        public string MasterCategoryName { get; set;}
        public string PartnerTypeName { get; set;}
        public string WorkflowName { get; set;}
        public string CountryName { get; set; }

        public List<PartnerCountryLookupTableDto> PartnerCountryList { get; set; }
        public List<PartnerMasterCategoryLookupTableDto> PartnerMasterCategoryList { get; set; }
        public List<PartnerPartnerTypeLookupTableDto> PartnerPartnerTypeList { get; set; }
        public List<PartnerWorkflowLookupTableDto> PartnerWorkflowList { get; set; }
        // public List<PartnerCurrencyLookupTableDto> PartnerCurrencyList { get; set; }
        public bool IsEditMode => Partner.Id.HasValue;

    }
}
