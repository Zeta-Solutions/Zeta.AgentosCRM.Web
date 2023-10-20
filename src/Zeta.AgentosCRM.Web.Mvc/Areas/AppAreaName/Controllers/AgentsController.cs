using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.CRMSetup.FeeType;
using Zeta.AgentosCRM.CRMSetup.FeeType.Dtos;
//using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.FeeTypes;
//using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.MasterCategories;
using Zeta.AgentosCRM.Web.Controllers;


namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]

    public class AgentsController :  AgentosCRMControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddAgentdetail()
        {
            return View("~/Areas/AppAreaName/Views/Agents/AddAgents/Index.cshtml");
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
    }
}
