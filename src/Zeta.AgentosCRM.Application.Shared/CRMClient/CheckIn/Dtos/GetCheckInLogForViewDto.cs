namespace Zeta.AgentosCRM.CRMClient.CheckIn.Dtos
{
    public class GetCheckInLogForViewDto
    {
        public CheckInLogDto CheckInLog { get; set; }

        public string ClientDisplayProperty { get; set; }

        public string UserName { get; set; }

    }
}