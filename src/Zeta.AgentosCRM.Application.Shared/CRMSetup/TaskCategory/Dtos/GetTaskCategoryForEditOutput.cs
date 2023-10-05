using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.TaskCategory.Dtos
{
    public class GetTaskCategoryForEditOutput
    {
        public CreateOrEditTaskCategoryDto TaskCategory { get; set; }

    }
}