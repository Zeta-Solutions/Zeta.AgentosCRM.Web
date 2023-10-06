using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.priority
{
    public class CreateOrEditTaskPriorityModalViewModel
    {
        public CreateOrEditTaskPriorityDto TaskPriority { get; set; }
        public bool IsEditMode => TaskPriority.Id.HasValue;
    }
}

