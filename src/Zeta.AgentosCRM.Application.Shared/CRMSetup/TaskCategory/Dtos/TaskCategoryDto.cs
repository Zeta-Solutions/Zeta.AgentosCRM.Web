using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.TaskCategory.Dtos
{
    public class TaskCategoryDto : EntityDto
    {
        public string Abbrivation { get; set; }

        public string Name { get; set; }

    }
}