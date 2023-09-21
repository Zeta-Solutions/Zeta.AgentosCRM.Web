using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup
{
    public interface IWorkflowsAppService : IApplicationService
    {
        Task<PagedResultDto<GetWorkflowForViewDto>> GetAll(GetAllWorkflowsInput input);

        Task<GetWorkflowForViewDto> GetWorkflowForView(int id);

        Task<GetWorkflowForEditOutput> GetWorkflowForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditWorkflowDto input);

        Task Delete(EntityDto input);

    }
}