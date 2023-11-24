using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup.Email;
using Zeta.AgentosCRM.CRMSetup.Email.Dtos; 
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.EmailTemplates;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    public class EmailTemplateController : AgentosCRMControllerBase
    {
        private readonly IEmailTemplatesAppService _emailTemplatesAppService;

        public EmailTemplateController(IEmailTemplatesAppService emailTemplatesAppService)
        {
            _emailTemplatesAppService = emailTemplatesAppService;

        }
        public IActionResult Index()
        {
            var model = new EmailTemplatesViewModel()
            {
                FilterText = ""
            };
            return View(model);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_EmailTemplates_Create, AppPermissions.Pages_EmailTemplates_Edit)]

        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {

            GetEmailTemplateForEditOutput getEmailTemplateForEditOutput;

            if (id.HasValue)
            {
                getEmailTemplateForEditOutput = await _emailTemplatesAppService.GetEmailTemplateForEdit(new EntityDto { Id = (int)id });
                  
            }
            else
            {
                getEmailTemplateForEditOutput = new GetEmailTemplateForEditOutput
                {
                    EmailTemplate = new CreateOrEditEmailTemplateDto()
                };
            }

            var viewModel = new CreateOrEditEmailTemplateModalViewModel()
            {
                EmailTemplate= getEmailTemplateForEditOutput.EmailTemplate,

            };
            return PartialView("_CreateOrEditModal", viewModel);
        }

        //public async Task<PartialViewResult> ViewFeeTypeModal(int id)
        //{
        //    var getFeeTypeForViewDto = await _emailTemplatesAppService.GetEmailTemplateForView(id);
        //    var model = new EmailTemplateViewModel()
        //    {
        //        EmailTemplate= getFeeTypeForViewDto.EmailTemplate
        //    };

        //    return PartialView("_ViewEmailTemplateModal", model);
        //}
    }

}
