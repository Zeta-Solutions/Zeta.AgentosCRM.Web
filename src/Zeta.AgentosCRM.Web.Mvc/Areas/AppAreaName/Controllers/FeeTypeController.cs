using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization; 
using Zeta.AgentosCRM.CRMSetup.FeeType;
using Zeta.AgentosCRM.CRMSetup.FeeType.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.FeeTypes; 
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_FeeTypes)]

    public class FeeTypeController : AgentosCRMControllerBase
    {
        private readonly IFeeTypesAppService _feeTypesAppService;
 
        public FeeTypeController(IFeeTypesAppService feeTypesAppService)
        {
            _feeTypesAppService = feeTypesAppService;

        }
        public IActionResult Index()
        {
            var model = new FeeTypesViewModel()
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_FeeTypes_Create, AppPermissions.Pages_FeeTypes_Edit)]

        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {


            GetFeeTypeForEditOutput getFeeTypeForEditOutput;

            if (id.HasValue)
            {
                getFeeTypeForEditOutput = await _feeTypesAppService.GetFeeTypeForEdit(new EntityDto { Id = (int)id }); 
                    //.GetMasterCategoryForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getFeeTypeForEditOutput = new GetFeeTypeForEditOutput
                {
                    FeeType = new CreateOrEditFeeTypeDto()
                };
            }

            var viewModel = new CreateOrEditFeeTypeModalViewModel()
            {
                FeeType = getFeeTypeForEditOutput.FeeType,

            };

            //return PartialView("_CreateOrEditModal", viewModel);
            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewFeeTypeModal(int id)
        {
            var getFeeTypeForViewDto = await _feeTypesAppService.GetFeeTypeForView(id);
            var model = new FeeTypeViewModel()
            {
                FeeType = getFeeTypeForViewDto.FeeType
            };

            return PartialView("_ViewFeeTypeModal", model);
        }
    }
}
