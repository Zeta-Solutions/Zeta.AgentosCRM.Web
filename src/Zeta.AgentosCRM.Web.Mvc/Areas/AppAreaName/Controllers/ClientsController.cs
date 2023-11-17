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
using Zeta.AgentosCRM.CRMAppointments;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Appointments;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Applications;
using Zeta.AgentosCRM.CRMAppointments.Dtos;
using Zeta.AgentosCRM.CRMApplications.Dtos;
using Zeta.AgentosCRM.CRMApplications;
using Zeta.AgentosCRM.CRMClient.InterstedServices;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients.InterestedServices;
using Zeta.AgentosCRM.CRMClient.InterstedServices.Dtos;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMClient.Education;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Education;
using Zeta.AgentosCRM.CRMClient.Education.Dtos;
using Zeta.AgentosCRM.CRMNotes.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.NotesAndTerms;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Tasks;
using Zeta.AgentosCRM.CRMNotes;
using Zeta.AgentosCRM.TaskManagement;
using Zeta.AgentosCRM.TaskManagement.Dtos;
using Zeta.AgentosCRM.CRMClient.CheckIn;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients.CheckInLogs;
using Zeta.AgentosCRM.CRMClient.CheckIn.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Clients)]
    public class ClientsController : AgentosCRMControllerBase
    {
        private readonly IClientsAppService _clientsAppService;
        private readonly IAppointmentsAppService _appointmentsAppService;
        private readonly IClientTagsAppService _clientTagsAppService;
        private readonly IFollowersAppService _followersAppService;
        private readonly IApplicationsAppService _applicationsAppService;
        private readonly IClientInterstedServicesAppService _clientInterstedServicesAppService;
        private readonly IClientEducationsAppService _clientEducationsAppService;
        private readonly INotesAppService _notesAppService;
        private readonly ICRMTasksAppService _cRMTasksAppService;
        private readonly ICheckInLogsAppService _checkInLogsAppService;
        

        public ClientsController(IClientsAppService clientsAppService, IAppointmentsAppService appointmentsAppService, 
            IClientTagsAppService clientTagsAppService, IFollowersAppService followersAppService, 
            IApplicationsAppService applicationsAppService ,
            IClientInterstedServicesAppService clientInterstedServicesAppService,
            IClientEducationsAppService clientEducationsAppService, INotesAppService notesAppService, 
            ICRMTasksAppService cRMTasksAppService, ICheckInLogsAppService checkInLogsAppService)
        {
            _clientsAppService = clientsAppService;
            _appointmentsAppService = appointmentsAppService;
            _clientTagsAppService = clientTagsAppService;
            _followersAppService = followersAppService;
            _applicationsAppService = applicationsAppService;
            _clientInterstedServicesAppService = clientInterstedServicesAppService;
            _clientEducationsAppService = clientEducationsAppService;
            _notesAppService = notesAppService;
            _cRMTasksAppService = cRMTasksAppService;
            _checkInLogsAppService = checkInLogsAppService;
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
            var getAppointmentsForViewDto = await _appointmentsAppService.GetAppointmentForView(id);
            var model = new AppointmentViewModel()
            {
                Appointment = getAppointmentsForViewDto.Appointment
               

            };

            return View("Appointment/AppointmentsIndex", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Appointments_Create, AppPermissions.Pages_Appointments_Edit)]
        public async Task<PartialViewResult> CreateOrEditAppointmentModal(long? id)
        {
            GetAppointmentForEditOutput getAppointmentForEditOutput;
            if (id.HasValue)
            {
                getAppointmentForEditOutput = await _appointmentsAppService.GetAppointmentForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getAppointmentForEditOutput = new GetAppointmentForEditOutput
                {
                    Appointment = new CreateOrEditAppointmentDto()
                };
            }
            var ViewModel = new CreateOrEditAppointmentsViewModel()
            {
                Appointment = getAppointmentForEditOutput.Appointment,
                AppointmentInviteesList = await _appointmentsAppService.GetAllUserForTableDropdown()

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
        #region "Application"

        [AbpMvcAuthorize(AppPermissions.Pages_Applications_Create, AppPermissions.Pages_Applications_Edit)]
        public async Task<ActionResult> ApplicationsIndex(long? id)
        {
            long applicationId = id.GetValueOrDefault();
            var getApplicationForViewDto = await _applicationsAppService.GetApplicationForView(applicationId);
            var model = new ApplicationViewModel()
            {
                Application = getApplicationForViewDto.Application


            };

            return View("Application/ApplicationsIndex", model);
            //return View("~/Application/ApplicationsIndex.cshtml");
        }
        public async Task<PartialViewResult> CreateOrEditApplicationModal(long? id)
        {
            GetApplicationForEditOutput getApplicationForEditOutput;
            if (id.HasValue)
            {
                getApplicationForEditOutput = await _applicationsAppService.GetApplicationForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getApplicationForEditOutput = new GetApplicationForEditOutput
                {
                    Application = new CreateOrEditApplicationDto()
                };
            }
            var ViewModel = new CreateOrEditApplicationsViewModel()
            {
                Application = getApplicationForEditOutput.Application,
                ApplicationWorkflowList = await _applicationsAppService.GetAllWorkflowForTableDropdown(),
                ApplicationPartnerList = await _applicationsAppService.GetAllPartnerForTableDropdown(),
                ApplicationProductList = await _applicationsAppService.GetAllProductForTableDropdown()

            };

            return PartialView("Application/_CreateOrEditModal", ViewModel);
            //return PartialView("Client/ApplicationClient/_CreateOrEditModal", "");


        }

        #endregion

        #region  "Interested Service"


         public async Task<ActionResult> InterestedServicesIndex(long? id)
        {
            long applicationId = id.GetValueOrDefault();
            var getInterestedServicesForViewDto = await _clientInterstedServicesAppService.GetClientInterstedServiceForView(applicationId);
            var model = new InterestedServiceViewModel()
            {
                ClientInterstedService = getInterestedServicesForViewDto.ClientInterstedService


            };

            return View("IntrestedService/IntrestedServiceIndex", model);
            //return View("~/Application/ApplicationsIndex.cshtml");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_ClientInterstedServices_Create, AppPermissions.Pages_ClientInterstedServices_Edit)]
        public async Task<PartialViewResult> CreateOrEditIntrestedServiceModal(long? id)
        {
            GetClientInterstedServiceForEditOutput getClientInterstedServiceForEditOutput;
            if (id.HasValue)
            {
                getClientInterstedServiceForEditOutput = await _clientInterstedServicesAppService.GetClientInterstedServiceForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getClientInterstedServiceForEditOutput = new GetClientInterstedServiceForEditOutput
                {
                    ClientInterstedService = new CreateOrEditClientInterstedServiceDto()
                };
            }
            var ViewModel = new CreateOrEditInterestedServicesViewModel()
            {
                ClientInterestedService = getClientInterstedServiceForEditOutput.ClientInterstedService,
                ClientInterestedServiceList = await _clientInterstedServicesAppService.GetAllClientForTableDropdown(),
                ClientInterestedServicePartnerList = await _clientInterstedServicesAppService.GetAllPartnerForTableDropdown(),
                ClientInterestedServiceProductList = await _clientInterstedServicesAppService.GetAllProductForTableDropdown(),
                ClientInterestedServiceBranchList = await _clientInterstedServicesAppService.GetAllBranchForTableDropdown(),
                ClientInterestedServiceWorkflowList = await _clientInterstedServicesAppService.GetAllWorkflowForTableDropdown()

            };

            return PartialView("InterestedService/_CreateOrEditModal", ViewModel);


        }




        #endregion
        #region "Education"
        public async Task<ActionResult> EducationIndex(long? id)
        {
            long applicationId = id.GetValueOrDefault();
            var getClientEducationForViewDto = await _clientEducationsAppService.GetClientEducationForView(applicationId);
            var model = new EducationViewModel()
            {
                ClientEducation = getClientEducationForViewDto.ClientEducation


            };

            return View("Education/EducationIndex", model);
        }
        public async Task<PartialViewResult> CreateOrEditClientEducationModal(int? id)
        {
            GetClientEducationForEditOutput getClientEducationForEditOutput;
            if (id.HasValue)
            {
                getClientEducationForEditOutput = await _clientEducationsAppService.GetClientEducationForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getClientEducationForEditOutput = new GetClientEducationForEditOutput
                {
                    ClientEducation = new CreateOrEditClientEducationDto()
                };
            }
            var ViewModel = new CreateOrEditEducationViewModel()
            {
                ClientEducation = getClientEducationForEditOutput.ClientEducation,
                ClientEducationDegreeLevelList = await _clientEducationsAppService.GetAllDegreeLevelForTableDropdown(),
                ClientEducationSubjectList = await _clientEducationsAppService.GetAllSubjectForTableDropdown(),
                ClientEducationSubjectAreaList = await _clientEducationsAppService.GetAllSubjectAreaForTableDropdown(),

            };
           

            return PartialView("Education/_CreateOrEditModal", ViewModel);
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
        #endregion

        #region "Notes ANd Terms"
        public async Task<ActionResult> NotesAndTerms(int id)
        {
            var getNoteForViewDto = await _notesAppService.GetNoteForView(id);
            var model = new NoteViewModel()
            {
                Note = getNoteForViewDto.Note


            };

            return View("NotesAndTerms/NotesAndTerms", model);
        }
        public async Task<PartialViewResult> CreateOrEditNotesModal(long? id)
        {
            GetNoteForEditOutput getNotesForEditOutput;
            if (id.HasValue)
            {
                getNotesForEditOutput = await _notesAppService.GetNoteForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getNotesForEditOutput = new GetNoteForEditOutput
                {
                    Note = new CreateOrEditNoteDto()
                };
            }
            var viewModel = new CreateOrEditNotesModalViewModel()
            {
                Note = getNotesForEditOutput.Note,


            };

            return PartialView("NotesAndTerms/_CreateOrEditNotesModal", viewModel);
        }
        #endregion

        #region "Tasks"
        public async Task<ActionResult> Tasks(int id)
        {
            var getCRMTaskForViewDto = await _cRMTasksAppService.GetCRMTaskForView(id);
            var model = new TaskViewModel()
            {
                CRMTask = getCRMTaskForViewDto.CRMTask


            };

            return View("Tasks/Tasks", model);
        }
        public async Task<PartialViewResult> CreateOrEditTasksModal(long? id)
        {
            GetCRMTaskForEditOutput getCRMTaskForEditOutput;
            if (id.HasValue)
            {
                getCRMTaskForEditOutput = await _cRMTasksAppService.GetCRMTaskForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getCRMTaskForEditOutput = new GetCRMTaskForEditOutput
                {
                    CRMTask = new CreateOrEditCRMTaskDto()
                };
                getCRMTaskForEditOutput.CRMTask.DueDate = DateTime.Now;
                getCRMTaskForEditOutput.CRMTask.DueTime = DateTime.Now;
            }

            var viewModel = new CreateOrEditTaskModalViewModel()
            {
                CRMTask = getCRMTaskForEditOutput.CRMTask,
                TaskCategoryName = getCRMTaskForEditOutput.TaskCategoryName,
                TaskPriorityName = getCRMTaskForEditOutput.TaskPriorityName,
                TaskUserName = getCRMTaskForEditOutput.UserName,
                CRMTaskTaskCategoryList = await _cRMTasksAppService.GetAllTaskCategoryForTableDropdown(),
                CRMTaskTaskPriorityList = await _cRMTasksAppService.GetAllTaskPriorityForTableDropdown(),
                CRMTaskUserList = await _cRMTasksAppService.GetAllUserForTableDropdown(),


            };
            return PartialView("Tasks/_CreateOrEditTaskModal", viewModel);

        }
        #endregion

        #region "Check-In Logs"
        public async Task<ActionResult> CheckInLogsIndex(int id)
        {
            var getCheckInLogsForViewDto = await _checkInLogsAppService.GetCheckInLogForView(id);
            var model = new CheckInLogViewModel()
            {
                CheckInLog = getCheckInLogsForViewDto.CheckInLog


            };

            return View("ClientCheckInLogs/CheckInLogs", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_CheckInLogs_Create, AppPermissions.Pages_CheckInLogs_Edit)]
        public async Task<PartialViewResult> CreateOrEditCheckInLogsModal(long? id)
        {
            GetCheckInLogForEditOutput getCheckInLogForEditOutput;
            if (id.HasValue)
            {
                getCheckInLogForEditOutput = await _checkInLogsAppService.GetCheckInLogForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getCheckInLogForEditOutput = new GetCheckInLogForEditOutput
                {
                    CheckInLog = new CreateOrEditCheckInLogDto()
                };
            }
            var viewModel = new CreateOrEditCheckInLogsViewModel()
            {
                CheckInLog = getCheckInLogForEditOutput.CheckInLog,
                CheckInLogClientList = await _checkInLogsAppService.GetAllClientForTableDropdown(),
                CheckInLogUserList = await _checkInLogsAppService.GetAllUserForTableDropdown(),

            };

            return PartialView("ClientCheckInLogs/_CreateOrEditModal", viewModel);
        }
        #endregion
    }
}