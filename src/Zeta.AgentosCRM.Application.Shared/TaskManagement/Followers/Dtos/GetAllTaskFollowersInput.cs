using Abp.Application.Services.Dto;
using System;

namespace Zeta.AgentosCRM.TaskManagement.Followers.Dtos
{
    public class GetAllTaskFollowersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string UserNameFilter { get; set; }

        public string CRMTaskTitleFilter { get; set; }

    }
}