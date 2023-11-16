using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.TaskManagement.Followers.Dtos
{
    public class CreateOrEditTaskFollowerDto : EntityDto<long?>
    {

        public string Name { get; set; }

        public long UserId { get; set; }

        public long CRMTaskId { get; set; }

    }
}