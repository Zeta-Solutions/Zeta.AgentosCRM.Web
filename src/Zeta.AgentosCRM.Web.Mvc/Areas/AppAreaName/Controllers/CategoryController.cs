using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Controllers;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.MasterCategories;
using System.Threading.Tasks;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    public class CategoryController : AgentosCRMControllerBase
    { 
        public IActionResult Index()
        {  
            return View();
           // return View();
        }
        public async Task<PartialViewResult> ViewCategoryModal(int id)
        { 
            return PartialView("_ViewCategoryModal", "");
        }
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            return PartialView("_CreateOrEditModal", "");
        }
     
    }
}
