using System.Collections.Generic; 
using Zeta.AgentosCRM.CRMSetup.Document.Dtos; 

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.WorkflowDocument
{
    public class CreateOrEditWorkflowDocumentModalViewModel
    { 
        public CreateOrEditWorkflowDocumentDto WorkflowDocument { get; set; }

        public List<WorkflowDocumentWorkflowLookupTableDto> WorkFlowList { get; set; }
        
        public bool IsEditMode => WorkflowDocument.Id.HasValue;
    }
}
