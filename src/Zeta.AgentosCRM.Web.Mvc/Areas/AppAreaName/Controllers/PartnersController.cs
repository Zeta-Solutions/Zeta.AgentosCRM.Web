using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.LeadSource;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    public class PartnersController : AgentosCRMControllerBase
    {
        public IActionResult Index()
        {
            return View();
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

    }
}
