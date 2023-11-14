 
using Zeta.AgentosCRM.CRMSetup.CRMCurrency.Dtos;

using Abp.Extensions;
namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.CRMCurrencies
{
    public class CreateOrEditCRMCurrencyModalViewModel
    {
        public CreateOrEditCRMCurrencyDto CRMCurrency { get; set; }

        public bool IsEditMode => CRMCurrency.Id.HasValue;

    }
}
