using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMClient.Education.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMClient.Education
{
    public interface IClientEducationsAppService : IApplicationService
    {
        Task<PagedResultDto<GetClientEducationForViewDto>> GetAll(GetAllClientEducationsInput input);

        Task<GetClientEducationForViewDto> GetClientEducationForView(long id);

        Task<GetClientEducationForEditOutput> GetClientEducationForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditClientEducationDto input);

        Task Delete(EntityDto<long> input);

        Task<List<ClientEducationDegreeLevelLookupTableDto>> GetAllDegreeLevelForTableDropdown();

        Task<List<ClientEducationSubjectLookupTableDto>> GetAllSubjectForTableDropdown();

        Task<List<ClientEducationSubjectAreaLookupTableDto>> GetAllSubjectAreaForTableDropdown();

    }
}