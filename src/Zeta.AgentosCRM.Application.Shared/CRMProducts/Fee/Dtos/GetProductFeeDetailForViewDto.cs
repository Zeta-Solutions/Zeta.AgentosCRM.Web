namespace Zeta.AgentosCRM.CRMProducts.Fee.Dtos
{
    public class GetProductFeeDetailForViewDto
    {
        public ProductFeeDetailDto ProductFeeDetail { get; set; }

        public string FeeTypeName { get; set; }

        public string ProductFeeName { get; set; }
        public string CountryName { get; set; }

        public string InstallmentTypeName { get; set; }

    }
}