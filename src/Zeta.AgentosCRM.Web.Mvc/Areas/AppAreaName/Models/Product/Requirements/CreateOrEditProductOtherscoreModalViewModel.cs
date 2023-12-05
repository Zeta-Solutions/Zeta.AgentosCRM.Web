using Zeta.AgentosCRM.CRMProducts.Requirements;
using Zeta.AgentosCRM.CRMProducts.Requirements.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Product.Requirements
{
    public class CreateOrEditProductOtherscoreModalViewModel
    {
        public CreateOrEditProductOtherTestRequirementDto ProductOtherTestRequirement{ get; set; }
        public bool IsEditMode => ProductOtherTestRequirement.Id.HasValue;
    }
}
