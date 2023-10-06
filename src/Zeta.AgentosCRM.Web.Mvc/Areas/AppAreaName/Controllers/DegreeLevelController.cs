using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.DegreeLevel;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_DegreeLevels)]
    public class DegreeLevelController : AgentosCRMControllerBase
    
    {
        private readonly IDegreeLevelsAppService _degreeLevelsAppService;

        public DegreeLevelController(IDegreeLevelsAppService degreeLevelsAppService)
        {
            _degreeLevelsAppService = degreeLevelsAppService;
        }
        public IActionResult Index()
        {
            var model=new DegreeLevelsViewModel
            {
                FilterText = ""
            };
            return View(model);
        }


        [AbpMvcAuthorize(AppPermissions.Pages_DegreeLevels_Create, AppPermissions.Pages_DegreeLevels_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetDegreeLevelForEditOutput getDegreeLevelForEditOutput;

            if (id.HasValue)
            {
                getDegreeLevelForEditOutput = await _degreeLevelsAppService.GetDegreeLevelForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getDegreeLevelForEditOutput = new GetDegreeLevelForEditOutput
                {
                    DegreeLevel = new CreateOrEditDegreeLevelDto()
                };
            }

            var viewModel = new CreateOrEditDegreeLevelModalViewModel()
            {
                DegreeLevel = getDegreeLevelForEditOutput.DegreeLevel,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }
        public async Task<PartialViewResult> ViewDegreeLevelModal(int id)
        {
            var getDegreeLevelForViewDto = await _degreeLevelsAppService.GetDegreeLevelForView(id);

            var model = new DegreeLevelViewModel()
            {
                DegreeLevel = getDegreeLevelForViewDto.DegreeLevel
            };

            return PartialView("_ViewDegreeLevelModal", model);
        }
    }
}
