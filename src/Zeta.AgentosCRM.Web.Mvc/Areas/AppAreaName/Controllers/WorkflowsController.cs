using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Workflows;
using Zeta.AgentosCRM.Web.Controllers;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Workflows)]
    public class WorkflowsController : AgentosCRMControllerBase
    {
        private readonly IWorkflowsAppService _workflowsAppService;

        public WorkflowsController(IWorkflowsAppService workflowsAppService)
        {
            _workflowsAppService = workflowsAppService;

        }

        public ActionResult Index()
        {
            var model = new WorkflowsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Workflows_Create, AppPermissions.Pages_Workflows_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetWorkflowForEditOutput getWorkflowForEditOutput;

            if (id.HasValue)
            {
                getWorkflowForEditOutput = await _workflowsAppService.GetWorkflowForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getWorkflowForEditOutput = new GetWorkflowForEditOutput
                {
                    Workflow = new CreateOrEditWorkflowDto()
                };
            }

            var viewModel = new CreateOrEditWorkflowModalViewModel()
            {
                Workflow = getWorkflowForEditOutput.Workflow,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewWorkflowModal(int id)
        {
            var getWorkflowForViewDto = await _workflowsAppService.GetWorkflowForView(id);

            var model = new WorkflowViewModel()
            {
                Workflow = getWorkflowForViewDto.Workflow
            };

            return PartialView("_ViewWorkflowModal", model);
        }

    }
}