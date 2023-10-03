using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.LeadSource.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.LeadSource
{
    public interface ILeadSourcesAppService : IApplicationService
    {
        Task<PagedResultDto<GetLeadSourceForViewDto>> GetAll(GetAllLeadSourcesInput input);

        Task<GetLeadSourceForViewDto> GetLeadSourceForView(int id);

        Task<GetLeadSourceForEditOutput> GetLeadSourceForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditLeadSourceDto input);

        Task Delete(EntityDto input);

    }
}