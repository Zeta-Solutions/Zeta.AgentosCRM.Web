using System.Collections.Generic;
using Zeta.AgentosCRM.CRMLeadInquiry.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMLeadInquiry.Exporting
{
    public interface ICRMInquiriesExcelExporter
    {
        FileDto ExportToFile(List<GetCRMInquiryForViewDto> crmInquiries);
    }
}