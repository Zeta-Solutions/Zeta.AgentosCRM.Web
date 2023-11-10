using System.Collections.Generic;
using Zeta.AgentosCRM.CRMAgent.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMAgent.Exporting
{
    public interface IAgentsExcelExporter
    {
        FileDto ExportToFile(List<GetAgentForViewDto> agents);
    }
}