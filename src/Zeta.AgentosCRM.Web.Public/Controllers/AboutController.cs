using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Public.Controllers
{
    public class AboutController : AgentosCRMControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}