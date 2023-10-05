using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMSetup
{
    public interface ISubjectsAppService : IApplicationService
    {
        Task<PagedResultDto<GetSubjectForViewDto>> GetAll(GetAllSubjectsInput input);

        Task<GetSubjectForViewDto> GetSubjectForView(int id);

        Task<GetSubjectForEditOutput> GetSubjectForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditSubjectDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetSubjectsToExcel(GetAllSubjectsForExcelInput input);

        Task<List<SubjectSubjectAreaLookupTableDto>> GetAllSubjectAreaForTableDropdown();

    }
}