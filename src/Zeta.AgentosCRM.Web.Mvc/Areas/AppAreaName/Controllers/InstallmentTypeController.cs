using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.MasterCategories;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    public class InstallmentTypeController : AgentosCRMControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {


            return PartialView("_CreateOrEditModal", "");
        }

        public async Task<PartialViewResult> ViewInstallmentTypeModal(int id)
        {
            

            return PartialView("_ViewInstallmentTypeModal", "");
        }

    }
}
