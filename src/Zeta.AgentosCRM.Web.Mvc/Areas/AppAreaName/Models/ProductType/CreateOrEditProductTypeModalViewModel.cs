using Zeta.AgentosCRM.CRMSetup.ProductType.Dtos;

using Abp.Extensions;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMSetup.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.ProductType
{
    public class CreateOrEditProductTypeModalViewModel
    {
        public CreateOrEditProductTypeDto  ProductType { get; set; }
        public string MasterCategoryName { get; set; }

        public List<ProductTypeMasterCategoryLookupTableDto> ProductTypeMasterCategoryList { get; set; }
        public bool IsEditMode => ProductType.Id.HasValue;  
    }
}
