using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos
{
    public class GetInvoiceDetailForViewDto
    {
        public InvoiceDetailDto InvoiceDetail { get; set; }
        public string InvoiceTaxName { get; set; }

        public string InvoiceIncomeTypeName { get; set; }
    }
}
