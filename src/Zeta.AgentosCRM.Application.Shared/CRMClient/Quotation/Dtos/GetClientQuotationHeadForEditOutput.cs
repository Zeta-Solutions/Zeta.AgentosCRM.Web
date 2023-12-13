using System.Collections.Generic; 

namespace Zeta.AgentosCRM.CRMClient.Quotation.Dtos
{
    public class GetClientQuotationHeadForEditOutput
    {
        public CreateOrEditClientQuotationHeadDto ClientQuotationHead { get; set; }

        public string ClientFirstName { get; set; }

        public string CRMCurrencyName { get; set; }

		public List<CreateOrEditClientQuotationDetailDto> ClientQuotationDetail { get; set; }

	}
}