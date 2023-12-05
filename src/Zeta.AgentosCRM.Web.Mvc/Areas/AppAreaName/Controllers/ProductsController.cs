using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMPartner.Dtos;
using Zeta.AgentosCRM.CRMProducts;
using Zeta.AgentosCRM.CRMProducts.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Partners;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Product;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    public class ProductsController : AgentosCRMControllerBase
    {
        private readonly IProductsAppService _productsAppService;
        public ProductsController(IProductsAppService productsAppService)
        {
            _productsAppService = productsAppService;
        }
        #region Products
        public IActionResult Index()
        {
            var model = new ProductsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }
        public async Task<ActionResult> AddProducts(long? id)
        {
            GetProductForEditOutput getProductForEditOutput;

            if (id.HasValue)
            {
                getProductForEditOutput = await _productsAppService.GetProductForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getProductForEditOutput = new GetProductForEditOutput
                {
                    Product = new CreateOrEditProductDto()
                };
                //getClientForEditOutput.Client.DateofBirth = DateTime.Now;
                //getClientForEditOutput.Client.PreferedIntake = DateTime.Now;
                //getClientForEditOutput.Client.VisaExpiryDate = DateTime.Now;
            }

            var viewModel = new CreateOrEditProductModalViewModel()
            {
                Product = getProductForEditOutput.Product,

                PartnerName = getProductForEditOutput.PartnerPartnerName,
                PartnerTypeName = getProductForEditOutput.PartnerTypeName,
                BranchName = getProductForEditOutput.BranchName,
                ProductPartnerList = await _productsAppService.GetAllPartnerForTableDropdown(),
                ProductPartnerTypeList = await _productsAppService.GetAllPartnerTypeForTableDropdown(),
                ProductBranchList = await _productsAppService.GetAllBranchForTableDropdown(),

            };

            return View(viewModel);
        }

        public async Task<ActionResult> ProductsDetail(int id)
        {
            var getProductForViewDto = await _productsAppService.GetProductForView(id);
            var model = new ProductViewModel()
            {
                Product = getProductForViewDto.Product,
                PartnerTypeName = getProductForViewDto.PartnerTypeName

                ,
                PartnerPartnerName = getProductForViewDto.PartnerPartnerName

                ,
                BranchName = getProductForViewDto.BranchName

            };

            return View(model);
        }

        #endregion
    }
}
