namespace Zeta.AgentosCRM.CRMClient.Quotation.Dtos
{
    public class GetClientQuotationHeadForViewDto
    {
        public ClientQuotationHeadDto ClientQuotationHead { get; set; }

        public string ClientFirstName { get; set; }

        public string CRMCurrencyName { get; set; }

        public string UserName { get; set; }

    }
}