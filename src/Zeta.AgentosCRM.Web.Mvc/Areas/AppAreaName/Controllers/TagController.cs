using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup.Tag;
using Zeta.AgentosCRM.CRMSetup.Tag.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Tag;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{ 
    [Area("AppAreaName")]

    [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_Tags)]
    public class TagController : AgentosCRMControllerBase
    {
        private readonly ITagsAppService _tagsAppService;

        public TagController(ITagsAppService tagsAppService)
        {
            _tagsAppService = tagsAppService;
        }

        public IActionResult Index()
        {
            var model = new TagsViewModel()
            {
                FilterText = ""
            };
            return View(model);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_Tags_Create , AppPermissions.Pages_CRMSetup_Tags_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetTagForEditOutput getTagForEditOutput;
            if (id.HasValue)
            {
                getTagForEditOutput=await _tagsAppService.GetTagForEdit(new EntityDto {Id = (int)id});
            }
            else
            {
                getTagForEditOutput = new GetTagForEditOutput
                {
                    Tag = new CreateOrEditTagDto()
                };
            }
            var ViewModel = new CreateOrEditTagModalViewModel
            {
                Tag = getTagForEditOutput.Tag,
            };


            return PartialView("_CreateOrEditModal", ViewModel);
        }
        public async Task<PartialViewResult> ViewTagModal(int id)
        {
            var getTagForViewDto = await _tagsAppService.GetTagForView(id);

           var model = new TagViewModel()
           {
               Tag = getTagForViewDto.Tag
           };

            return PartialView("_ViewTagModal", model);
        }
       
    }
}
