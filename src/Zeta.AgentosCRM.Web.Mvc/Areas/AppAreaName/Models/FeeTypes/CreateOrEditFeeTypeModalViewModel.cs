 
using Zeta.AgentosCRM.CRMSetup.FeeType.Dtos;

using Abp.Extensions;
namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.FeeTypes
{
    public class CreateOrEditFeeTypeModalViewModel
    {
        public CreateOrEditFeeTypeDto FeeType { get; set; }

        public bool IsEditMode => FeeType.Id.HasValue;

    }
}
