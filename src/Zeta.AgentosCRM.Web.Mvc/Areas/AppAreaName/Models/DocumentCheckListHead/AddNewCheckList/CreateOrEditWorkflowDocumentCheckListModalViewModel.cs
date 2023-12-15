using System.Collections.Generic; 
using Zeta.AgentosCRM.CRMSetup.Documents.Dtos;
using Zeta.AgentosCRM.CRMSetup.Documents.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.WorkflowDocumentCheckList
{
    public class CreateOrEditWorkflowDocumentCheckListModalViewModel
    { 
        public CreateOrEditWorkflowStepDocumentCheckListDto WorkflowDocumentCheckList { get; set; }
         
        public List<WorkflowStepDocumentCheckListDocumentTypeLookupTableDto> WorkFlowDocumentCheckListDoucType { get; set; }
        public bool IsEditMode => WorkflowDocumentCheckList.Id.HasValue;
    }
}
