using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.TaskManagement.Followers.Dtos
{
    public class GetTaskFollowerForEditOutput
    {
        public CreateOrEditTaskFollowerDto TaskFollower { get; set; }

        public string UserName { get; set; }

        public string CRMTaskTitle { get; set; }

    }
}