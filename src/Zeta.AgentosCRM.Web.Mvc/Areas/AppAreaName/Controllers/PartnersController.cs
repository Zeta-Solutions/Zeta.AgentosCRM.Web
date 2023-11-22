using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMApplications;
using Zeta.AgentosCRM.CRMApplications.Dtos;
using Zeta.AgentosCRM.CRMAppointments;
using Zeta.AgentosCRM.CRMAppointments.Dtos;
using Zeta.AgentosCRM.CRMClient.Dtos;
using Zeta.AgentosCRM.CRMNotes;
using Zeta.AgentosCRM.CRMNotes.Dtos;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMPartner.Contact;
using Zeta.AgentosCRM.CRMPartner.Contact.Dtos;
using Zeta.AgentosCRM.CRMPartner.Contract;
using Zeta.AgentosCRM.CRMPartner.Contract.Dtos;
using Zeta.AgentosCRM.CRMPartner.Dtos;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos;
using Zeta.AgentosCRM.CRMPartner.Promotion;
using Zeta.AgentosCRM.CRMPartner.Promotion.Dtos;
using Zeta.AgentosCRM.CRMProducts;
using Zeta.AgentosCRM.CRMProducts.Dtos;
using Zeta.AgentosCRM.TaskManagement;
using Zeta.AgentosCRM.TaskManagement.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Applications;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Appointments;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.LeadSource;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.NotesAndTerms;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.PartnerBranch;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Partners;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Product;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Tasks;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    public class PartnersController : AgentosCRMControllerBase
    {
        private readonly IPartnersAppService _partnersAppService;
        private readonly IBranchesAppService _branchsAppService;
        private readonly IPartnerContactsAppService _partnerContactsAppService;
        private readonly IPartnerContractsAppService _partnerContractsAppService;
        private readonly INotesAppService _notesAppService;
        private readonly IPartnerPromotionsAppService _partnerPromotionsAppService;
        private readonly ICRMTasksAppService _cRMTasksAppService;
        private readonly IProductsAppService _productsAppService;
        private readonly IAppointmentsAppService _appointmentsAppService;
        private readonly IApplicationsAppService _applicationsAppService;
        private readonly IPromotionProductsAppService _promotionProductsAppService;
        public PartnersController(IPartnersAppService partnersAppService, IBranchesAppService branchsAppService, IPartnerContactsAppService partnerContactsAppService, IPartnerContractsAppService partnerContractsAppService, INotesAppService notesAppService, IPartnerPromotionsAppService partnerPromotionsAppService, ICRMTasksAppService cRMTasksAppService, IProductsAppService productsAppService, IAppointmentsAppService appointmentsAppService, IApplicationsAppService applicationsAppService, IPromotionProductsAppService promotionProductsAppService)
        {
            _partnersAppService = partnersAppService;
            _branchsAppService = branchsAppService;
            _partnerContactsAppService = partnerContactsAppService;
            _partnerContractsAppService = partnerContractsAppService;
            _notesAppService = notesAppService;
            _partnerPromotionsAppService = partnerPromotionsAppService;
            _cRMTasksAppService = cRMTasksAppService;
            _productsAppService = productsAppService;
            _appointmentsAppService = appointmentsAppService;
            _applicationsAppService = applicationsAppService;
            _promotionProductsAppService = promotionProductsAppService;
        }

        public IActionResult Index()
        {
            var model = new PartnersViewModel
            {
                FilterText = ""
            };

            return View(model);
        }
        public ActionResult PartnersDetails()
        {

            // return RedirectToAction("DetailsForm.cshtml");
            return View("~/Areas/AppAreaName/Views/Partners/DetailsForm.cshtml");
        }
        public ActionResult InformationsDetails()
        {

            return View();
        }
        public ActionResult CreateOrEditModal(int? id)
        {

            return PartialView("_CreateOrEditModal", "");
        }
        public ActionResult CreateOrEditAppModal(int? id)
        {

            return PartialView("Applications/_CreateOrEditModal", "");
        }
        public ActionResult CreateOrEditProductsModal(int? id)
        {

            return PartialView("Products/_CreateOrEditModal", "");
        }

        public ActionResult ViewApplicationDetails()
        {

            return PartialView("~/Areas/AppAreaName/Views/Partners/Applications/_Application.cshtml");

        }
        public ActionResult CreateOrEditEmailModal()
        {

            return PartialView("ComposeEmail/_CreateOrEditModal", "");

        } 

        public ActionResult CreateOrEditApponitmentlModal()
        {

            return PartialView("Appointments/_CreateOrEditModal", "");

        }



        public ActionResult AddPartnersDetails()
        {

            //return PartialView("_ViewPartnersDetails");

            return View("~/Areas/AppAreaName/Views/Partners/InformationsDetails.cshtml");

        }

        public ActionResult AddApplicationForm()
        {

            //return PartialView("_ViewPartnersDetails");

            return View("~/Areas/AppAreaName/Views/Partners/ApplicationForm/Index.cshtml");

        }

        [AbpMvcAuthorize(AppPermissions.Pages_Partners_Create, AppPermissions.Pages_Partners_Edit)]
        public async Task<ActionResult> CreateOrEdit(long? id)
        {
            GetPartnerForEditOutput getPartnerForEditOutput;

            if (id.HasValue)
            {
                getPartnerForEditOutput = await _partnersAppService.GetPartnerForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getPartnerForEditOutput = new GetPartnerForEditOutput
                {
                    Partner = new CreateOrEditPartnerDto()
                };
                //getClientForEditOutput.Client.DateofBirth = DateTime.Now;
                //getClientForEditOutput.Client.PreferedIntake = DateTime.Now;
                //getClientForEditOutput.Client.VisaExpiryDate = DateTime.Now;
            }

            var viewModel = new CreateOrEditPartnerViewModel()
            {
                Partner = getPartnerForEditOutput.Partner,
                //PartnerName = getPartnerForEditOutput.Partner.PartnerName,
                ////BusinessRegistrationNumber = getPartnerForEditOutput.BusinessRegistrationNumber,
                //Street = getPartnerForEditOutput.Partner.Street,
                //City = getPartnerForEditOutput.Partner.City,
                //State = getPartnerForEditOutput.Partner.State,
                //Zipcode = getPartnerForEditOutput.Partner.ZipCode,
                //Phone = getPartnerForEditOutput.Partner.PhoneNo,
                //Email = getPartnerForEditOutput.Partner.Email,
                //Fax = getPartnerForEditOutput.Partner.Fax,
                //Website = getPartnerForEditOutput.Partner.Website,
                //MarketingOfficeEmail = getPartnerForEditOutput.Partner.MarketingEmail,
                //University = getPartnerForEditOutput.Partner.University,
                //Currency = getPartnerForEditOutput.Partner.Currency,
                MasterCategoryName = getPartnerForEditOutput.MasterCategoryName,
                PartnerTypeName = getPartnerForEditOutput.PartnerTypeName,
                WorkflowName = getPartnerForEditOutput.WorkflowName,
                CountryName = getPartnerForEditOutput.CountryName,
                PartnerCountryList = await _partnersAppService.GetAllCountryForTableDropdown(),
                PartnerMasterCategoryList = await _partnersAppService.GetAllMasterCategoryForTableDropdown(),
                PartnerPartnerTypeList = await _partnersAppService.GetAllPartnerTypeForTableDropdown(),
                PartnerWorkflowList = await _partnersAppService.GetAllWorkflowForTableDropdown(),
            };

            return View(viewModel);
        }
        public async Task<ActionResult> DetailsForm(int id)
        {
            var getPartnerForViewDto = await _partnersAppService.GetPartnerForView(id);

            var model = new PartnerViewModel()
            {
                Partner = getPartnerForViewDto.Partner
                ,
                BinaryObjectDescription = getPartnerForViewDto.BinaryObjectDescription

                ,
                MasterCategoryName = getPartnerForViewDto.MasterCategoryName

                ,
                PartnerTypeName = getPartnerForViewDto.PartnerTypeName
 
                ,
                WorkflowName = getPartnerForViewDto.WorkflowName 

                ,
                CountryName = getPartnerForViewDto.CountryName

                //,
                //CountryDisplayProperty2 = getPartnerForViewDto.CountryDisplayProperty2

            };

            return View(model);
        }
        #region Branches
        public async Task<ActionResult> Branches(int id)
        {
            var getBranchForViewDto = await _branchsAppService.GetBranchForView(id);
            var model = new BranchViewModel()
            {
                Branch = getBranchForViewDto.Branch
               ,
                CountryName = getBranchForViewDto.CountryName

            };

            return View("Branches/Branches", model);
        }

        public async Task<PartialViewResult> CreateOrEditBranchesModal(long? id)
        {
            GetBranchForEditOutput getBranchForEditOutput;
            if (id.HasValue)
            {
                getBranchForEditOutput = await _branchsAppService.GetBranchForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getBranchForEditOutput = new GetBranchForEditOutput
                {
                    Branch = new CreateOrEditBranchDto()
                };
            }
                var viewModel = new CreateOrEditBranchModalViewModel()
                {
                    Branch=getBranchForEditOutput.Branch, 
                    CountryName = getBranchForEditOutput.CountryName,
                    //PartnerId= partnerId,
                    BranchCountryList = await _branchsAppService.GetAllCountryForTableDropdown(),

                };
            
            return PartialView("Branches/_CreateOrEditBranchesModal", viewModel);
        }
        #endregion
        #region Contacts
        public async Task<PartialViewResult> CreateOrEditContactslModal(long? id)
        {
            GetPartnerContactForEditOutput getPartnerContactForEditOutput;
            if (id.HasValue)
            {
                getPartnerContactForEditOutput = await _partnerContactsAppService.GetPartnerContactForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getPartnerContactForEditOutput = new GetPartnerContactForEditOutput
                {
                    PartnerContact = new CreateOrEditPartnerContactDto()
                };
            }
            var viewModel = new CreateOrEditPartnerContactModalViewModel()
            {
                PartnerContact = getPartnerContactForEditOutput.PartnerContact,
                BranchName = getPartnerContactForEditOutput.BranchName,
                //PartnerId= partnerId,
                PartnerContactBranchList = await _partnerContactsAppService.GetAllBranchForTableDropdown(),

            };
            return PartialView("Contacts/_CreateOrEditModal", viewModel);

        }
        public async Task<ActionResult> Contacts(int id)
        {
            var getPartnerContactForViewDto = await _partnerContactsAppService.GetPartnerContactForView(id);
            var model = new PartnerContactViewModel()
            {
                PartnerContact = getPartnerContactForViewDto.PartnerContact
               ,
                BranchName = getPartnerContactForViewDto.BranchName

            };

            return View("Contacts/Contacts", model);
        }
        #endregion
        #region NotesAndTerms
        public ActionResult CreateOrEditNotesAndTermsModal()
        {

            return PartialView("NotesAndTerms/_CreateOrEditModal", "");

        }
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
        #region Agreements
        public async Task<PartialViewResult> CreateOrEditAgreementsModal(long? id)
        {
            GetPartnerContractForEditOutput getPartnerContractForEditOutput;

            if (id.HasValue)
            {
                getPartnerContractForEditOutput = await _partnerContractsAppService.GetPartnerContractForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getPartnerContractForEditOutput = new GetPartnerContractForEditOutput
                {
                    PartnerContract = new CreateOrEditPartnerContractDto()
                };
                getPartnerContractForEditOutput.PartnerContract.ContractExpiryDate = DateTime.Now;
                //getClientForEditOutput.Client.PreferedIntake = DateTime.Now;
                //getClientForEditOutput.Client.VisaExpiryDate = DateTime.Now;
            }

            var viewModel = new CreateOrEditPartnerContractModalViewModel()
            {
                PartnerContract = getPartnerContractForEditOutput.PartnerContract,
                AgentName = getPartnerContractForEditOutput.AgentName,
                RegionName = getPartnerContractForEditOutput.RegionName,
                PartnerContractAgentList = await _partnerContractsAppService.GetAllAgentForTableDropdown(),
                PartnerContractRegionList = await _partnerContractsAppService.GetAllRegionForTableDropdown(),

            };
            return PartialView("Agreements/_CreateOrEditAgreementsModal", viewModel);

        }
        public async Task<ActionResult> Agreements(int id)
        {
            var getPartnerContractForViewDto = await _partnerContractsAppService.GetPartnerContractForView(id);
            var model = new PartnerContractViewModel()
            {
                PartnerContract = getPartnerContractForViewDto.PartnerContract


            };

            return View("Agreements/Agreements", model);
        }
        #endregion
        #region Promotions
        public async Task<PartialViewResult> CreateOrEditPromotionslModal(long? id)
        {
            GetPartnerPromotionForEditOutput getPartnerPromotionForEditOutput;
            if (id.HasValue)
            {
                getPartnerPromotionForEditOutput = await _partnerPromotionsAppService.GetPartnerPromotionForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getPartnerPromotionForEditOutput = new GetPartnerPromotionForEditOutput
                {
                    PartnerPromotion = new CreateOrEditPartnerPromotionDto()
                };
                getPartnerPromotionForEditOutput.PartnerPromotion.StartDate = DateTime.Now;
                getPartnerPromotionForEditOutput.PartnerPromotion.ExpiryDate = DateTime.Now;
            }
            //var productId= new List<long>();
            //foreach (var item in getPartnerPromotionForEditOutput.PromotionProduct)
            //{
            //    productId.Add(item.ProductId);
            //}
            var viewModel = new CreateOrEditPartnerPromotionsModalViewModel()
            {
                PartnerPromotion = getPartnerPromotionForEditOutput.PartnerPromotion, 
                //ProductIdList = productId,
                PromotionProductProductList = await _promotionProductsAppService.GetAllProductForTableDropdown(id)
            };
          
            return PartialView("Promotions/_CreateOrEditPromotionslModal", viewModel);

        }
        public async Task<ActionResult> Promotions(int id)
        {
            var getPartnerPromotionForViewDto = await _partnerPromotionsAppService.GetPartnerPromotionForView(id);
            var model = new PartnerPromotionViewModel()
            {
                PartnerPromotion = getPartnerPromotionForViewDto.PartnerPromotion


            };

            return View("Promotions/Promotions", model);
        }
        #endregion
        #region Task
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
        #region Products
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

            return View("Products/AddProducts", viewModel);
        }
        //public ActionResult AddProductsDetails()
        //{

        //    //return PartialView("_ViewPartnersDetails");

        //    return View("~/Areas/AppAreaName/Views/Partners/Products/AddProducts.cshtml");

        //}
        public async Task<ActionResult> Products(int id)
        {
            var getProductForViewDto = await _productsAppService.GetProductForView(id);
            var model = new ProductViewModel()
            {
                Product = getProductForViewDto.Product


            };

            return View("Products/Products", model);
        }

        #endregion
        #region Appointments
        public async Task<ActionResult> AppointmentsIndex(int id)
        {
            var getAppointmentsForViewDto = await _appointmentsAppService.GetAppointmentForView(id);
            var model = new AppointmentViewModel()
            {
                Appointment = getAppointmentsForViewDto.Appointment


            };

            return View("Appointments/AppointmentIndex", model);
        }

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
            return PartialView("Appointments/_CreateOrEditAppointmentModal", ViewModel);

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
