using Zeta.AgentosCRM.CRMSetup.ServiceCategory.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.ServiceCategory
{
    public class CreateOrEditServiceCategoryModalViewModel
    {
        public CreateOrEditServiceCategoryDto ServiceCategory { get; set; }

        public bool IsEditMode => ServiceCategory.Id.HasValue;
    }
}
