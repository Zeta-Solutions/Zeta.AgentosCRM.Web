using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.CRMSetup.Dtos
{
    public class GetWorkflowOfficeForEditOutput
    {
        public CreateOrEditWorkflowOfficeDto WorkflowOffice { get; set; }

        public string OrganizationUnitDisplayName { get; set; }

        public string WorkflowName { get; set; }

    }
}