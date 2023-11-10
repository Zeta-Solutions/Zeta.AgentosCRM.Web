﻿using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMPartner.Contact;
using Zeta.AgentosCRM.CRMPartner.Contact.Dtos;
using Zeta.AgentosCRM.CRMPartner.Dtos;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.LeadSource;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.PartnerBranch;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Partners;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    public class PartnersController : AgentosCRMControllerBase
    {
        private readonly IPartnersAppService _partnersAppService;
        private readonly IBranchesAppService _branchsAppService;
        private readonly IPartnerContactsAppService _partnerContactsAppService;
        public PartnersController(IPartnersAppService partnersAppService, IBranchesAppService branchsAppService, IPartnerContactsAppService partnerContactsAppService)
        {
            _partnersAppService = partnersAppService;
            _branchsAppService = branchsAppService;
            _partnerContactsAppService = partnerContactsAppService;
        }
      
        public IActionResult Index()
        {
            var model= new PartnersViewModel
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
        public ActionResult CreateOrEditTasksModal()
        {

            return PartialView("Tasks/_CreateOrEditModal", "");

        }
        public ActionResult CreateOrEditPromotionsModal()
        {

            return PartialView("Promotions/_CreateOrEditModal", "");

        }
        public ActionResult CreateOrEditNotesAndTermsModal()
        {

            return PartialView("NotesAndTerms/_CreateOrEditModal", "");

        }
        public ActionResult AddPartnersDetails()
        {

           //return PartialView("_ViewPartnersDetails");

            return View("~/Areas/AppAreaName/Views/Partners/InformationsDetails.cshtml");

        }
        public ActionResult AddProductsDetails()
        {

            //return PartialView("_ViewPartnersDetails");

            return View("~/Areas/AppAreaName/Views/Partners/Products/AddProducts.cshtml");

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


    }
}
