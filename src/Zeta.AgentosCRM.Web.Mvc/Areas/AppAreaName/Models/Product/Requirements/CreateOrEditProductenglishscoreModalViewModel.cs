using Zeta.AgentosCRM.CRMProducts.Requirements;
using Zeta.AgentosCRM.CRMProducts.Requirements.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Product.Requirements
{
    public class CreateOrEditProductenglishscoreModalViewModel
    {
        public CreateOrEditProductEnglishRequirementDto ProductEnglishRequirement{ get; set; }
        public bool IsEditMode => ProductEnglishRequirement.Id.HasValue;
    }
}
