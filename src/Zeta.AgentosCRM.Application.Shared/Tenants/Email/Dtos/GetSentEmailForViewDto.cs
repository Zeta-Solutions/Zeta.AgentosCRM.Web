namespace Zeta.AgentosCRM.Tenants.Email.Dtos
{
    public class GetSentEmailForViewDto
    {
        public SentEmailDto SentEmail { get; set; }

        public string EmailTemplateTitle { get; set; }

        public string EmailConfigurationName { get; set; }

        public string ClientFirstName { get; set; }

        public string ApplicationName { get; set; }

    }
}