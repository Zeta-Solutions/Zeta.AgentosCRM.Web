using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.TaskCategory.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.TaskCategory
{
    public interface ITaskCategoriesAppService : IApplicationService
    {
        Task<PagedResultDto<GetTaskCategoryForViewDto>> GetAll(GetAllTaskCategoriesInput input);

        Task<GetTaskCategoryForViewDto> GetTaskCategoryForView(int id);

        Task<GetTaskCategoryForEditOutput> GetTaskCategoryForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditTaskCategoryDto input);

        Task Delete(EntityDto input);

    }
}