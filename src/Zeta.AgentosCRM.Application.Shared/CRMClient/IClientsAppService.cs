using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMClient.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using Zeta.AgentosCRM.CRMClient.Conversation;

namespace Zeta.AgentosCRM.CRMClient
{
    public interface IClientsAppService : IApplicationService
    {
        Task<PagedResultDto<GetClientForViewDto>> GetAll(GetAllClientsInput input);

        Task<GetClientForViewDto> GetClientForView(long id);

        Task<GetClientForEditOutput> GetClientForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditClientDto input);

        Task UpdateClientIsArchived(UpdateArchivedClientDto input);
        Task UpdateClientAssignee(UpdateClientAssigneeDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetClientsToExcel(GetAllClientsForExcelInput input);

        Task<List<ClientCountryLookupTableDto>> GetAllCountryForTableDropdown();

        Task<List<ClientUserLookupTableDto>> GetAllUserForTableDropdown();

        Task<PagedResultDto<ClientBinaryObjectLookupTableDto>> GetAllBinaryObjectForLookupTable(GetAllForLookupTableInput input);

        Task<List<ClientDegreeLevelLookupTableDto>> GetAllDegreeLevelForTableDropdown();

        Task<List<ClientSubjectAreaLookupTableDto>> GetAllSubjectAreaForTableDropdown();

        Task<List<ClientLeadSourceLookupTableDto>> GetAllLeadSourceForTableDropdown();

        Task<List<ClientAgentLookupTableDto>> GetAllAgentForTableDropdown();

        Task SendConversationSms(SendConversationSmsInputDto input);
    }
}