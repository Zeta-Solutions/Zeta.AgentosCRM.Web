using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMPartner.Dtos;
using Zeta.AgentosCRM.CRMProducts;
using Zeta.AgentosCRM.CRMProducts.Dtos;
using Zeta.AgentosCRM.CRMProducts.OtherInfo;
using Zeta.AgentosCRM.CRMProducts.OtherInfo.Dtos;
using Zeta.AgentosCRM.CRMProducts.Requirements;
using Zeta.AgentosCRM.CRMProducts.Requirements.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Product;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Product.Otherinfo;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Product.Requirements;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    public class ProductsController : AgentosCRMControllerBase
    {
        private readonly IProductsAppService _productsAppService;
        private readonly IProductOtherInformationsAppService _productOtherInformationsAppService;
        private readonly IProductAcadamicRequirementsAppService _productAcadamicRequirementsAppService;
        private readonly IProductEnglishRequirementsAppService _productEnglishRequirementsAppService;
        private readonly IProductOtherTestRequirementsAppService _ProductOtherTestRequirementsAppService;
        public ProductsController(IProductsAppService productsAppService, IProductOtherInformationsAppService productOtherInformationsAppService, IProductAcadamicRequirementsAppService productAcadamicRequirementsAppService, IProductEnglishRequirementsAppService productEnglishRequirementsAppService, IProductOtherTestRequirementsAppService productOtherTestRequirementsAppService)
        {
            _productsAppService = productsAppService;
            _productOtherInformationsAppService = productOtherInformationsAppService;
            _productAcadamicRequirementsAppService = productAcadamicRequirementsAppService;
            _productEnglishRequirementsAppService = productEnglishRequirementsAppService;
            _ProductOtherTestRequirementsAppService = productOtherTestRequirementsAppService;

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
        #region Otherinfo
        public async Task<ActionResult> Otherinfo(int id)
        {
            var getProductOtherInformationForViewDto = await _productOtherInformationsAppService.GetProductOtherInformationForView(id);
            var model = new OtherinfoViewModel()
            {
                ProductOtherInformation = getProductOtherInformationForViewDto.ProductOtherInformation


            };

            return View("Otherinfo/Otherinfo", model);
        }
        public async Task<PartialViewResult> CreateOrEditOtherinfoModal(int? id)
        {
            GetProductOtherInformationForEditOutput getProductOtherInformationForEditOutput;
            CRMProducts.OtherInfo.Dtos.GetAllForLookupTableInput getAllForLookupTableInput = new CRMProducts.OtherInfo.Dtos.GetAllForLookupTableInput
            {
                Filter = ""
            };

            if (id.HasValue)
            {
                getProductOtherInformationForEditOutput = await _productOtherInformationsAppService.GetProductOtherInformationForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getProductOtherInformationForEditOutput = new GetProductOtherInformationForEditOutput
                {
                    ProductOtherInformation = new CreateOrEditProductOtherInformationDto()
                };
               
            }

            var viewModel = new CreateOrEditProductOtherInfoModalViewModel()
            {
           
                ProductOtherInformation = getProductOtherInformationForEditOutput.ProductOtherInformation,
                SubjectAreaName = getProductOtherInformationForEditOutput.SubjectAreaName,
                SubjectName = getProductOtherInformationForEditOutput.SubjectName,
                DegreeLevelName = getProductOtherInformationForEditOutput.DegreeLevelName,
                ProductOtherInformationSubjectAreaList = await _productOtherInformationsAppService.GetAllSubjectAreaForTableDropdown(),
                ProductOtherInformationSubjectList = await _productOtherInformationsAppService.GetAllSubjectForTableDropdown(),
                ProductOtherInformationDegreeLevelList = await _productOtherInformationsAppService.GetAllDegreeLevelForLookupTable(getAllForLookupTableInput),


            };
            return PartialView("Otherinfo/_CreateOrEditOtherinfoModal", viewModel);

        }
        #endregion
        #region ProductAcadamicRequirement
        public async Task<ActionResult> ProductAcadamicRequirement(int id)
        {
            var getProductAcadamicRequirementForViewDto = await _productAcadamicRequirementsAppService.GetProductAcadamicRequirementForView(id);
            var model = new ProductRequirementViewModel()
            {
                ProductAcadamicRequirement = getProductAcadamicRequirementForViewDto.ProductAcadamicRequirement


            };

            return View("ProductRequirement/ProductRequirement", model);
        }
        public async Task<PartialViewResult> CreateOrEditAcadamicRequirementModal(int? id)
        {
            GetProductAcadamicRequirementForEditOutput getProductAcadamicRequirementForEditOutput;

            if (id.HasValue)
            {
                getProductAcadamicRequirementForEditOutput = await _productAcadamicRequirementsAppService.GetProductAcadamicRequirementForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getProductAcadamicRequirementForEditOutput = new GetProductAcadamicRequirementForEditOutput
                {
                    ProductAcadamicRequirement = new CreateOrEditProductAcadamicRequirementDto()
                };

            }

            var viewModel = new CreateOrEditProductRequirementModalViewModel()
            {

                ProductAcadamicRequirement = getProductAcadamicRequirementForEditOutput.ProductAcadamicRequirement,
                DegreeLevelName = getProductAcadamicRequirementForEditOutput.DegreeLevelName,

                ProductAcadamicRequirementDegreeLevelList = await _productAcadamicRequirementsAppService.GetAllDegreeLevelForTableDropdown(),


            };
            return PartialView("ProductRequirement/_CreateOrEditProductRequirementModal", viewModel);

        }

        #endregion
        #region productEnglishScore
        public async Task<PartialViewResult> CreateOrEditproductenglishscoreModal(long? id)
        {
            GetProductEnglishRequirementForEditOutput getProductEnglishRequirementForEditOutput;
            if (id.HasValue)
            {
                getProductEnglishRequirementForEditOutput = await _productEnglishRequirementsAppService.GetProductEnglishRequirementForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getProductEnglishRequirementForEditOutput = new GetProductEnglishRequirementForEditOutput
                {
                    ProductEnglishRequirement = new CreateOrEditProductEnglishRequirementDto()
                };
            
            }

            var viewModel = new CreateOrEditProductenglishscoreModalViewModel()
            {
                ProductEnglishRequirement = getProductEnglishRequirementForEditOutput.ProductEnglishRequirement,
               


            };
            return PartialView("ProudctEnglishScore/_CreateOrEditproductenglishscoreModal", viewModel);

        }
        #endregion
        #region ProductOtherscore
        public async Task<PartialViewResult> CreateOrEditproductotherscoreModal(long? id)
        {
            GetProductOtherTestRequirementForEditOutput getProductOtherTestRequirementForEditOutput;
            if (id.HasValue)
            {
                getProductOtherTestRequirementForEditOutput = await _ProductOtherTestRequirementsAppService.GetProductOtherTestRequirementForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getProductOtherTestRequirementForEditOutput = new GetProductOtherTestRequirementForEditOutput
                {
                    ProductOtherTestRequirement = new CreateOrEditProductOtherTestRequirementDto()
                };
            }

            var viewModel = new CreateOrEditProductOtherscoreModalViewModel()
            {
                ProductOtherTestRequirement = getProductOtherTestRequirementForEditOutput.ProductOtherTestRequirement,


            };
            return PartialView("ProductOtherscore/_CreateOrEditproductotherscoreModal", viewModel);

        }
        #endregion
    }
}
