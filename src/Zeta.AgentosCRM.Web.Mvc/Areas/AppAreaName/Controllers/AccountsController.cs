using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMSetup.Account;
using Zeta.AgentosCRM.CRMSetup.Account.Dtos;
using Zeta.AgentosCRM.CRMSetup.Documents;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Accounting;
using Zeta.AgentosCRM.Web.Controllers;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Accounts.ManualPaymentDetail; 
using Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos; 

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    public class AccountsController : AgentosCRMControllerBase
    {
        readonly IBusinessRegNummbersAppService _businessRegNummbersAppService;
        readonly IManualPaymentDetailsAppService _manualPaymentDetailsAppService;  
        public AccountsController(IBusinessRegNummbersAppService businessRegNummbersAppService,IManualPaymentDetailsAppService manualPaymentDetailsAppService)
        {
            _businessRegNummbersAppService = businessRegNummbersAppService;
            _manualPaymentDetailsAppService= manualPaymentDetailsAppService;
        }
        public IActionResult Index()
        {
            return View();
        } 

        [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_ManualPaymentDetails_Create, AppPermissions.Pages_CRMSetup_ManualPaymentDetails_Edit)]

        
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetManualPaymentDetailForEditOutput getManualPaymentDetailForEditOutput;
              
            if (id.HasValue)
            {
                getManualPaymentDetailForEditOutput = await _manualPaymentDetailsAppService.GetManualPaymentDetailForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getManualPaymentDetailForEditOutput= new GetManualPaymentDetailForEditOutput
                {
                    ManualPaymentDetail = new CreateOrEditManualPaymentDetailDto()
                };
            }
            var viewModel = new CreateOrEditManualPaymentTypeModalViewModel()
            {
                ManualPaymentDetail = getManualPaymentDetailForEditOutput.ManualPaymentDetail,
                
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}
