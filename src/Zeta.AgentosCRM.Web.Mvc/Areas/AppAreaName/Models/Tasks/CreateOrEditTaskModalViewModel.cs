using System.Collections.Generic;
using Zeta.AgentosCRM.TaskManagement.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Tasks
{
    public class CreateOrEditTaskModalViewModel
    {
        public CreateOrEditCRMTaskDto CRMTask { get; set; }

        public bool IsEditMode => CRMTask.Id.HasValue;
        public string TaskCategoryName { get; set; }
        public string TaskPriorityName { get; set; }
        public string TaskClientName { get; set; }
        public string TaskPartnerName { get; set; }
        public string TaskApplicationName { get; set; }
        public string TaskApplicationStageName { get; set; }
        public string TaskUserName { get; set; }

        public List<CRMTaskTaskCategoryLookupTableDto> CRMTaskTaskCategoryList { get; set; }
        public List<CRMTaskTaskPriorityLookupTableDto> CRMTaskTaskPriorityList { get; set; }
        public List<CRMTaskClientLookupTableDto> CRMTaskClientList { get; set; }
        public List<CRMTaskPartnerLookupTableDto> CRMTaskPartnerList { get; set; }
        public List<CRMTaskApplicationLookupTableDto> CRMTaskApplicationList { get; set; }
        public List<CRMTaskApplicationStageLookupTableDto> CRMTaskApplicationStageList { get; set; }
        public List<CRMTaskUserLookupTableDto> CRMTaskUserList { get; set; }

        public int PartnerId { get; set; }
        public int ClientId { get; set; }

    }
}
