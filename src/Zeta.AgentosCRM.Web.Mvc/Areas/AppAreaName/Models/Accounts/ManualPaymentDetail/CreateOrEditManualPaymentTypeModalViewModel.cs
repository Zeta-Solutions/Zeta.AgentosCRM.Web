 
using Zeta.AgentosCRM.CRMSetup.Account.Dtos;

using Abp.Extensions;
namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Accounts.ManualPaymentDetail
{
    public class CreateOrEditManualPaymentTypeModalViewModel
    {
        public CreateOrEditManualPaymentDetailDto ManualPaymentDetail { get; set; }

        public bool IsEditMode => ManualPaymentDetail.Id.HasValue;

    }
}
