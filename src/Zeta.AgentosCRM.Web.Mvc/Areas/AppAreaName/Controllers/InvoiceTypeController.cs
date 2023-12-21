using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup.Account;
using Zeta.AgentosCRM.CRMSetup.Account.Dtos; 
using Zeta.AgentosCRM.Web.Controllers;
 
using Zeta.AgentosCRM.CRMSetup.Account;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.InvoiceTypes;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    public class InvoiceTypeController : AgentosCRMControllerBase
    {
        private readonly IInvoiceTypesAppService _invoiceTypesAppService;
        public InvoiceTypeController(IInvoiceTypesAppService invoiceTypesAppService)
        {
            _invoiceTypesAppService = invoiceTypesAppService;
        }
        public IActionResult Index()
        {
            var model = new InvoiceTypesViewModel()
            {
                FilterText = ""
            };
            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_InvoiceTypes_Create, AppPermissions.Pages_CRMSetup_InvoiceTypes_Edit)]

        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {

            GetInvoiceTypeForEditOutput getInvoiceTypeForEditOutput;

            if (id.HasValue)
            {
                getInvoiceTypeForEditOutput = await _invoiceTypesAppService.GetInvoiceTypeForEdit(new EntityDto { Id = (int)id });

            }
            else
            {
                getInvoiceTypeForEditOutput = new GetInvoiceTypeForEditOutput
                {
                    InvoiceType = new CreateOrEditInvoiceTypeDto()
                };
            }

            var viewModel = new CreateOrEditInvoiceTypeModalViewModel()
            {
                InvoiceType = getInvoiceTypeForEditOutput.InvoiceType,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewDocumentTypeModal(int id)
        {
            var getInvoiceTypeForViewDto = await _invoiceTypesAppService.GetInvoiceTypeForView(id);
            var model = new InvoiceTypeViewModel()
            {
                InvoiceType = getInvoiceTypeForViewDto.InvoiceType
            };

            return PartialView("_ViewInvoiceTypeModal", model);
        }

    }
}
