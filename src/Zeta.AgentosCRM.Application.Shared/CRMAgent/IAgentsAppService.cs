using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMAgent.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMAgent
{
    public interface IAgentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetAgentForViewDto>> GetAll(GetAllAgentsInput input);

        Task<GetAgentForViewDto> GetAgentForView(long id);

        Task<GetAgentForEditOutput> GetAgentForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditAgentDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetAgentsToExcel(GetAllAgentsForExcelInput input);

        Task<List<AgentCountryLookupTableDto>> GetAllCountryForTableDropdown();

        Task<List<AgentOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown();

        Task RemoveProfilePictureIdFile(EntityDto<long> input);

    }
}