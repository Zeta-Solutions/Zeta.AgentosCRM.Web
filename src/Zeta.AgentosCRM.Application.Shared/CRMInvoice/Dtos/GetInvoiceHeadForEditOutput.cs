using System;
using System.Collections.Generic;
using System.Text;
using Zeta.AgentosCRM.CRMInvoice.Dtos.InvIncomeSharing;
using Zeta.AgentosCRM.CRMInvoice.Dtos.InvPaymentReceived;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos
{
    public class GetInvoiceHeadForEditOutput
    {
        public CreateOrEditInvoiceHeadDto InvoiceHead { get; set; }
        public List<CreateOrEditInvoiceDetailDto> InvoiceDetail { get; set; }
        public List<CreateOrEditInvPaymentReceivedDto> InvPaymentReceived { get; set; }
        public List<CreateOrEditInvIncomeSharingDto> InvIncomeSharing { get; set; }
        public string InvoiceManualPaymentDetailName { get; set; }
        public string InvoiceCurrencyName { get; set; }
    }
}
