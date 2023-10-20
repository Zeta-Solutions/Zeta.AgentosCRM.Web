using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    public class ClientController : AgentosCRMControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ClientDetail()
        {
            return View();
        } 
        public IActionResult ClientCreateDetail()
        {
            return View();
        }       
        public IActionResult ClientEmailCompose()
        {
            return PartialView("_ClientEmailCompose", "");
        }
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            return PartialView("_CreateOrEditModal", "");
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
