using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMClient.Education.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMClient.Education
{
    public interface IOtherTestScoresAppService : IApplicationService
    {
        Task<PagedResultDto<GetOtherTestScoreForViewDto>> GetAll(GetAllOtherTestScoresInput input);

        Task<GetOtherTestScoreForViewDto> GetOtherTestScoreForView(int id);

        Task<GetOtherTestScoreForEditOutput> GetOtherTestScoreForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditOtherTestScoreDto input);

        Task Delete(EntityDto input);

        Task<List<OtherTestScoreClientLookupTableDto>> GetAllClientForTableDropdown();

    }
}