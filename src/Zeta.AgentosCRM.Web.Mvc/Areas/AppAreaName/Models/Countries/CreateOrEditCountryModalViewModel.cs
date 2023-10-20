using System.Collections.Generic;
using Zeta.AgentosCRM.CRMSetup.Countries.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Countries
{
    public class CreateOrEditCountryModalViewModel
    {
        public CreateOrEditCountryDto Country { get; set; }
        public string RegionName { get; set; }

        public List<CountryRegionLookupTableDto> CountryRegionList { get; set; }

        public bool IsEditMode => Country.Id.HasValue; 

    }
}
