using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.TaskManagement.Dtos
{
    public class GetAllCRMTasksForExcelInput
    {
        public string Filter { get; set; }

        public string TitleFilter { get; set; }

        public DateTime? MaxDueDateFilter { get; set; }
        public DateTime? MinDueDateFilter { get; set; }

        public DateTime? MaxDueTimeFilter { get; set; }
        public DateTime? MinDueTimeFilter { get; set; }

        public string DescriptionFilter { get; set; }

        public int? MaxRelatedToFilter { get; set; }
        public int? MinRelatedToFilter { get; set; }

        public string TaskCategoryNameFilter { get; set; }

        public string UserNameFilter { get; set; }

        public string TaskPriorityNameFilter { get; set; }

        public string ClientDisplayPropertyFilter { get; set; }

        public string PartnerPartnerNameFilter { get; set; }

        public string ApplicationNameFilter { get; set; }

        public string ApplicationStageNameFilter { get; set; }

    }
}