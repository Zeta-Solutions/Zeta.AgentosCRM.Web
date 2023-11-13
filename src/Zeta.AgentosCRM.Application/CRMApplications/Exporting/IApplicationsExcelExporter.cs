using System.Collections.Generic;
using Zeta.AgentosCRM.CRMApplications.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMApplications.Exporting
{
    public interface IApplicationsExcelExporter
    {
        FileDto ExportToFile(List<GetApplicationForViewDto> applications);
    }
}