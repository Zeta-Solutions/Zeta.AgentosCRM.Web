using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class WorkflowStepDto : EntityDto
    {
        public int SrlNo { get; set; }

        public string Abbrivation { get; set; }
        
        public string Name { get; set; }

        public bool IsPartnerClientIdRequired { get; set; }

        public bool IsStartEndDateRequired { get; set; }

        public bool IsNoteRequired { get; set; }

        public bool IsApplicationIntakeRequired { get; set; }

        public bool IsActive { get; set; }

        public bool IsWinStage { get; set; }

        public int WorkflowId { get; set; }

    }
}