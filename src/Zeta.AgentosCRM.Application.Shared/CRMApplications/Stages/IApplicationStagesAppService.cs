using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMApplications.Stages.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMApplications.Stages
{
    public interface IApplicationStagesAppService : IApplicationService
    {
        Task<PagedResultDto<GetApplicationStageForViewDto>> GetAll(GetAllApplicationStagesInput input);

        Task<GetApplicationStageForViewDto> GetApplicationStageForView(long id);

        Task<GetApplicationStageForEditOutput> GetApplicationStageForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditApplicationStageDto input);

        Task Delete(EntityDto<long> input);

        Task<List<ApplicationStageApplicationLookupTableDto>> GetAllApplicationForTableDropdown();

    }
}