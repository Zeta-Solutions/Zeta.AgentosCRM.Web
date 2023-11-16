using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMClient.InterstedServices.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMClient.InterstedServices
{
    public interface IClientInterstedServicesAppService : IApplicationService
    {
        Task<PagedResultDto<GetClientInterstedServiceForViewDto>> GetAll(GetAllClientInterstedServicesInput input);

        Task<GetClientInterstedServiceForViewDto> GetClientInterstedServiceForView(long id);

        Task<GetClientInterstedServiceForEditOutput> GetClientInterstedServiceForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditClientInterstedServiceDto input);

        Task Delete(EntityDto<long> input);

        Task<List<ClientInterstedServiceClientLookupTableDto>> GetAllClientForTableDropdown();

        Task<List<ClientInterstedServicePartnerLookupTableDto>> GetAllPartnerForTableDropdown();

        Task<List<ClientInterstedServiceProductLookupTableDto>> GetAllProductForTableDropdown();

        Task<List<ClientInterstedServiceBranchLookupTableDto>> GetAllBranchForTableDropdown();

        Task<List<ClientInterstedServiceWorkflowLookupTableDto>> GetAllWorkflowForTableDropdown();

    }
}