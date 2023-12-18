namespace Zeta.AgentosCRM.CRMPartner.Dtos
{
    public class GetPartnerForViewDto
    {
        public PartnerDto Partner { get; set; }

        public string BinaryObjectDescription { get; set; }

        public string MasterCategoryName { get; set; }

        public string PartnerTypeName { get; set; }

        public string WorkflowName { get; set; }

        public string CountryName { get; set; }

        public string CRMCurrencyName { get; set; }
        public string ImageBytes { get; set; }

    }
}