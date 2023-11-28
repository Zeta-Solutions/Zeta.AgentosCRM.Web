namespace Zeta.AgentosCRM.CRMSetup.Documents.Dtos
{
    public class GetDocumentCheckListProductForViewDto
    {
        public DocumentCheckListProductDto DocumentCheckListProduct { get; set; }

        public string ProductName { get; set; }

        public string WorkflowStepDocumentCheckListName { get; set; }

    }
}