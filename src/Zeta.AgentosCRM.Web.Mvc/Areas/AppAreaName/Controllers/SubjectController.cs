using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.PartnerTypes;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Subject;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Subjects)]
    public class SubjectController : AgentosCRMControllerBase
    {
        private readonly ISubjectsAppService _subjectsAppService;

        public SubjectController(ISubjectsAppService subjectsAppService)
        {
            _subjectsAppService = subjectsAppService;
        }
        public IActionResult Index()
        {
            var model=new SubjectsViewModel
            {
                FilterText = ""
            };
            return View(model);
        }
        public async Task<PartialViewResult> ViewSubjectModal(int id)
        {
            var getSubjectForViewDto=await _subjectsAppService.GetSubjectForView(id);
            var model = new SubjectViewModel()
            {
                Subject = getSubjectForViewDto.Subject
               ,
                //SubjectAreaName = getSubjectForViewDto.SubjectAreaName

            };
            return PartialView("_ViewSubjectModal", model);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Subjects_Create, AppPermissions.Pages_Subjects_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetSubjectForEditOutput getSubjectForEditOutput;
            if (id.HasValue)
            {
                getSubjectForEditOutput = await _subjectsAppService.GetSubjectForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getSubjectForEditOutput = new GetSubjectForEditOutput
                {
                    Subject = new CreateOrEditSubjectDto()
                };
            }
            var viewModel = new CreateOrEditSubjectModalViewModel()
            {
                Subject = getSubjectForEditOutput.Subject,
                //SubjectAreaName = getSubjectForEditOutput.SubjectAreaName,
                //SubjectSubjectAreaList = await _subjectsAppService.GetAllSubjectAreaForTableDropdown(),

            };
            return PartialView("_CreateOrEditModal", viewModel);
        }

    }
}
