using Zeta.AgentosCRM.CRMSetup.Dtos;

using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.WorkflowSteps
{
    public class CreateOrEditWorkflowStepModalViewModel
    {
        public CreateOrEditWorkflowStepDto WorkflowStep { get; set; }

        public string WorkflowName { get; set; }

        public bool IsEditMode => WorkflowStep.Id.HasValue;
    }
}