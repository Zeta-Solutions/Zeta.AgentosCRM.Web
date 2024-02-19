using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos
{
    public class GetInvoiceDetailForEditOutput
    {
        public CreateOrEditInvoiceDetailDto InvoiceDetail { get; set; }
        public string InvoiceTaxName { get; set; }

        public string InvoiceIncomeTypeName { get; set; }
    }
}
