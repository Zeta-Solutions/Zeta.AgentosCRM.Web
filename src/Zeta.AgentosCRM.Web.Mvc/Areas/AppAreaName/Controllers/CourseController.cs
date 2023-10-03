using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    public class CourseController : AgentosCRMControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> ViewCourseModal(int id)
        {
            return PartialView("_ViewCourseModal", "");
        }
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            return PartialView("_CreateOrEditModal", "");
        }
    }
}
