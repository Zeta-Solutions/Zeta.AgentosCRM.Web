using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMNotes.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;

namespace Zeta.AgentosCRM.CRMNotes.Exporting
{
    public class NotesExcelExporter : MiniExcelExcelExporterBase, INotesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public NotesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetNoteForViewDto> notes)
        {
            var excelPackage= new List<Dictionary<string, object>>();

            foreach (var note in notes)
            {
                excelPackage.Add(new Dictionary<string, object> {
                    { L("Title"), note.Note.Title },
                    { L("Description"), note.Note.Description } 
                });
            }

            return CreateExcelPackage( "Notes.xlsx", excelPackage);
        }
    }
}