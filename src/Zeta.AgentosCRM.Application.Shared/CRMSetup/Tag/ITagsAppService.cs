using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Tag.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Tag
{
    public interface ITagsAppService : IApplicationService
    {
        Task<PagedResultDto<GetTagForViewDto>> GetAll(GetAllTagsInput input);

        Task<GetTagForViewDto> GetTagForView(int id);

        Task<GetTagForEditOutput> GetTagForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditTagDto input);

        Task Delete(EntityDto input);

    }
}