using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Tenants.Email.Configuration.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.Tenants.Email.Configuration
{
    public interface IEmailConfigurationsAppService : IApplicationService
    {
        Task<PagedResultDto<GetEmailConfigurationForViewDto>> GetAll(GetAllEmailConfigurationsInput input);

        Task<GetEmailConfigurationForViewDto> GetEmailConfigurationForView(long id);

        Task<GetEmailConfigurationForEditOutput> GetEmailConfigurationForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditEmailConfigurationDto input);

        Task Delete(EntityDto<long> input);

    }
}