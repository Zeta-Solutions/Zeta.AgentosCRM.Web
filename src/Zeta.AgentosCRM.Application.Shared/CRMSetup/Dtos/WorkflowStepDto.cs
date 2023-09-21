using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class WorkflowStepDto : EntityDto
    {
        public int SrlNo { get; set; }

        public string Abbrivation { get; set; }

        public string Name { get; set; }

        public int WorkflowId { get; set; }

    }
}