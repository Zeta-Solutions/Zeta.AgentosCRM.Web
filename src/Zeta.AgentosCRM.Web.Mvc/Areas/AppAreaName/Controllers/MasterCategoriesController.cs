using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.MasterCategories;
using Zeta.AgentosCRM.Web.Controllers;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_MasterCategories)]
    public class MasterCategoriesController : AgentosCRMControllerBase
    {
        private readonly IMasterCategoriesAppService _masterCategoriesAppService;

        public MasterCategoriesController(IMasterCategoriesAppService masterCategoriesAppService)
        {
            _masterCategoriesAppService = masterCategoriesAppService;

        }

        public ActionResult Index()
        {
            var model = new MasterCategoriesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_MasterCategories_Create, AppPermissions.Pages_CRMSetup_MasterCategories_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetMasterCategoryForEditOutput getMasterCategoryForEditOutput;

            if (id.HasValue)
            {
                getMasterCategoryForEditOutput = await _masterCategoriesAppService.GetMasterCategoryForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getMasterCategoryForEditOutput = new GetMasterCategoryForEditOutput
                {
                    MasterCategory = new CreateOrEditMasterCategoryDto()
                };
            }

            var viewModel = new CreateOrEditMasterCategoryModalViewModel()
            {
                MasterCategory = getMasterCategoryForEditOutput.MasterCategory,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewMasterCategoryModal(int id)
        {
            var getMasterCategoryForViewDto = await _masterCategoriesAppService.GetMasterCategoryForView(id);

            var model = new MasterCategoryViewModel()
            {
                MasterCategory = getMasterCategoryForViewDto.MasterCategory
            };

            return PartialView("_ViewMasterCategoryModal", model);
        }

    }
}