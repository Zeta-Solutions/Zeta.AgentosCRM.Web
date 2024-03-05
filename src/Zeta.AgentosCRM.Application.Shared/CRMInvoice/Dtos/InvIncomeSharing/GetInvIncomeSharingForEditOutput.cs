using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.CRMInvoice.Dtos.InvIncomeSharing
{
    public class GetInvIncomeSharingForEditOutput
    {
        public CreateOrEditInvIncomeSharingDto InvIncomeSharing { get; set; }
        public string InvoiceTaxName { get; set; }

        public string OrganizationUnitDisplayName { get; set; }
    }
}
