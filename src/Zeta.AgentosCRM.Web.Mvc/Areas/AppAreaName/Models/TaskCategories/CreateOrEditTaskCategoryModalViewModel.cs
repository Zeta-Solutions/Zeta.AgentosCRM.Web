using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.TaskCategory.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.TaskCategories
{
    public class CreateOrEditTaskCategoryModalViewModel
    {
        public CreateOrEditTaskCategoryDto TaskCategory { get; set; }
        public bool IsEditMode => TaskCategory.Id.HasValue;
    }
}
