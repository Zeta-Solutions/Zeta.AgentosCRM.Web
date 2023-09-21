using System.Collections.Generic;
using Zeta.AgentosCRM.Auditing.Dto;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
