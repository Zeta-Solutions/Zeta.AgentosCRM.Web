using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Tenants.Email.Configuration;
using Zeta.AgentosCRM.Tenants.Email.Configuration.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.EmailConfigurations;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    public class EmailConfigurationsController : AgentosCRMControllerBase
    {
        private readonly IEmailConfigurationsAppService _emailConfigurationsAppService;

        public EmailConfigurationsController(IEmailConfigurationsAppService emailConfigurationsAppService)
        {
            _emailConfigurationsAppService = emailConfigurationsAppService;
        }

        public IActionResult Index()
        {
            var model =new EmailConfigurationsViewModel
            {
                FilterText = ""
            };
            return View(model);
        }
            public async Task<PartialViewResult> ViewEmailConfigurations (int id)
            {
                var getEmailConfigurationForViewDto = await _emailConfigurationsAppService.GetEmailConfigurationForView(id);
                var model = new EmailConfigurationViewModel()
                {
                    EmailConfiguration = getEmailConfigurationForViewDto.EmailConfiguration,
                };
                return PartialView("_ViewSubjectAreaModal", model);
            }
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetEmailConfigurationForEditOutput getEmailConfigurationForEditOutput;
            if (id.HasValue)
            {
                getEmailConfigurationForEditOutput = await _emailConfigurationsAppService.GetEmailConfigurationForEdit(new EntityDto<long> { Id = (long)id });
            }
            else
            {
                getEmailConfigurationForEditOutput = new GetEmailConfigurationForEditOutput
                {
                    EmailConfiguration = new CreateOrEditEmailConfigurationDto()
                };
            }
            var viewModel = new CreateOrEditEmailConfigurationModelViewModel()
            {
                EmailConfiguration = getEmailConfigurationForEditOutput.EmailConfiguration,

            };
            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}
