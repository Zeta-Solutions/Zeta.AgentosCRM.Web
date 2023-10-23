using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMClient.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMClient
{
    public interface IClientTagsAppService : IApplicationService
    {
        Task<PagedResultDto<GetClientTagForViewDto>> GetAll(GetAllClientTagsInput input);

        Task<GetClientTagForEditOutput> GetClientTagForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditClientTagDto input);

        Task Delete(EntityDto input);

        Task<List<ClientTagClientLookupTableDto>> GetAllClientForTableDropdown();

        Task<List<ClientTagTagLookupTableDto>> GetAllTagForTableDropdown();

    }
}