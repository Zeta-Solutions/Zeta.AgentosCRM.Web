using System.Collections.Generic;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Exporting
{
    public interface IPartnerTypesExcelExporter
    {
        FileDto ExportToFile(List<GetPartnerTypeForViewDto> partnerTypes);
    }
}