using System.Collections.Generic;
using Zeta.AgentosCRM.CRMClient.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMClient.Exporting
{
    public interface IClientsExcelExporter
    {
        FileDto ExportToFile(List<GetClientForViewDto> clients);
    }
}