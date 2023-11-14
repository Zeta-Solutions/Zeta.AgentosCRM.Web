using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.CRMSetup.CRMCurrency;
using Zeta.AgentosCRM.CRMSetup.CRMCurrency.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.CRMCurrencies; 
using Zeta.AgentosCRM.Web.Controllers; 

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]

    [AbpMvcAuthorize(AppPermissions.Pages_CRMCurrencies)]
    public class QuotationCurrencyController : AgentosCRMControllerBase
    {
        private readonly ICRMCurrenciesAppService _cRMCurrenciesAppService;
        public QuotationCurrencyController(ICRMCurrenciesAppService cRMCurrenciesAppService)
        {
                _cRMCurrenciesAppService = cRMCurrenciesAppService;
        }
        public IActionResult Index()
        { 
            var model = new CRMCurrenciesViewModel()
            {
                FilterText = ""
            };
            return View(model);
             
        }
        public async Task<PartialViewResult> ViewQuotationCurrenyModal(int id)
        {
            var GetCRMCurrencyForViewDto = await _cRMCurrenciesAppService.GetCRMCurrencyForView(id);
            var model = new CRMCurrencyViewModel()
            {
                CRMCurrency = GetCRMCurrencyForViewDto.CRMCurrency,

            };

            return PartialView("_ViewQuotationCurrenyModal", model);
            //return PartialView("_ViewQuotationCurrenyModal", "");
        }

        [AbpMvcAuthorize(AppPermissions.Pages_CRMCurrencies_Create, AppPermissions.Pages_CRMCurrencies_Edit)]

        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {

            GetCRMCurrencyForEditOutput GetCRMCurrencyForEditOutput;

            if (id.HasValue)
            {
                GetCRMCurrencyForEditOutput = await _cRMCurrenciesAppService.GetCRMCurrencyForEdit(new EntityDto { Id = (int)id }); 
            }
            else
            {
                GetCRMCurrencyForEditOutput = new GetCRMCurrencyForEditOutput
                {
                    CRMCurrency = new CreateOrEditCRMCurrencyDto()
                };
            }

            var viewModel = new CreateOrEditCRMCurrencyModalViewModel()
            {
                CRMCurrency = GetCRMCurrencyForEditOutput.CRMCurrency,

            };
             
            return PartialView("_CreateOrEditModal", viewModel);

            //return PartialView("_CreateOrEditModal", "");
        }
    }
}
