namespace Zeta.AgentosCRM.CRMLeadInquiry.Dtos
{
    public class GetCRMInquiryForViewDto
    {
        public CRMInquiryDto CRMInquiry { get; set; }

        public string CountryName { get; set; }

        public string CountryName2 { get; set; }

        public string DegreeLevelName { get; set; }

        public string SubjectName { get; set; }

        public string SubjectAreaName { get; set; }

        public string OrganizationUnitDisplayName { get; set; }

        public string LeadSourceName { get; set; }

        public string TagName { get; set; }

    }
}