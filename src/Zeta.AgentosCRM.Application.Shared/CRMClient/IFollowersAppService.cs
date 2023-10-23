using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMClient.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMClient
{
    public interface IFollowersAppService : IApplicationService
    {
        Task<PagedResultDto<GetFollowerForViewDto>> GetAll(GetAllFollowersInput input);

        Task<GetFollowerForEditOutput> GetFollowerForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditFollowerDto input);

        Task Delete(EntityDto input);

        Task<List<FollowerClientLookupTableDto>> GetAllClientForTableDropdown();

        Task<List<FollowerUserLookupTableDto>> GetAllUserForTableDropdown();

    }
}