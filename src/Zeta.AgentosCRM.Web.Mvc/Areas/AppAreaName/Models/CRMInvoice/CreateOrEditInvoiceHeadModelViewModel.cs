using System.Collections.Generic;
using Zeta.AgentosCRM.CRMInvoice.Dtos;
using Zeta.AgentosCRM.CRMLead.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.CRMInvoice
{
    public class CreateOrEditInvoiceHeadModelViewModel
    {
        public CreateOrEditInvoiceHeadDto InvoiceHead { get; set; }
        public string InvoiceManualPaymentDetailName { get; set; }
        public string InvoiceCurrencyName { get; set; }
        public List<InvoiceCurrencyLookupTableDto> InvoiceCurrencyList  { get; set; }
        public List<InvoicePaymentLookupTableDto> InvoicePaymentList { get; set; }
        public bool IsEditMode => InvoiceHead.Id.HasValue;
    }
}
