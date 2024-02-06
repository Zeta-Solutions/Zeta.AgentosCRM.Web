using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMClient.Dtos;
using Zeta.AgentosCRM.CRMLead.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMLead.Exporting
{
    public interface ILeadDetailsExcelExporter
    {
        FileDto ExportToFile(List<GetLeadDetailForViewDto> clients);
    }
}
