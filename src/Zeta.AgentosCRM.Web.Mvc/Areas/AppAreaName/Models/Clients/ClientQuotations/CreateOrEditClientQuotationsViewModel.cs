using System.Collections.Generic;
using Zeta.AgentosCRM.CRMClient.Quotation.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Clients.ClientQuotations
{
    public class CreateOrEditClientQuotationsViewModel
    {
        public CreateOrEditClientQuotationHeadDto ClientQuotationHead { get; set; }
		public List<CreateOrEditClientQuotationDetailDto> clientQuotationDeatils { get; set; }

		public List<ClientQuotationHeadClientLookupTableDto> QuotationHeadClientList { get; set; }
        public List<ClientQuotationHeadCRMCurrencyLookupTableDto> QuotationHeadCRMCurrencyList { get; set; }
        public bool IsEditMode => ClientQuotationHead.Id.HasValue;
    }
}
