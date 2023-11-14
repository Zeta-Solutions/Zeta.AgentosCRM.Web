using Zeta.AgentosCRM.CRMSetup.Dtos;

using Abp.Extensions;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Workflows
{
    public class CreateOrEditWorkflowModalViewModel
    {
        public CreateOrEditWorkflowDto Workflow { get; set; }
        public List<CreateOrEditWorkflowStepDto> WorkflowStep { get; set; }
        public bool IsEditMode => Workflow.Id.HasValue;
    }
}