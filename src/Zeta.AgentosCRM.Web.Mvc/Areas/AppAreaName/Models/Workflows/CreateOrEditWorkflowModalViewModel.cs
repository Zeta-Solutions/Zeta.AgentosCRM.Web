using Zeta.AgentosCRM.CRMSetup.Dtos;

using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Workflows
{
    public class CreateOrEditWorkflowModalViewModel
    {
        public CreateOrEditWorkflowDto Workflow { get; set; }

        public bool IsEditMode => Workflow.Id.HasValue;
    }
}