using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;

namespace Zeta.AgentosCRM.CRMSetup.Exporting
{
    public class SubjectAreasExcelExporter : MiniExcelExcelExporterBase, ISubjectAreasExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public SubjectAreasExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetSubjectAreaForViewDto> subjectAreas)
        {
            var item = new List<Dictionary<string, object>>();

            foreach (var subjectArea in subjectAreas)
            {
                item.Add(new Dictionary<string, object>()
                {
                    {L("Abbrivation"),subjectArea.SubjectArea.Abbrivation },
                    {L("Name"),subjectArea.SubjectArea.Name }, 
                });
            }
            return CreateExcelPackage("SubjectAreas.xlsx", item);
        }
    }
}