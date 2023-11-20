using System;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class WorkflowOfficeDto : EntityDto
    {
        public string Name { get; set; }

        public long? OrganizationUnitId { get; set; }

        public int WorkflowId { get; set; }

    }
}