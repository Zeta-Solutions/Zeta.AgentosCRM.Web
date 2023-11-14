using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.TaskManagement.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;

namespace Zeta.AgentosCRM.TaskManagement.Exporting
{
    public class CRMTasksExcelExporter : MiniExcelExcelExporterBase, ICRMTasksExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public CRMTasksExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetCRMTaskForViewDto> crmTasks)
        {
            var excelPackage = new List<Dictionary<string, object>>();

            foreach (var task in crmTasks)
            {
                excelPackage.Add(new Dictionary<string, object>
                {
                    { L("Title"), task.CRMTask.Title },
                    {   L("DueDate"), task.CRMTask.DueDate },
                    {   L("DueTime"), task.CRMTask.DueTime },
                    {   L("Description"),  task.CRMTask.Description }
                });
            }

            return CreateExcelPackage( "CRMTasks.xlsx", excelPackage);
        }
    }
}