using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMClient.Education.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMClient.Education
{
    public interface IEnglisTestScoresAppService : IApplicationService
    {
        Task<PagedResultDto<GetEnglisTestScoreForViewDto>> GetAll(GetAllEnglisTestScoresInput input);

        Task<GetEnglisTestScoreForViewDto> GetEnglisTestScoreForView(long id);

        Task<GetEnglisTestScoreForEditOutput> GetEnglisTestScoreForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditEnglisTestScoreDto input);

        Task Delete(EntityDto<long> input);

        Task<List<EnglisTestScoreClientLookupTableDto>> GetAllClientForTableDropdown();

    }
}