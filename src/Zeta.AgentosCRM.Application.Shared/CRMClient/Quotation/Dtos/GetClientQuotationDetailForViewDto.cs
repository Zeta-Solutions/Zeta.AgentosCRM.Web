namespace Zeta.AgentosCRM.CRMClient.Quotation.Dtos
{
    public class GetClientQuotationDetailForViewDto
    {
        public ClientQuotationDetailDto ClientQuotationDetail { get; set; }

        public string WorkflowName { get; set; }

        public string PartnerPartnerName { get; set; }

        public string BranchName { get; set; }

        public string ProductName { get; set; }

        public string ClientQuotationHeadClientName { get; set; }

    }
}