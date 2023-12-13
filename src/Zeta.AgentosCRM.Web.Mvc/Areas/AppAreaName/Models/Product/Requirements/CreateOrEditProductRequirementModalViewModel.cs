using System.Collections.Generic;
using Zeta.AgentosCRM.CRMProducts.OtherInfo;
using Zeta.AgentosCRM.CRMProducts.Requirements.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Product.Requirements
{
    public class CreateOrEditProductRequirementModalViewModel
    {
        public CreateOrEditProductAcadamicRequirementDto ProductAcadamicRequirement { get; set; }
        public string DegreeLevelName { get; set; }
        public List<ProductAcadamicRequirementDegreeLevelLookupTableDto> ProductAcadamicRequirementDegreeLevelList { get; set; }

        public bool IsEditMode => ProductAcadamicRequirement.Id.HasValue;
    }
}
