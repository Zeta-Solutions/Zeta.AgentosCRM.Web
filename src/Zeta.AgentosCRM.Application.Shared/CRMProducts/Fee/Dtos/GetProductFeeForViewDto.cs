namespace Zeta.AgentosCRM.CRMProducts.Fee.Dtos
{
    public class GetProductFeeForViewDto
    {
        public ProductFeeDto ProductFee { get; set; }

        public string CountryName { get; set; }

        public string InstallmentTypeName { get; set; }

        public string FeeTypeName { get; set; }

        public string ProductName { get; set; }

    }
}