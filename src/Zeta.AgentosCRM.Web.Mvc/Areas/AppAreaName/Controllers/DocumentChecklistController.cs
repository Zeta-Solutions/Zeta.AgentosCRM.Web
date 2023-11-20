
using Microsoft.AspNetCore.Mvc;

using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    public class DocumentChecklistController : AgentosCRMControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateOrEditModal()
        {
            return PartialView("_CreateOrEditModal", "");
        }
    }
}
