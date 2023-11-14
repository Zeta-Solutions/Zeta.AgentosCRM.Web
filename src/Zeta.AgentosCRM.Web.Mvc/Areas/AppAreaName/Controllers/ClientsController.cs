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
using Zeta.AgentosCRM.CRMClient.Appointment;
using Zeta.AgentosCRM.CRMClient.Appointment.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients.ClientsAppointments;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Tag;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.PartnerTypes;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Clients)]
    public class ClientsController : AgentosCRMControllerBase
    {
        private readonly IClientsAppService _clientsAppService;
        private readonly IClientAppointmentsAppService _clientAppointmentsAppService;
        private readonly IClientTagsAppService _clientTagsAppService;
        private readonly IFollowersAppService _followersAppService;   


        public ClientsController(IClientsAppService clientsAppService, IClientAppointmentsAppService clientAppointmentsAppService, IClientTagsAppService clientTagsAppService, IFollowersAppService followersAppService)
        {
            _clientsAppService = clientsAppService;
            _clientAppointmentsAppService = clientAppointmentsAppService;
            _clientTagsAppService = clientTagsAppService;
            _followersAppService = followersAppService; 
        }

        #region "Clents"
        public ActionResult Index()
        {
            var model = new ClientsViewModel
            {
                FilterText = ""
            };

            return View(model);
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
                //CountryDisplayProperty = getClientForEditOutput.CountryDisplayProperty,
                UserName = getClientForEditOutput.UserName,
                BinaryObjectDescription = getClientForEditOutput.BinaryObjectDescription,
                DegreeLevelName = getClientForEditOutput.DegreeLevelName,
                SubjectAreaName = getClientForEditOutput.SubjectAreaName,
                LeadSourceName = getClientForEditOutput.LeadSourceName,
                PassportCountry = getClientForEditOutput.PassportCountry,
                ClientCountryList = await _clientsAppService.GetAllCountryForTableDropdown(),
                ClientUserList = await _clientsAppService.GetAllUserForTableDropdown(),
                ClientDegreeLevelList = await _clientsAppService.GetAllDegreeLevelForTableDropdown(),
                ClientSubjectAreaList = await _clientsAppService.GetAllSubjectAreaForTableDropdown(),
                ClientLeadSourceList = await _clientsAppService.GetAllLeadSourceForTableDropdown(),
            };


            return View(viewModel);
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
                CountryName = getClientForEditOutput.CountryName,
                UserName = getClientForEditOutput.UserName,
                BinaryObjectDescription = getClientForEditOutput.BinaryObjectDescription,
                DegreeLevelName = getClientForEditOutput.DegreeLevelName,
                SubjectAreaName = getClientForEditOutput.SubjectAreaName,
                LeadSourceName = getClientForEditOutput.LeadSourceName,
                PassportCountry = getClientForEditOutput.PassportCountry,
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
                CountryName = getClientForViewDto.CountryName

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
                PassportCountry = getClientForViewDto.PassportCountry

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
        public ActionResult ClientEmailCompose()
        {
            return PartialView("_ClientEmailCompose", "");
        }
        public async Task<ActionResult> ClientProfileDetail(long id)
        {
            var getClientForViewDto = await _clientsAppService.GetClientForView(id);

            var model = new ClientViewModel()
            {
                Client = getClientForViewDto.Client
                ,
                CountryName = getClientForViewDto.CountryName

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
                PassportCountry = getClientForViewDto.PassportCountry

            };

            return View(model);
        }

        #endregion
        #region "ClientAppointments"
       
        public async Task<ActionResult> AppointmentsIndex(int id)
        {
            var getAppointmentsForViewDto = await _clientAppointmentsAppService.GetClientAppointmentForView(id);
            var model = new ClientsAppointmentViewModel()
            {
                ClientAppointment = getAppointmentsForViewDto.ClientAppointment
               

            };

            return View("Appointment/AppointmentsIndex", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_ClientAppointments_Create, AppPermissions.Pages_ClientAppointments_Edit)]
        public async Task<PartialViewResult> CreateOrEditClientAppointmentModal(long? id)
        {
            GetClientAppointmentForEditOutput getClientAppointmentForEditOutput;
            if (id.HasValue)
            {
                getClientAppointmentForEditOutput = await _clientAppointmentsAppService.GetClientAppointmentForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getClientAppointmentForEditOutput = new GetClientAppointmentForEditOutput
                {
                    ClientAppointment = new CreateOrEditClientAppointmentDto()
                };
            }
            var ViewModel = new CreateOrEditClientAppointmentViewModel()
            {
                ClientAppointment = getClientAppointmentForEditOutput.ClientAppointment,
                ClientAppointmentInviteesList = await _clientAppointmentsAppService.GetAllClientForTableDropdown()

            };

            //return PartialView("_CreateOrEditModal", ViewModel);
            return PartialView("Appointment/_CreateOrEditModal", ViewModel);

        }
        public async Task<PartialViewResult> CreateOrEditClientTags(int? id)
        {
            GetClientTagForEditOutput getClientTagForEditOutput;
            if (id.HasValue)
            {
                getClientTagForEditOutput = await _clientTagsAppService.GetClientTagForEdit( new EntityDto { Id = (int)id });
            }
            else
            {
                getClientTagForEditOutput = new GetClientTagForEditOutput
                {
                    ClientTag = new CreateOrEditClientTagDto()
                };
            }
            var ViewModel = new CreateOrEditClientTagViewModel()
            {
                ClientTag = getClientTagForEditOutput.ClientTag,
                ClientTagList = await _clientTagsAppService.GetAllTagForTableDropdown()

            };

            return PartialView("_CreateOrEditClientTags", ViewModel);
        }
        public async Task<PartialViewResult> CreateOrEditClientFllowers(int? id)
        {
            GetFollowerForEditOutput getFollowerForEditOutput;
            if (id.HasValue)
            {
                getFollowerForEditOutput = await _followersAppService.GetFollowerForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getFollowerForEditOutput = new GetFollowerForEditOutput
                {
                    Follower = new CreateOrEditFollowerDto()
                };
            }
            var ViewModel = new CreateOrEditClientFllowersViewModel()
            {
                clientFolllowers = getFollowerForEditOutput.Follower,
                clientFolllowersList = await _followersAppService.GetAllUserForTableDropdown()

            };

            return PartialView("_CreateOrEditClientFllowers", ViewModel);
        }
        #endregion

    }
}