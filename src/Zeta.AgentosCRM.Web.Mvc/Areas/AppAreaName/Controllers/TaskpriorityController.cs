using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Controllers;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.priority; 

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_TaskPriorities)]
    public class TaskpriorityController : AgentosCRMControllerBase
    {
        private readonly ITaskPrioritiesAppService _taskPriorityAppService;

        public TaskpriorityController(ITaskPrioritiesAppService taskPriorityAppService)
        {
            _taskPriorityAppService = taskPriorityAppService;
        }
        public ActionResult Index()
        {
           // var model = new TaskPrioritiesViewModel 
            var model=new TaskPrioritesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_TaskPriorities_Create, AppPermissions.Pages_TaskPriorities_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetTaskPriorityForEditOutput getTaskPriorityForEditOutput;

            if (id.HasValue)
            {
                getTaskPriorityForEditOutput = await _taskPriorityAppService.GetTaskPriorityForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getTaskPriorityForEditOutput = new GetTaskPriorityForEditOutput
                {
                    TaskPriority = new CreateOrEditTaskPriorityDto()
                };
            }

            var viewModel = new CreateOrEditTaskPriorityModalViewModel()
            {
                TaskPriority = getTaskPriorityForEditOutput.TaskPriority,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewpriorityModal(int id)
        {
            var getTaskPriorityForViewDto = await _taskPriorityAppService.GetTaskPriorityForView(id);

            var model = new TaskPriorityViewModel()
            {
                TaskPriority = getTaskPriorityForViewDto.TaskPriority
            };

            return PartialView("_ViewPriorityModal", model);
        }

    }
}
