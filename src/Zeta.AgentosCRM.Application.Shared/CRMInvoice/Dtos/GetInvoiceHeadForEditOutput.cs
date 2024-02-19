using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos
{
    public class GetInvoiceHeadForEditOutput
    {
        public CreateOrEditInvoiceHeadDto InvoiceHead { get; set; }
        public List<CreateOrEditInvoiceDetailDto> InvoiceDetail { get; set; }
        public string InvoiceManualPaymentDetailName { get; set; }
        public string InvoiceCurrencyName { get; set; }
    }
}
