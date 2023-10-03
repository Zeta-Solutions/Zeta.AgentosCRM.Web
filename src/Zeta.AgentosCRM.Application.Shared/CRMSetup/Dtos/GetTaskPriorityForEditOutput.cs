using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class GetTaskPriorityForEditOutput
    {
        public CreateOrEditTaskPriorityDto TaskPriority { get; set; }

    }
}