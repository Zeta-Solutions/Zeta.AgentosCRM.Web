using Zeta.AgentosCRM.CRMSetup.Dtos;

using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.MasterCategories
{
    public class CreateOrEditMasterCategoryModalViewModel
    {
        public CreateOrEditMasterCategoryDto MasterCategory { get; set; }

        public bool IsEditMode => MasterCategory.Id.HasValue;
    }
}