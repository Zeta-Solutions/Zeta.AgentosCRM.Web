namespace Zeta.AgentosCRM.CRMApplications.Stages.Dtos
{
    public class GetApplicationStageForViewDto
    {
        public ApplicationStageDto ApplicationStage { get; set; }

        public string ApplicationName { get; set; }

        public string WorkflowStepName { get; set; }

    }
}