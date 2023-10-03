using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup
{
    public interface IDegreeLevelsAppService : IApplicationService
    {
        Task<PagedResultDto<GetDegreeLevelForViewDto>> GetAll(GetAllDegreeLevelsInput input);

        Task<GetDegreeLevelForViewDto> GetDegreeLevelForView(int id);

        Task<GetDegreeLevelForEditOutput> GetDegreeLevelForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditDegreeLevelDto input);

        Task Delete(EntityDto input);

    }
}