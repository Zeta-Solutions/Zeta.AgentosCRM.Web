namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class GetWorkflowStepDocumentCheckListForViewDto
    {
        public WorkflowStepDocumentCheckListDto WorkflowStepDocumentCheckList { get; set; }

        public string WorkflowStepName { get; set; }

        public string DocumentTypeName { get; set; }

    }
}