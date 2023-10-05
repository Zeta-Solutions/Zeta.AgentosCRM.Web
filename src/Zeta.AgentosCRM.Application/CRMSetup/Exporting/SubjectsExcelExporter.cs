using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;

namespace Zeta.AgentosCRM.CRMSetup.Exporting
{
    public class SubjectsExcelExporter : MiniExcelExcelExporterBase, ISubjectsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public SubjectsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetSubjectForViewDto> subjects)
        {
            var item= new List<Dictionary<string, object>>();

            foreach (var subject in subjects)
            {
                item.Add(new Dictionary<string, object>()
                {
                    {L("Abbrivation"),subject.Subject.Abbrivation },
                    {L("Name"),subject.Subject.Name },
                    {L("AreaName"),subject.SubjectAreaName },
                });
            }
            return CreateExcelPackage("Subjects.xlsx",item);
        }
    }
}