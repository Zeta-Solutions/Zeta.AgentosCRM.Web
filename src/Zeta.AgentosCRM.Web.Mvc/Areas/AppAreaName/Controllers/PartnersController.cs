using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMPartner.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.LeadSource;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Partners;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    public class PartnersController : AgentosCRMControllerBase
    {
        private readonly IPartnersAppService _partnersAppService;
        public PartnersController(IPartnersAppService partnersAppService)
        {
            _partnersAppService = partnersAppService;
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
        public ActionResult CreateOrEditBranchesModal(int? id)
        {

            return PartialView("Branches/_CreateOrEditModal", "");
        }
        public ActionResult ViewApplicationDetails()
        {

            return PartialView("~/Areas/AppAreaName/Views/Partners/Applications/_Application.cshtml");

        }
        public ActionResult CreateOrEditEmailModal()
        {

            return PartialView("ComposeEmail/_CreateOrEditModal", "");

        } 
        public ActionResult CreateOrEditContactslModal()
        {

            return PartialView("Contacts/_CreateOrEditModal", "");

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
                PartnerName = getPartnerForEditOutput.Partner.PartnerName,
                //BusinessRegistrationNumber = getPartnerForEditOutput.BusinessRegistrationNumber,
                Street = getPartnerForEditOutput.Partner.Street,
                City = getPartnerForEditOutput.Partner.City,
                State = getPartnerForEditOutput.Partner.State,
                Zipcode = getPartnerForEditOutput.Partner.ZipCode,
                Phone = getPartnerForEditOutput.Partner.PhoneNo,
                Email = getPartnerForEditOutput.Partner.Email,
                Fax = getPartnerForEditOutput.Partner.Fax,
                Website = getPartnerForEditOutput.Partner.Website,
                MarketingOfficeEmail = getPartnerForEditOutput.Partner.MarketingEmail,
                University = getPartnerForEditOutput.Partner.University,
                //Currency = getPartnerForEditOutput.Partner.Currency,
                MasterCategoryName = getPartnerForEditOutput.MasterCategoryName,
                PartnerTypeName = getPartnerForEditOutput.PartnerTypeName,
                WorkflowName = getPartnerForEditOutput.WorkflowName,
                CountryName2 = getPartnerForEditOutput.CountryName,
                PartnerCountryList = await _partnersAppService.GetAllCountryForTableDropdown(),
                PartnerMasterCategoryList = await _partnersAppService.GetAllMasterCategoryForTableDropdown(),
                PartnerPartnerTypeList = await _partnersAppService.GetAllPartnerTypeForTableDropdown(),
                PartnerWorkflowList = await _partnersAppService.GetAllWorkflowForTableDropdown(),
            };

            return View(viewModel);
        }
        public async Task<ActionResult> ViewPartner(int id)
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
                 

            };

            return View(model);
        }
    }
}
