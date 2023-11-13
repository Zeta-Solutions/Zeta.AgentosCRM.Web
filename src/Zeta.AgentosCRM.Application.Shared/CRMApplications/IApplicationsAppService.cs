using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMApplications.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMApplications
{
    public interface IApplicationsAppService : IApplicationService
    {
        Task<PagedResultDto<GetApplicationForViewDto>> GetAll(GetAllApplicationsInput input);

        Task<GetApplicationForViewDto> GetApplicationForView(long id);

        Task<GetApplicationForEditOutput> GetApplicationForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditApplicationDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetApplicationsToExcel(GetAllApplicationsForExcelInput input);

        Task<List<ApplicationClientLookupTableDto>> GetAllClientForTableDropdown();

        Task<List<ApplicationWorkflowLookupTableDto>> GetAllWorkflowForTableDropdown();

        Task<List<ApplicationPartnerLookupTableDto>> GetAllPartnerForTableDropdown();

        Task<List<ApplicationProductLookupTableDto>> GetAllProductForTableDropdown();

    }
}