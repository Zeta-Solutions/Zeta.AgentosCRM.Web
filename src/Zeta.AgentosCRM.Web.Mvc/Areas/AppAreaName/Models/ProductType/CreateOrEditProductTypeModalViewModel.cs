using Zeta.AgentosCRM.CRMSetup.ProductType.Dtos;

using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.ProductType
{
    public class CreateOrEditProductTypeModalViewModel
    {
        public CreateOrEditProductTypeDto  ProductType { get; set; }
        public bool IsEditMode => ProductType.Id.HasValue;  
    }
}
