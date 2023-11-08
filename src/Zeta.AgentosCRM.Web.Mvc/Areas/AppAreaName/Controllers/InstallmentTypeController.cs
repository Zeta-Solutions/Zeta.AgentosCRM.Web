using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Web.Controllers;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization; 
using Zeta.AgentosCRM.Authorization; 
using Zeta.AgentosCRM.CRMSetup.InstallmentType;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.InstallmentType;
using Zeta.AgentosCRM.CRMSetup.InstallmentType.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_InstallmentTypes)]

    public class InstallmentTypeController : AgentosCRMControllerBase
    {
        private readonly IInstallmentTypesAppService _installmentTypesAppService;
        public InstallmentTypeController(IInstallmentTypesAppService installmentTypesAppService)
        {
                _installmentTypesAppService = installmentTypesAppService;
        }
        public IActionResult Index()
        {
            var Model = new InstallmentTypesViewModel() 
            {
                FilterText=""
            };

            return View(Model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_InstallmentTypes_Create, AppPermissions.Pages_InstallmentTypes_Edit)]

        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {


            GetInstallmentTypeForEditOutput getInstallmentTypeForEditOutput;

            if (id.HasValue)
            {
                getInstallmentTypeForEditOutput = await _installmentTypesAppService.GetInstallmentTypeForEdit(new EntityDto { Id = (int)id });
                //.GetMasterCategoryForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getInstallmentTypeForEditOutput = new GetInstallmentTypeForEditOutput
                {
                    InstallmentType = new CreateOrEditInstallmentTypeDto()
                };
            }

            var viewModel = new CreateOrEditInstallmentTypeModalViewModel()
            {
                InstallmentType = getInstallmentTypeForEditOutput.InstallmentType,

            }; 
            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewInstallmentTypeModal(int id)
        {

            var getInstallmentTypeForViewDto = await _installmentTypesAppService.GetInstallmentTypeForView(id);
            var model = new InstallmentTypeViewModel()
            {
                InstallmentType = getInstallmentTypeForViewDto.InstallmentType
            };
             

            return PartialView("_ViewInstallmentTypeModal", model);
        }

    }
}
