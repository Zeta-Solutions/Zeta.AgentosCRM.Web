using System.Collections.Generic;
using Zeta.AgentosCRM.CRMProducts.Fee.Dtos;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Product.Fee
{
    public class CreateOrEditProductFeeModalViewModel
    {
        public CreateOrEditProductFeeDto ProductFee { get; set; }
        public string CountryName { get; set; }
        public string InstallmentTypeName { get; set; }
        public List<ProductFeeInstallmentTypeLookupTableDto> ProductFeeInstallmentTypeList { get; set; }
        public List<ProductFeeCountryLookupTableDto> ProductFeeCountryList { get; set; }
        public bool IsEditMode => ProductFee.Id.HasValue;
    }
}
