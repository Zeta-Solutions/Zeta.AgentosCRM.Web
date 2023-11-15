using Abp.Application.Services.Dto;

namespace Zeta.AgentosCRM.TaskManagement.Followers.Dtos
{
    public class TaskFollowerUserLookupTableDto
    {
        public long Id { get; set; }

        public string DisplayName { get; set; }
    }
}