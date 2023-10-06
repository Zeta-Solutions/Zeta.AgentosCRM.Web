using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.CRMSetup.ProductType;
using Zeta.AgentosCRM.CRMSetup.ProductType.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.ProductType;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.MasterCategories;
using Zeta.AgentosCRM.Web.Controllers;
//using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.FeeTypes;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_ProductTypes)]

    public class ProductTypeController : AgentosCRMControllerBase
    {
        private readonly IProductTypesAppService _productTypesAppService;
        public ProductTypeController(IProductTypesAppService productTypesAppService)
        {
                _productTypesAppService = productTypesAppService;
        }
        public IActionResult Index()
        {
            var model = new ProductTypesViewModel()
            {
                FilterText = ""
            };
            return View(model);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_ProductTypes_Create, AppPermissions.Pages_ProductTypes_Edit)]

        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetProductTypeForEditOutput getProductTypeForEditOutput;
            if (id.HasValue)
            {
                getProductTypeForEditOutput = await _productTypesAppService.GetProductTypeForEdit(new EntityDto { Id = (int)id });

            }
            else
            {
                getProductTypeForEditOutput = new GetProductTypeForEditOutput
                {
                    ProductType = new CreateOrEditProductTypeDto()
                };
            }
            var Viewmodel = new CreateOrEditProductTypeModalViewModel()
            {
                ProductType = getProductTypeForEditOutput.ProductType,
                MasterCategoryName = getProductTypeForEditOutput.MasterCategoryName,
                ProductTypeMasterCategoryList = await _productTypesAppService.GetAllMasterCategoryForTableDropdown(),
            };


            return PartialView("_CreateOrEditModal", Viewmodel);
        }

        public async Task<PartialViewResult> ViewProductTypeModal(int id)
        {

            var getProductTypeForViewDto = await _productTypesAppService.GetProductTypeForView(id);
            var model = new ProductTypeViewModel()
            {
                ProductType = getProductTypeForViewDto.ProductType,

                MasterCategoryName = getProductTypeForViewDto.MasterCategoryName
            };
             
            return PartialView("_ViewProductTypeModal", model);
        }
        //Product_Type
    }
}
