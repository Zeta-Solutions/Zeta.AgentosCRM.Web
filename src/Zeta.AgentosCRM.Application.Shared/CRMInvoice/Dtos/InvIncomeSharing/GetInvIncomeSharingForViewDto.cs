using System;
using System.Collections.Generic;
using System.Text;
using Zeta.AgentosCRM.CRMInvoice.Dtos.InvPaymentReceived;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos.InvIncomeSharing
{
    public class GetInvIncomeSharingForViewDto
    {
        public InvIncomeSharingDto InvIncomeSharing { get; set; }
        public string InvoiceTaxName { get; set; }

        public string OrganizationUnitDisplayName { get; set; }
    }
}
