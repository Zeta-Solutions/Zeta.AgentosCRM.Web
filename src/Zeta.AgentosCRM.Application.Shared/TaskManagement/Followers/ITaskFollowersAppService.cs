using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.TaskManagement.Followers.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.TaskManagement.Followers
{
    public interface ITaskFollowersAppService : IApplicationService
    {
        Task<PagedResultDto<GetTaskFollowerForViewDto>> GetAll(GetAllTaskFollowersInput input);

        Task<GetTaskFollowerForViewDto> GetTaskFollowerForView(long id);

        Task<GetTaskFollowerForEditOutput> GetTaskFollowerForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditTaskFollowerDto input);

        Task Delete(EntityDto<long> input);

        Task<List<TaskFollowerUserLookupTableDto>> GetAllUserForTableDropdown();

        Task<List<TaskFollowerCRMTaskLookupTableDto>> GetAllCRMTaskForTableDropdown();

    }
}