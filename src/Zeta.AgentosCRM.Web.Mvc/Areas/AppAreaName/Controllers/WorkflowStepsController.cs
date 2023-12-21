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
    public class WorkflowStepsController : AgentosCRMControllerBase
    {
        private readonly IWorkflowStepsAppService _workflowStepsAppService;

        public WorkflowStepsController(IWorkflowStepsAppService workflowStepsAppService)
        {
            _workflowStepsAppService = workflowStepsAppService;

        }

        public ActionResult Index()
        {
            var model = new WorkflowStepsViewModel
            {
                FilterText = ""
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

            var viewModel = new CreateOrEditWorkflowStepModalViewModel()
            {
                WorkflowStep = getWorkflowStepForEditOutput.WorkflowStep,
                WorkflowName = getWorkflowStepForEditOutput.WorkflowName,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewWorkflowStepModal(int id)
        {
            var getWorkflowStepForViewDto = await _workflowStepsAppService.GetWorkflowStepForView(id);

            var model = new WorkflowStepViewModel()
            {
                WorkflowStep = getWorkflowStepForViewDto.WorkflowStep
                ,
                WorkflowName = getWorkflowStepForViewDto.WorkflowName

            };

            return PartialView("_ViewWorkflowStepModal", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_WorkflowSteps_Create, AppPermissions.Pages_CRMSetup_WorkflowSteps_Edit)]
        public PartialViewResult WorkflowLookupTableModal(int? id, string displayName)
        {
            var viewModel = new WorkflowStepWorkflowLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_WorkflowStepWorkflowLookupTableModal", viewModel);
        }

    }
}