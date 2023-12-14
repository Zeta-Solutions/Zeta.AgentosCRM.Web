using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.TaskManagement;
using Zeta.AgentosCRM.TaskManagement.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Tasks;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    //[AbpMvcAuthorize(AppPermissions.Pages_Tasks)]
    public class TasksController : AgentosCRMControllerBase
    {
		private readonly ICRMTasksAppService _cRMTasksAppService;

		public TasksController(ICRMTasksAppService cRMTasksAppService)
		{
			_cRMTasksAppService = cRMTasksAppService;
		}

		public ActionResult Index()
		{
			var model = new TasksViewModel
			{
				FilterText = ""
			};

			return View(model);
		}
		public async Task<ActionResult> Tasks(int id)
		{
			var getCRMTaskForViewDto = await _cRMTasksAppService.GetCRMTaskForView(id);
			var model = new TaskViewModel()
			{
				CRMTask = getCRMTaskForViewDto.CRMTask


			};

			return View("Tasks/Tasks", model);
		}
		public async Task<PartialViewResult> CreateOrEditTasksModal(long? id)
		{
			GetCRMTaskForEditOutput getCRMTaskForEditOutput;
			if (id.HasValue)
			{
				getCRMTaskForEditOutput = await _cRMTasksAppService.GetCRMTaskForEdit(new EntityDto<long> { Id = (long)id });
			}
			else
			{
				getCRMTaskForEditOutput = new GetCRMTaskForEditOutput
				{
					CRMTask = new CreateOrEditCRMTaskDto()
				};
				getCRMTaskForEditOutput.CRMTask.DueDate = DateTime.Now;
				getCRMTaskForEditOutput.CRMTask.DueTime = DateTime.Now;
			}

			var viewModel = new CreateOrEditTaskModalViewModel()
			{
				CRMTask = getCRMTaskForEditOutput.CRMTask,
				TaskCategoryName = getCRMTaskForEditOutput.TaskCategoryName,
				TaskPriorityName = getCRMTaskForEditOutput.TaskPriorityName,
				TaskUserName = getCRMTaskForEditOutput.UserName,
				CRMTaskTaskCategoryList = await _cRMTasksAppService.GetAllTaskCategoryForTableDropdown(),
				CRMTaskTaskPriorityList = await _cRMTasksAppService.GetAllTaskPriorityForTableDropdown(),
				CRMTaskUserList = await _cRMTasksAppService.GetAllUserForTableDropdown(),


			};
            return PartialView("_CreateOrEditTasksModal", viewModel);

        }
	}
}
