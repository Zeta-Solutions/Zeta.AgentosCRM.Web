using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class WorkflowDto : EntityDto
    {
        public string Name { get; set; }

        public bool IsForAllOffices { get; set; } 
    }
}