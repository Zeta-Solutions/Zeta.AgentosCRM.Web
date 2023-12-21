using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.CRMSetup.Countries.Dtos;
using Zeta.AgentosCRM.CRMSetup.ProductType.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Countries;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]

    [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_Countries)]
    public class CountryController : AgentosCRMControllerBase
    {
        private readonly ICountriesAppService _countriesAppService;

        public CountryController(ICountriesAppService countriesAppService)
        {
            _countriesAppService = countriesAppService;
        }

        public IActionResult Index()
        {
            var model = new CountriesViewModel()
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_Countries_Create, AppPermissions.Pages_CRMSetup_Countries_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetCountryForEditOutput getCountryForEditOutput;
            if (id.HasValue)
            {
                getCountryForEditOutput =await _countriesAppService.GetCountryForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getCountryForEditOutput = new GetCountryForEditOutput
                {
                    Country = new CreateOrEditCountryDto()
                };
            }
            var ViewModel = new CreateOrEditCountryModalViewModel
            {
                Country = getCountryForEditOutput.Country,
                RegionName = getCountryForEditOutput.RegionName,
                CountryRegionList = await _countriesAppService.GetAllRegionForTableDropdown(),
            };

            return PartialView("_CreateOrEditModal", ViewModel);
        }
        public async Task<PartialViewResult> ViewCountryModal(int id)
        {
            var getCountriesForViewDto = await _countriesAppService.GetCountryForView(id);

            var model = new CountryViewModel()
            {
                Country = getCountriesForViewDto.Country
                ,

                RegionName = getCountriesForViewDto.RegionName
            };
            return PartialView("_ViewCountryModal", model);
        }
       
    }
}
