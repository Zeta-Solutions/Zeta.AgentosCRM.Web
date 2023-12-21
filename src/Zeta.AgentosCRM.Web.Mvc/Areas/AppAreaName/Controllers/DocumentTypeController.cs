using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup.Documents;
using Zeta.AgentosCRM.CRMSetup.Documents.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.DocumentTypes;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.FeeTypes;
using Zeta.AgentosCRM.Web.Controllers;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    public class DocumentTypeController : AgentosCRMControllerBase
    {
        private readonly IDocumentTypesAppService _documentTypesAppService;
        public DocumentTypeController(IDocumentTypesAppService documentTypesAppService)
        {
            _documentTypesAppService = documentTypesAppService;
        }
        public IActionResult Index()
        {
            var model = new DocumentTypesViewModel()
            {
                FilterText = ""
            };
            return View(model);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_CRMSetup_DocumentTypes_Create, AppPermissions.Pages_CRMSetup_DocumentTypes_Edit)]

        public async Task<PartialViewResult> CreateOrEditModal(int ?id )
        {

            GetDocumentTypeForEditOutput getDocumentTypeForEditOutput;

            if (id.HasValue)
            {
                getDocumentTypeForEditOutput = await _documentTypesAppService.GetDocumentTypeForEdit(new EntityDto { Id = (int)id });
                
            }
            else
            {
                getDocumentTypeForEditOutput = new GetDocumentTypeForEditOutput
                {
                    DocumentType = new CreateOrEditDocumentTypeDto()
                };
            }

            var viewModel = new CreateOrEditDocumentTypeModalViewModel()
            {
                DocumentType = getDocumentTypeForEditOutput.DocumentType,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewDocumentTypeModal(int id)
        {
            var getDocumentTypeForViewDto = await _documentTypesAppService.GetDocumentTypeForView(id);
            var model = new DocumentTypeViewModel()
            {
                DocumentType = getDocumentTypeForViewDto.DocumentType
            };

            return PartialView("_ViewDocumentTypeModal", model);
        }
    }
}
