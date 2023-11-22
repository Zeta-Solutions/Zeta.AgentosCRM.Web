using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMAgent.Contacts.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMAgent.Contacts
{
    public interface IAgentContactsAppService : IApplicationService
    {
        Task<PagedResultDto<GetAgentContactForViewDto>> GetAll(GetAllAgentContactsInput input);

        Task<GetAgentContactForViewDto> GetAgentContactForView(long id);

        Task<GetAgentContactForEditOutput> GetAgentContactForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditAgentContactDto input);

        Task Delete(EntityDto<long> input);

        Task<List<AgentContactAgentLookupTableDto>> GetAllAgentForTableDropdown();

    }
}