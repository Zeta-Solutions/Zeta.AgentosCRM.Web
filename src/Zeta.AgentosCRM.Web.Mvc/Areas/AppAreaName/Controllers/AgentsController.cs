using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMAgent;
using Zeta.AgentosCRM.CRMAgent.Dtos;
using Zeta.AgentosCRM.CRMApplications;
using Zeta.AgentosCRM.CRMApplications.Dtos;
using Zeta.AgentosCRM.CRMNotes;
using Zeta.AgentosCRM.CRMNotes.Dtos;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.CRMSetup.FeeType;
using Zeta.AgentosCRM.CRMSetup.FeeType.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Agents;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Applications;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.NotesAndTerms;
//using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.FeeTypes;
//using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.MasterCategories;
using Zeta.AgentosCRM.Web.Controllers;


namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]

    public class AgentsController :  AgentosCRMControllerBase
    {
        private readonly IAgentsAppService _agentsAppService;
        private readonly INotesAppService _notesAppService;
        private readonly IApplicationsAppService _applicationsAppService;
        public AgentsController(IAgentsAppService agentsAppService, INotesAppService notesAppService, IApplicationsAppService applicationsAppService)
        {
            _agentsAppService = agentsAppService;
            _notesAppService = notesAppService;
            _applicationsAppService = applicationsAppService;
        }
        public IActionResult Index()
        {
            var model = new AgentsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }
        public IActionResult AddAgentdetail()
        {
            return View("~/Areas/AppAreaName/Views/Agents/AddAgents/CreateOrEdit.cshtml");
        }
        public ActionResult CreateOrEditModal(int? id)
        {

            return PartialView("_CreateOrEditModal", "");
        }
        public IActionResult ViewAgentMainFormModal(int id)
        {
            return View("~/Areas/AppAreaName/Views/Agents/MainFormDetails.cshtml");


            //return PartialView("_ViewAgentMainFormModal", "");
        }

        //applications 

        public IActionResult ApplicationView()
        {
            return View("~/Areas/AppAreaName/Views/Agents/Application/Index.cshtml");
        }

        //Note And Term Data Grid
        public ActionResult CreateOrEditNotesAndTermsModal()
        {
            return PartialView("NoteandTerm/_CreateOrEditModal", "");
        }
        // Referred Client
        public ActionResult CreateOrEditReferredClientsModal()
        {
            return PartialView("ReferredClients/_CreateOrEditModal", "");
        }
        public async Task<ActionResult> CreateOrEdit(long? id)
        {
            GetAgentForEditOutput getAgentForEditOutput;

            if (id.HasValue)
            {
                getAgentForEditOutput = await _agentsAppService.GetAgentForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getAgentForEditOutput = new GetAgentForEditOutput
                {
                    Agent = new CreateOrEditAgentDto()
                };
                getAgentForEditOutput.Agent.ContractExpiryDate = DateTime.Now;
                //getClientForEditOutput.Client.PreferedIntake = DateTime.Now;
                //getClientForEditOutput.Client.VisaExpiryDate = DateTime.Now;
            }

            var viewModel = new CreateOrEditAgentsViewModel()
            {
                Agent = getAgentForEditOutput.Agent,
              
                CountryName = getAgentForEditOutput.CountryName,
                OrganizationUnitName = getAgentForEditOutput.OrganizationUnitDisplayName,
                AgentCountryList = await _agentsAppService.GetAllCountryForTableDropdown(),
                AgentOrganizationUnitList = await _agentsAppService.GetAllOrganizationUnitForTableDropdown(),
               
            };

            return View(viewModel);
        }
        public async Task<ActionResult> DetailsForm(int id)
        {
            var getAgentForViewDto = await _agentsAppService.GetAgentForView(id);

            var model = new AgentViewModel()
            {
                Agent = getAgentForViewDto.Agent

                ,
                CountryName = getAgentForViewDto.CountryName

                //,
                //CountryDisplayProperty2 = getPartnerForViewDto.CountryDisplayProperty2

            };

            return View(model);
        }
        #region NotesandTerms
        public async Task<ActionResult> NotesAndTerms(int id)
        {
            var getNoteForViewDto = await _notesAppService.GetNoteForView(id);
            var model = new NoteViewModel()
            {
                Note = getNoteForViewDto.Note


            };

            return View("NoteandTerm/NotesAndTerms", model);
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

            return PartialView("NoteandTerm/_CreateOrEditModal", viewModel);
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

            return View("Applications/Application", model);
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

            return PartialView("Applications/_CreateOrEditModal", ViewModel);
            //return PartialView("Client/ApplicationClient/_CreateOrEditModal", "");


        }

        #endregion
    }
}
