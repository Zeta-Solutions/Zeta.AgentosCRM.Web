using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMNotes.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMNotes
{
    public interface INotesAppService : IApplicationService
    {
        Task<PagedResultDto<GetNoteForViewDto>> GetAll(GetAllNotesInput input);

        Task<GetNoteForViewDto> GetNoteForView(long id);

        Task<GetNoteForEditOutput> GetNoteForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditNoteDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetNotesToExcel(GetAllNotesForExcelInput input);

        Task<List<NoteClientLookupTableDto>> GetAllClientForTableDropdown();

        Task<List<NotePartnerLookupTableDto>> GetAllPartnerForTableDropdown();

    }
}