using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization; 
using Zeta.AgentosCRM.CRMSetup.ServiceCategory;
using Zeta.AgentosCRM.CRMSetup.ServiceCategory.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.ServiceCategory;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_ServiceCategories)]
    public class ServiceCategoryController : AgentosCRMControllerBase
    {
        private readonly IServiceCategoriesAppService _serviceCategoriesAppService;

        public ServiceCategoryController(IServiceCategoriesAppService serviceCategoriesAppService)
        {
            _serviceCategoriesAppService = serviceCategoriesAppService;
        }

        public IActionResult Index()
        {
            var model = new ServiceCategoriesViewModel()
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_ServiceCategories_Create, AppPermissions.Pages_ServiceCategories_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetServiceCategoryForEditOutput getServiceCategoryForEditOutput;
            if (id.HasValue)
            {
                getServiceCategoryForEditOutput = await _serviceCategoriesAppService.GetServiceCategoryForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getServiceCategoryForEditOutput = new GetServiceCategoryForEditOutput
                {

                    ServiceCategory = new CreateOrEditServiceCategoryDto()
                };
            }
            var viewModel = new CreateOrEditServiceCategoryModalViewModel()
            {
                ServiceCategory = getServiceCategoryForEditOutput.ServiceCategory,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewServiceCategoryModal(int id)
        {
            var getServiceCategoryForViewDto = await _serviceCategoriesAppService.GetServiceCategoryForView(id);

            var model = new ServiceCategoryViewModel()
            {
                ServiceCategory = getServiceCategoryForViewDto.ServiceCategory
            };

            return PartialView("_ViewServiceCategoryModal", model);
        }

    }
}
