using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class CreateOrEditWorkflowOfficeDto : EntityDto<int?>
    {

        public string Name { get; set; }

        public long? OrganizationUnitId { get; set; }

        public int WorkflowId { get; set; }

    }
}