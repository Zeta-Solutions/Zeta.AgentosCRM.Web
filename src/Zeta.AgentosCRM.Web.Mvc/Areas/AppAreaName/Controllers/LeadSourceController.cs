using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.Web.Controllers;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.ServiceCategory;
using Zeta.AgentosCRM.CRMSetup.LeadSource;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.LeadSource;
using Zeta.AgentosCRM.CRMSetup.LeadSource.Dtos;
using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{


    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_LeadSources)]

    public class LeadSourceController : AgentosCRMControllerBase
    {
        private readonly ILeadSourcesAppService _leadSourcesAppService;

        public LeadSourceController(ILeadSourcesAppService leadSourcesAppService)
        {
            _leadSourcesAppService = leadSourcesAppService;
        }

        public IActionResult Index()
        {
            var model = new LeadSourcesViewModel
            {
                FilterText = ""
            };
            return View();
        }
        [AbpMvcAuthorize(AppPermissions.Pages_LeadSources_Create, AppPermissions.Pages_LeadSources_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int ? id)
        {
            GetLeadSourceForEditOutput getLeadSourceForEditOutput;
            if (id.HasValue)
            {
                getLeadSourceForEditOutput = await _leadSourcesAppService.GetLeadSourceForEdit(new EntityDto { Id = (int)id });

            }
            else
            {
                getLeadSourceForEditOutput = new GetLeadSourceForEditOutput
                {
                    LeadSource = new CreateOrEditLeadSourceDto()

                };
            }
            var viewModel = new CreateOrEditLeadSourceModalViewModel()
            {
                LeadSource = getLeadSourceForEditOutput.LeadSource,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewLeadSourceModal(int id)
        {
            var getLeadSourceForViewDto = await _leadSourcesAppService.GetLeadSourceForView(id);

            var model = new LeadSourceViewModel()
            {
                LeadSource = getLeadSourceForViewDto.LeadSource
            };

            return PartialView("_ViewLeadSourceModal", model);

        }


    }
}
