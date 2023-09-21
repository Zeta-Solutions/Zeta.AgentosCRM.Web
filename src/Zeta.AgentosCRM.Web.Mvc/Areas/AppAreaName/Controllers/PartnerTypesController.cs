using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.PartnerTypes;
using Zeta.AgentosCRM.Web.Controllers;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_PartnerTypes)]
    public class PartnerTypesController : AgentosCRMControllerBase
    {
        private readonly IPartnerTypesAppService _partnerTypesAppService;

        public PartnerTypesController(IPartnerTypesAppService partnerTypesAppService)
        {
            _partnerTypesAppService = partnerTypesAppService;

        }

        public ActionResult Index()
        {
            var model = new PartnerTypesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_PartnerTypes_Create, AppPermissions.Pages_CRMSetup_PartnerTypes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetPartnerTypeForEditOutput getPartnerTypeForEditOutput;

            if (id.HasValue)
            {
                getPartnerTypeForEditOutput = await _partnerTypesAppService.GetPartnerTypeForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getPartnerTypeForEditOutput = new GetPartnerTypeForEditOutput
                {
                    PartnerType = new CreateOrEditPartnerTypeDto()
                };
            }

            var viewModel = new CreateOrEditPartnerTypeModalViewModel()
            {
                PartnerType = getPartnerTypeForEditOutput.PartnerType,
                MasterCategoryName = getPartnerTypeForEditOutput.MasterCategoryName,
                PartnerTypeMasterCategoryList = await _partnerTypesAppService.GetAllMasterCategoryForTableDropdown(),

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewPartnerTypeModal(int id)
        {
            var getPartnerTypeForViewDto = await _partnerTypesAppService.GetPartnerTypeForView(id);

            var model = new PartnerTypeViewModel()
            {
                PartnerType = getPartnerTypeForViewDto.PartnerType
                ,
                MasterCategoryName = getPartnerTypeForViewDto.MasterCategoryName

            };

            return PartialView("_ViewPartnerTypeModal", model);
        }

    }
}