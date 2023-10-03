using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup
{
    public interface ISubjectAreasAppService : IApplicationService
    {
        Task<PagedResultDto<GetSubjectAreaForViewDto>> GetAll(GetAllSubjectAreasInput input);

        Task<GetSubjectAreaForViewDto> GetSubjectAreaForView(int id);

        Task<GetSubjectAreaForEditOutput> GetSubjectAreaForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditSubjectAreaDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetSubjectAreasToExcel(GetAllSubjectAreasForExcelInput input);

        Task<List<SubjectAreaSubjectLookupTableDto>> GetAllSubjectForTableDropdown();

    }
}