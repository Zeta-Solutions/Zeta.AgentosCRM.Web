using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.WorkflowSteps;
using Zeta.AgentosCRM.Web.Controllers;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_WorkflowSteps)]
    public class MasterDetailChild_Workflow_WorkflowStepsController : AgentosCRMControllerBase
    {
        private readonly IWorkflowStepsAppService _workflowStepsAppService;

        public MasterDetailChild_Workflow_WorkflowStepsController(IWorkflowStepsAppService workflowStepsAppService)
        {
            _workflowStepsAppService = workflowStepsAppService;
        }

        public ActionResult Index(int workflowId)
        {
            var model = new MasterDetailChild_Workflow_WorkflowStepsViewModel
            {
                FilterText = "",
                WorkflowId = workflowId
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_WorkflowSteps_Create, AppPermissions.Pages_CRMSetup_WorkflowSteps_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetWorkflowStepForEditOutput getWorkflowStepForEditOutput;

            if (id.HasValue)
            {
                getWorkflowStepForEditOutput = await _workflowStepsAppService.GetWorkflowStepForEdit(new EntityDto { Id = (int)id }); 
            }
            else
            {
                getWorkflowStepForEditOutput = new GetWorkflowStepForEditOutput
                {
                    WorkflowStep = new CreateOrEditWorkflowStepDto()
                };
            }

            var viewModel = new MasterDetailChild_Workflow_CreateOrEditWorkflowStepModalViewModel()
            {
                WorkflowStep = getWorkflowStepForEditOutput.WorkflowStep,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewWorkflowStepModal(int id)
        {
            var getWorkflowStepForViewDto = await _workflowStepsAppService.GetWorkflowStepForView(id);

            var model = new MasterDetailChild_Workflow_WorkflowStepViewModel()
            {
                WorkflowStep = getWorkflowStepForViewDto.WorkflowStep
            };

            return PartialView("_ViewWorkflowStepModal", model);
        }

    }
}