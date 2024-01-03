using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.TaskManagement.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.TaskManagement
{
    public interface ICRMTasksAppService : IApplicationService
    {
        Task<PagedResultDto<GetCRMTaskForViewDto>> GetAll(GetAllCRMTasksInput input);

        Task<GetCRMTaskForViewDto> GetCRMTaskForView(long id);

        Task<GetCRMTaskForEditOutput> GetCRMTaskForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditCRMTaskDto input);
        Task UpdateTaskIsCompleted(UpdateTaskIsCompletedDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetCRMTasksToExcel(GetAllCRMTasksForExcelInput input);

        Task<List<CRMTaskTaskCategoryLookupTableDto>> GetAllTaskCategoryForTableDropdown();

        Task<List<CRMTaskUserLookupTableDto>> GetAllUserForTableDropdown();

        Task<List<CRMTaskTaskPriorityLookupTableDto>> GetAllTaskPriorityForTableDropdown();

        Task<List<CRMTaskClientLookupTableDto>> GetAllClientForTableDropdown();

        Task<List<CRMTaskPartnerLookupTableDto>> GetAllPartnerForTableDropdown();

        Task<List<CRMTaskApplicationLookupTableDto>> GetAllApplicationForTableDropdown();

        Task<List<CRMTaskApplicationStageLookupTableDto>> GetAllApplicationStageForTableDropdown();

        Task RemoveAttachmentFile(EntityDto<long> input);

    }
}