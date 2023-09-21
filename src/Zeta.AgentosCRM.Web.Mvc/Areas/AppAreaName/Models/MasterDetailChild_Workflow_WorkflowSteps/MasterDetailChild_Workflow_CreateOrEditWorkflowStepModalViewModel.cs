using Zeta.AgentosCRM.CRMSetup.Dtos;

using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.WorkflowSteps
{
    public class MasterDetailChild_Workflow_CreateOrEditWorkflowStepModalViewModel
    {
        public CreateOrEditWorkflowStepDto WorkflowStep { get; set; }

        public bool IsEditMode => WorkflowStep.Id.HasValue;
    }
}