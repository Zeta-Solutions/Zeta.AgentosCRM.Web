using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos
{
    public class GetInvoiceHeadForViewDto
    {
       public InvoiceHeadDto InvoiceHead { get; set; }    
       public string InvoiceManualPaymentDetailName { get; set; }
        public string InvoiceCurrencyName { get; set; }
        public string InvoiceClientName { get; set; }
        public string InvoicePartnerName { get; set; }
        public string InvoiceApplicationName { get; set; }
    }
}
