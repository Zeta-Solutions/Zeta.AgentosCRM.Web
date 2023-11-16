namespace Zeta.AgentosCRM.TaskManagement.Followers.Dtos
{
    public class GetTaskFollowerForViewDto
    {
        public TaskFollowerDto TaskFollower { get; set; }

        public string UserName { get; set; }

        public string CRMTaskTitle { get; set; }

    }
}