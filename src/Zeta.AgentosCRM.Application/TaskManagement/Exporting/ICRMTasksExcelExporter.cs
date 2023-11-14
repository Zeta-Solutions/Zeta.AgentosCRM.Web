using System.Collections.Generic;
using Zeta.AgentosCRM.TaskManagement.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.TaskManagement.Exporting
{
    public interface ICRMTasksExcelExporter
    {
        FileDto ExportToFile(List<GetCRMTaskForViewDto> crmTasks);
    }
}