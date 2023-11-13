namespace Zeta.AgentosCRM.CRMClient.Education.Dtos
{
    public class GetClientEducationForViewDto
    {
        public ClientEducationDto ClientEducation { get; set; }

        public string DegreeLevelName { get; set; }

        public string SubjectName { get; set; }

        public string SubjectAreaName { get; set; }

    }
}