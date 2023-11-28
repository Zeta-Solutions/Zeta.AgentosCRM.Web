 
using Zeta.AgentosCRM.CRMSetup.Account.Dtos;

using Abp.Extensions;
namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.InvoiceTypes
{
    public class CreateOrEditInvoiceTypeModalViewModel
    {
        public CreateOrEditInvoiceTypeDto InvoiceType { get; set; }

        public bool IsEditMode => InvoiceType.Id.HasValue;

    }
}
