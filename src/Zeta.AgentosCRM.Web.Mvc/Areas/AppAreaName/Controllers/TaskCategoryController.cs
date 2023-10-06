using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Controllers;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Zeta.AgentosCRM.CRMSetup.TaskCategory;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.TaskCategories;
using Zeta.AgentosCRM.CRMSetup.TaskCategory.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.SubjectArea;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_TaskCategories)]
    public class TaskCategoryController : AgentosCRMControllerBase
    {
        private readonly ITaskCategoriesAppService _taskCategoriesAppService;
        public TaskCategoryController(ITaskCategoriesAppService taskCategoriesAppService)
        {
            _taskCategoriesAppService = taskCategoriesAppService;
        }
        public IActionResult Index()
        {  
            var model= new TaskCategoriesViewModel
            {
                FilterText = ""
            };
            return View(model);
         
        }
        public async Task<PartialViewResult> ViewCategoryModal(int id)
        {
            var getTaskCategoryForViewDto = await _taskCategoriesAppService.GetTaskCategoryForView(id);
            var model = new TaskCategoryViewModel()
            {
                TaskCategory = getTaskCategoryForViewDto.TaskCategory
            };
            return PartialView("_ViewCategoryModal", model);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_TaskCategories_Create, AppPermissions.Pages_TaskCategories_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetTaskCategoryForEditOutput getTaskCategoryForEditOutput;
            if (id.HasValue)
            {
                getTaskCategoryForEditOutput = await _taskCategoriesAppService.GetTaskCategoryForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getTaskCategoryForEditOutput = new GetTaskCategoryForEditOutput
                {
                    TaskCategory = new CreateOrEditTaskCategoryDto()
                };
            }
            var viewModel = new CreateOrEditTaskCategoryModalViewModel()
            {
                TaskCategory = getTaskCategoryForEditOutput.TaskCategory,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }
     
    }
}
