using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Controllers;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.SubjectArea;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_SubjectAreas)]
    public class SubjectAreaController : AgentosCRMControllerBase
    {
        private readonly ISubjectAreasAppService _subjectAreasAppService;
        public SubjectAreaController(ISubjectAreasAppService subjectAreasAppService)
        {
            _subjectAreasAppService = subjectAreasAppService;
        }

        public IActionResult Index()
        {
            var model=new SubjectAreasViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        public async Task<PartialViewResult> ViewSubjectAreaModal(int id)
        {
            var getSubjectAreaForViewDto = await _subjectAreasAppService.GetSubjectAreaForView(id);
            var model=new SubjectAreaViewModel()
            {
                SubjectArea = getSubjectAreaForViewDto.SubjectArea
            };
            return PartialView("_ViewSubjectAreaModal", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_SubjectAreas_Create, AppPermissions.Pages_CRMSetup_SubjectAreas_Edit)]

        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetSubjectAreaForEditOutput getSubjectAreaForEditOutput;
            if (id.HasValue)
            {
                getSubjectAreaForEditOutput = await _subjectAreasAppService.GetSubjectAreaForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getSubjectAreaForEditOutput = new GetSubjectAreaForEditOutput
                {
                    SubjectArea = new CreateOrEditSubjectAreaDto()
                };
            }
            var viewModel = new CreateOrEditSubjectAreaModalViewModel()
            {
                SubjectArea = getSubjectAreaForEditOutput.SubjectArea,

            };
            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}
