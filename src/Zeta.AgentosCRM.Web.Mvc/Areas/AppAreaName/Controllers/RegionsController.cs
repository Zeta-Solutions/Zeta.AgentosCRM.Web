using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Web.Controllers;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
 
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.CRMSetup.FeeType;
using Zeta.AgentosCRM.CRMSetup.FeeType.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.FeeTypes;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.MasterCategories;
using Zeta.AgentosCRM.CRMSetup.Regions;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Regions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Zeta.AgentosCRM.CRMSetup.Regions.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_Regions)]

    public class RegionsController : AgentosCRMControllerBase
    {
        private readonly IRegionsAppService _regionsAppService;
        public RegionsController(IRegionsAppService regionsAppService)
        {
                _regionsAppService = regionsAppService;
        }
        public IActionResult Index()
        {
            var Model = new RegionsViewModel() {
                FilterText = ""
            };
            return View(Model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_Regions_Create, AppPermissions.Pages_CRMSetup_Regions_Edit)]

        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {


            GetRegionForEditOutput getRegionForEditOutput;

            if (id.HasValue)
            {
                getRegionForEditOutput = await _regionsAppService.GetRegionForEdit(new EntityDto { Id = (int)id });
                //.GetMasterCategoryForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getRegionForEditOutput = new GetRegionForEditOutput
                {
                    Region = new CreateOrEditRegionDto()
                };
            }

            var viewModel = new CreateOrEditRegionModalViewModel()
            {
                Region = getRegionForEditOutput.Region,

            }; 
            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewRegionsModal(int id)
        {

            var getRegionForViewDto = await _regionsAppService.GetRegionForView(id);
            var model = new RegionViewModel()
            {
                Region = getRegionForViewDto.Region
            };
             
            return PartialView("_ViewRegionsModal", model);
        }

    }
}
