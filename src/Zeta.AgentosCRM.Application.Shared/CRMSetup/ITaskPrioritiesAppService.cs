using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup
{
    public interface ITaskPrioritiesAppService : IApplicationService
    {
        Task<PagedResultDto<GetTaskPriorityForViewDto>> GetAll(GetAllTaskPrioritiesInput input);

        Task<GetTaskPriorityForViewDto> GetTaskPriorityForView(int id);

        Task<GetTaskPriorityForEditOutput> GetTaskPriorityForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditTaskPriorityDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetTaskPrioritiesToExcel(GetAllTaskPrioritiesForExcelInput input);

    }
}