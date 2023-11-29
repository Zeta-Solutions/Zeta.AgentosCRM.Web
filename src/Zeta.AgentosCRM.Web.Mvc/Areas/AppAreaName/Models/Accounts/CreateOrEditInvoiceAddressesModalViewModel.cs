 
using Zeta.AgentosCRM.CRMSetup.Account.Dtos;

using Abp.Extensions;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMAgent.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Accounting
{
    public class CreateOrEditInvoiceAddressModalViewModel
    {
        public CreateOrEditInvoiceAddressDto InvoiceAddress { get; set; }

        public bool IsEditMode => InvoiceAddress.Id.HasValue;
        public List<InvoiceAddressOrganizationUnitLookupTableDto> InvoiceAddressOrganizationUnitList { get; set; }
        public string  OrganizationUnitName { get; set; }

    }
}
