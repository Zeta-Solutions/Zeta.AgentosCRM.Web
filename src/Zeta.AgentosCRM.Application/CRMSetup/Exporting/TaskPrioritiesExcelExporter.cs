using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;

namespace Zeta.AgentosCRM.CRMSetup.Exporting
{
    public class TaskPrioritiesExcelExporter : MiniExcelExcelExporterBase, ITaskPrioritiesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TaskPrioritiesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTaskPriorityForViewDto> taskPriorities)
        {
            var item= new List<Dictionary<string, object>>();

            foreach (var taskPriority in taskPriorities)
            {
                item.Add(new Dictionary<string, object>() 
                { 
                    {L("Abbrivation"),taskPriority.TaskPriority.Abbrivation },
                    {L("Name"),taskPriority.TaskPriority.Name},
                });
            }

            return CreateExcelPackage("TaskPriorities.xlsx",item);
        }
    }
}