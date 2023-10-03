using System.Collections.Generic;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Exporting
{
    public interface ISubjectAreasExcelExporter
    {
        FileDto ExportToFile(List<GetSubjectAreaForViewDto> subjectAreas);
    }
}