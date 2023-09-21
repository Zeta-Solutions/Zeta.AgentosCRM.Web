using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup
{
    public interface IWorkflowStepsAppService : IApplicationService
    {
        Task<PagedResultDto<GetWorkflowStepForViewDto>> GetAll(GetAllWorkflowStepsInput input);

        Task<GetWorkflowStepForViewDto> GetWorkflowStepForView(int id);

        Task<GetWorkflowStepForEditOutput> GetWorkflowStepForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditWorkflowStepDto input);

        Task Delete(EntityDto input);

        Task<PagedResultDto<WorkflowStepWorkflowLookupTableDto>> GetAllWorkflowForLookupTable(GetAllForLookupTableInput input);

    }
}