using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.CRMSetup.TaskCategory.Dtos
{
    public class GetAllTaskCategoriesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string AbbrivationFilter { get; set; }

        public string NameFilter { get; set; }

    }
}