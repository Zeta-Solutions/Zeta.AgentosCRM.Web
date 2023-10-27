using System.Collections.Generic;
using Zeta.AgentosCRM.CRMPartner.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMPartner.Exporting
{
    public interface IPartnersExcelExporter
    {
        FileDto ExportToFile(List<GetPartnerForViewDto> partners);
    }
}