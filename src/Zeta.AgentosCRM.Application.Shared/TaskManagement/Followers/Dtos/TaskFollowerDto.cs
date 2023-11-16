using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.TaskManagement.Followers.Dtos
{
    public class TaskFollowerDto : EntityDto<long>
    {
        public string Name { get; set; }

        public long UserId { get; set; }

        public long CRMTaskId { get; set; }

    }
}