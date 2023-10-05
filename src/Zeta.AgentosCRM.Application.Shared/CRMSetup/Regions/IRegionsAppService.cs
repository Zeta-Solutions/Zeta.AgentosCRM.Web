using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Regions.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Regions
{
    public interface IRegionsAppService : IApplicationService
    {
        Task<PagedResultDto<GetRegionForViewDto>> GetAll(GetAllRegionsInput input);

        Task<GetRegionForViewDto> GetRegionForView(int id);

        Task<GetRegionForEditOutput> GetRegionForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditRegionDto input);

        Task Delete(EntityDto input);

    }
}