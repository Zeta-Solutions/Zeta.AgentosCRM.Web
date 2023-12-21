using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMApplications.Dtos
{
    public class ApplicationDto : EntityDto<long>
    {
        public string Name { get; set; }

        public long ClientId { get; set; }

        public int WorkflowId { get; set; }

        public long? PartnerId { get; set; }

        public long ProductId { get; set; }

        public long BranchId { get; set; }

        public bool IsDiscontinue { get; set; }

    }
}