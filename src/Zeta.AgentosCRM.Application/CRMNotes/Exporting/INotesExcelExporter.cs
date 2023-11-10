using System.Collections.Generic;
using Zeta.AgentosCRM.CRMNotes.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMNotes.Exporting
{
    public interface INotesExcelExporter
    {
        FileDto ExportToFile(List<GetNoteForViewDto> notes);
    }
}