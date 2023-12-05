using Zeta.AgentosCRM.CRMProducts.Fee.Dtos;
using System.Collections.Generic;
using System.Collections.Generic;

using Abp.Extensions;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.ProductFeeDetails
{
    public class CreateOrEditProductFeeDetailModalViewModel
    {
        public CreateOrEditProductFeeDetailDto ProductFeeDetail { get; set; }

        public string FeeTypeName { get; set; }

        public string ProductFeeName { get; set; }

        public List<ProductFeeDetailFeeTypeLookupTableDto> ProductFeeDetailFeeTypeList { get; set; }

        public List<ProductFeeDetailProductFeeLookupTableDto> ProductFeeDetailProductFeeList { get; set; }

        public bool IsEditMode => ProductFeeDetail.Id.HasValue;
    }
}