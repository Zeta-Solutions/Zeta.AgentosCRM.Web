using System.Collections.Generic;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Exporting
{
    public interface ISubjectsExcelExporter
    {
        FileDto ExportToFile(List<GetSubjectForViewDto> subjects);
    }
}