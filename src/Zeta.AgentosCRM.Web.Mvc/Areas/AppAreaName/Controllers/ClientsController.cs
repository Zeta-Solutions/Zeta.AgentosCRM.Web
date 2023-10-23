using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients;
using Zeta.AgentosCRM.Web.Controllers;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMClient.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Clients)]
    public class ClientsController : AgentosCRMControllerBase
    {
        private readonly IClientsAppService _clientsAppService;

        public ClientsController(IClientsAppService clientsAppService)
        {
            _clientsAppService = clientsAppService;

        }

        public ActionResult Index()
        {
            var model = new ClientsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Clients_Create, AppPermissions.Pages_Clients_Edit)]
        public async Task<ActionResult> CreateOrEdit(long? id)
        {
            GetClientForEditOutput getClientForEditOutput;

            if (id.HasValue)
            {
                getClientForEditOutput = await _clientsAppService.GetClientForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getClientForEditOutput = new GetClientForEditOutput
                {
                    Client = new CreateOrEditClientDto()
                };
                getClientForEditOutput.Client.DateofBirth = DateTime.Now;
                getClientForEditOutput.Client.PreferedIntake = DateTime.Now;
                getClientForEditOutput.Client.VisaExpiryDate = DateTime.Now;
            }

            var viewModel = new CreateOrEditClientViewModel()
            {
                Client = getClientForEditOutput.Client,
                CountryDisplayProperty = getClientForEditOutput.CountryDisplayProperty,
                UserName = getClientForEditOutput.UserName,
                BinaryObjectDescription = getClientForEditOutput.BinaryObjectDescription,
                DegreeLevelName = getClientForEditOutput.DegreeLevelName,
                SubjectAreaName = getClientForEditOutput.SubjectAreaName,
                LeadSourceName = getClientForEditOutput.LeadSourceName,
                CountryName2 = getClientForEditOutput.CountryName2,
                CountryName3 = getClientForEditOutput.CountryName3,
                ClientCountryList = await _clientsAppService.GetAllCountryForTableDropdown(),
                ClientUserList = await _clientsAppService.GetAllUserForTableDropdown(),
                ClientDegreeLevelList = await _clientsAppService.GetAllDegreeLevelForTableDropdown(),
                ClientSubjectAreaList = await _clientsAppService.GetAllSubjectAreaForTableDropdown(),
                ClientLeadSourceList = await _clientsAppService.GetAllLeadSourceForTableDropdown(),
            };

            return View(viewModel);
        }

        public async Task<ActionResult> ViewClient(long id)
        {
            var getClientForViewDto = await _clientsAppService.GetClientForView(id);

            var model = new ClientViewModel()
            {
                Client = getClientForViewDto.Client
                ,
                CountryDisplayProperty = getClientForViewDto.CountryDisplayProperty

                ,
                UserName = getClientForViewDto.UserName

                ,
                BinaryObjectDescription = getClientForViewDto.BinaryObjectDescription

                ,
                DegreeLevelName = getClientForViewDto.DegreeLevelName

                ,
                SubjectAreaName = getClientForViewDto.SubjectAreaName

                ,
                LeadSourceName = getClientForViewDto.LeadSourceName

                ,
                CountryName2 = getClientForViewDto.CountryName2

                ,
                CountryName3 = getClientForViewDto.CountryName3

            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Clients_Create, AppPermissions.Pages_Clients_Edit)]
        public PartialViewResult BinaryObjectLookupTableModal(Guid? id, string displayName)
        {
            var viewModel = new ClientBinaryObjectLookupTableViewModel()
            {
                Id = id.ToString(),
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_ClientBinaryObjectLookupTableModal", viewModel);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Clients_Create, AppPermissions.Pages_Clients_Edit)]
        public PartialViewResult CountryLookupTableModal(int? id, string displayName)
        {
            var viewModel = new ClientCountryLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_ClientCountryLookupTableModal", viewModel);
        }

    }
}