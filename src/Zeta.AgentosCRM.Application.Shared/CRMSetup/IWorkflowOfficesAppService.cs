using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup
{
    public interface IWorkflowOfficesAppService : IApplicationService
    {
        Task<PagedResultDto<GetWorkflowOfficeForViewDto>> GetAll(GetAllWorkflowOfficesInput input);

        Task<GetWorkflowOfficeForViewDto> GetWorkflowOfficeForView(int id);

        Task<GetWorkflowOfficeForEditOutput> GetWorkflowOfficeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditWorkflowOfficeDto input);

        Task Delete(EntityDto input);

        Task<List<WorkflowOfficeOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown();

        Task<List<WorkflowOfficeWorkflowLookupTableDto>> GetAllWorkflowForTableDropdown();

    }
}