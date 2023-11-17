using System.Collections.Generic;
using Zeta.AgentosCRM.CRMProducts.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Product
{
    public class CreateOrEditProductModalViewModel
    {
        public CreateOrEditProductDto Product { get; set; }
        public string PartnerName { get; set; }
        public string PartnerTypeName { get; set; }
        public string BranchName { get; set; }

        public List<ProductPartnerLookupTableDto> ProductPartnerList { get; set; }
        public List<ProductPartnerTypeLookupTableDto> ProductPartnerTypeList { get; set; }
        public List<ProductBranchLookupTableDto> ProductBranchList { get; set; }
        public bool IsEditMode => Product.Id.HasValue;
    }
}
