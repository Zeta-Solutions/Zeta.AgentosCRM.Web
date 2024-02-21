using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    public class EquiriesController : AgentosCRMControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
