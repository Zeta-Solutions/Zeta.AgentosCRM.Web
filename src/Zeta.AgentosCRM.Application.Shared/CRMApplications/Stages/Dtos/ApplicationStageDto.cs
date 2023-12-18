using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMApplications.Stages.Dtos
{
    public class ApplicationStageDto : EntityDto<long>
    {
        public string Name { get; set; }

        public long ApplicationId { get; set; }
        public long WorkflowStepId { get; set; }

        public bool IsCurrent { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsActive { get; set; }

    }
}