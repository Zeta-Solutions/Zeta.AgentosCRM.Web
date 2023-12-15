
using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Controllers;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMSetup.Documents;
using Zeta.AgentosCRM.CRMSetup.Documents.Dtos;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.WorkflowDocument;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.DocumentTypes;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.WorkflowDocumentCheckList;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Controllers
{

    [Area("AppAreaName")]
    public class DocumentChecklistController : AgentosCRMControllerBase
    { 
        private readonly IWorkflowDocumentsAppService _workflowDocumentsAppService;
        private readonly IWorkflowStepDocumentCheckListsAppService _workflowStepDocumentCheckListsAppService;
        private readonly IDocumentCheckListPartnersAppService _documentCheckListPartnersAppService;
        private readonly IDocumentCheckListProductsAppService _documentCheckListProductsAppService;
        public DocumentChecklistController(IWorkflowDocumentsAppService workflowDocumentsAppService,IWorkflowStepDocumentCheckListsAppService workflowStepDocumentCheckListsAppService,IDocumentCheckListPartnersAppService documentCheckListPartnersAppService,IDocumentCheckListProductsAppService documentCheckListProductsAppService)
        {
            _workflowDocumentsAppService = workflowDocumentsAppService;
            _workflowStepDocumentCheckListsAppService = workflowStepDocumentCheckListsAppService;
            _documentCheckListPartnersAppService = documentCheckListPartnersAppService;
            _documentCheckListProductsAppService = documentCheckListProductsAppService;
        }
        public IActionResult Index()
        {
            var model = new WorkflowDocumentsViewModel()
            {
                FilterText = ""
            };
            return View(model);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_WorkflowDocuments_Create, AppPermissions.Pages_WorkflowDocuments_Edit)]

        public async Task<PartialViewResult>   CreateOrEditModal(int? id)
        {
            GetWorkflowDocumentForEditOutput getWorkflowDocumentForEditOutput;
             
            if (id.HasValue)
            {
                getWorkflowDocumentForEditOutput = await _workflowDocumentsAppService.GetWorkflowDocumentForEdit(new EntityDto { Id = (int)id });

            }
            else
            {
                getWorkflowDocumentForEditOutput = new GetWorkflowDocumentForEditOutput
                {
                    WorkflowDocument = new CreateOrEditWorkflowDocumentDto()
                };
            }

            var viewModel = new CreateOrEditWorkflowDocumentModalViewModel()
            {
                WorkflowDocument = getWorkflowDocumentForEditOutput.WorkflowDocument,

                WorkFlowList = await _workflowDocumentsAppService.GetAllWorkflowForTableDropdown(),
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }


        public async Task<PartialViewResult> ViewDocumentCheckListHeadModal(int id)
        {
            var getDocumentCheckListHeadForViewDto = await _workflowDocumentsAppService.GetWorkflowDocumentForView(id);
            var model = new WorkflowDocumentViewModel()
            {
                WorkflowDocument = getDocumentCheckListHeadForViewDto.WorkflowDocument
            };

            return PartialView("_ViewDocumentTypeModal", model);
        }

        public IActionResult EditDocumentCheckList()
        {
            return View("EditDocumentCheckListView", "");
        }
        public IActionResult AddCheckList()
        {
            return PartialView("AddNewCheckList/_AddNewCheckList", "");
        }
        [AbpMvcAuthorize(AppPermissions.Pages_WorkflowStepDocumentCheckLists_Create, AppPermissions.Pages_WorkflowStepDocumentCheckLists_Edit)]

        public async Task<PartialViewResult> CreateOrEditCheckListModal(int? id)
        {
            GetWorkflowStepDocumentCheckListForEditOutput getWorkflowStepDocumentCheckListForEditOutput;
              
            if (id.HasValue)
            {
                getWorkflowStepDocumentCheckListForEditOutput = await _workflowStepDocumentCheckListsAppService.GetWorkflowStepDocumentCheckListForEdit(new EntityDto { Id = (int)id });

            }
            else
            {
                getWorkflowStepDocumentCheckListForEditOutput = new GetWorkflowStepDocumentCheckListForEditOutput
                {
                    WorkflowStepDocumentCheckList = new CreateOrEditWorkflowStepDocumentCheckListDto()
                };
            }
            var viewModel = new CreateOrEditWorkflowDocumentCheckListModalViewModel
            {
                WorkflowDocumentCheckList = getWorkflowStepDocumentCheckListForEditOutput.WorkflowStepDocumentCheckList, 
                 WorkFlowDocumentCheckListDoucType = await _workflowStepDocumentCheckListsAppService.GetAllDocumentTypeForTableDropdown(),
            };
             
            return PartialView("AddNewCheckList/_CreateOrEditCheckListModal", viewModel);
        }


    }
}
