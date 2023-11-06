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

    public class ClientController : AgentosCRMControllerBase
    {
        private readonly IClientsAppService _clientsAppService;

        public ClientController(IClientsAppService clientsAppService)
        {
            _clientsAppService = clientsAppService;
        }

        public IActionResult Index()
        {
            var model = new ClientsViewModel
            {
                FilterText = ""
            };
            return View(model);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Clients_Create, AppPermissions.Pages_Clients_Edit)]
        public async Task<ActionResult> CreateOrEditModal(long? id)
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

            return PartialView("_CreateOrEditModal", viewModel);
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
        public IActionResult ClientDetail()
        {
            return View();
        }
        public async Task<ActionResult> ClientCreateDetail(long? id)
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
        public IActionResult ClientEmailCompose()
        {
            return PartialView("_ClientEmailCompose", "");
        }
        public IActionResult ClienChangeProfile()
        {
            return PartialView("_ChangePictureModal", "");
        }
        
        public IActionResult ApplicationIndex()
        {
            return View("~/ApplicationClient/ApplicationIndex.cshtml");
        }
        public async Task<PartialViewResult> ViewApplicationModal(int id)
        {

            return PartialView("_ViewMasterCategoryModal", "");
        }
        public async Task<PartialViewResult> CreateOrEditApplicationModalModal(int? id)
        {

            return PartialView("ApplicationClient/_CreateOrEditModal", "");
            //return PartialView("Client/ApplicationClient/_CreateOrEditModal", "");
        }
        public IActionResult IntrestedServiceIndex()
        {
            return View("~/IntrestedService/IntrestedServiceIndex.cshtml");
        }
        public async Task<PartialViewResult> CreateOrEditIntrestedServiceModal(int? id)
        {

            return PartialView("IntrestedService/_CreateOrEditModal", "");
            //return PartialView("Client/ApplicationClient/_CreateOrEditModal", "");
        }
        public IActionResult AppointmentIndex()
        {
            return View("~/IntrestedService/AppointmentIndex.cshtml");
        }
        public async Task<PartialViewResult> CreateOrEditAppointmentModal(int? id)
        {

            return PartialView("Appointment/_CreateOrEditModal", "");
            //return PartialView("Client/ApplicationClient/_CreateOrEditModal", "");
        }
        public IActionResult TaskIndex()
        {
            return View("~/ClientTask/TaskIndex.cshtml");
        }
        public async Task<PartialViewResult> CreateOrEditTaskModal(int? id)
        {

            return PartialView("ClientTask/_CreateOrEditModal", "");
            //return PartialView("Client/ApplicationClient/_CreateOrEditModal", "");
        }
        public IActionResult ClientQuotationIndex()
        {
            return View("~/ClientQuotation/ClientQuotationIndex.cshtml");
        }
        public async Task<PartialViewResult> CreateOrEditClientQuotationModal(int? id)
        {

            return PartialView("ClientQuotation/_CreateOrEditModal", "");
            //return PartialView("Client/ApplicationClient/_CreateOrEditModal", "");
        }
        public IActionResult EducationIndex()
        {
            return View("~/Education/EducationIndex.cshtml");
        }
        public async Task<PartialViewResult> CreateOrEditClientEducationModal(int? id)
        {

            return PartialView("Education/_CreateOrEditModal", "");
            //return PartialView("Client/ApplicationClient/_CreateOrEditModal", "");
        }
        public async Task<PartialViewResult> CreateOrEditEnglishScoreModal(int? id)
        {

            return PartialView("Education/_CreateOrEditEnglishScoreModal", "");
            //return PartialView("Client/ApplicationClient/_CreateOrEditModal", "");
        }        
        public async Task<PartialViewResult> CreateOrEditOtherScoreModal(int? id)
        {

            return PartialView("Education/_CreateOrEditOtherScoreModal", "");
            //return PartialView("Client/ApplicationClient/_CreateOrEditModal", "");
        }
        public IActionResult ClientNotesIndex()
        {
            return View("~/Notes/ClientNotesIndex.cshtml");
        }
        public async Task<PartialViewResult> CreateOrEditClientNotesModal(int? id)
        {

            return PartialView("Notes/_CreateOrEditModal", "");
            //return PartialView("Client/ApplicationClient/_CreateOrEditModal", "");
        }
        public IActionResult ApplicationMainTab()
        {
            //return View("~/ApplicationClient/ApplicationMainTab.cshtml");
            return View("~/Areas/AppAreaName/Views/Client/ApplicationClient/ApplicationMainTab.cshtml");
        }

    }
}
