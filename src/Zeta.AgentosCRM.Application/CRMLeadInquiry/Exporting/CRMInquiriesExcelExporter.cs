using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMLeadInquiry.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;

namespace Zeta.AgentosCRM.CRMLeadInquiry.Exporting
{
    public class CRMInquiriesExcelExporter : MiniExcelExcelExporterBase, ICRMInquiriesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public CRMInquiriesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetCRMInquiryForViewDto> crmInquiries)
        {
            var excelPackage = new List<Dictionary<string, object>>();

            foreach (var inquiries in crmInquiries)
            {
                excelPackage.Add(new Dictionary<string, object>
                {
                     {L("FirstName"), inquiries.CRMInquiry.FirstName },
                     {L("LastName"), inquiries.CRMInquiry.LastName },
                     {L("Email"), inquiries.CRMInquiry.Email },
                     {L("PhoneNo"), inquiries.CRMInquiry.PhoneNo },
                     {L("DateofBirth"), inquiries.CRMInquiry.DateofBirth },
                     {L("ContactPreference"), inquiries.CRMInquiry.ContactPreference },
                     {L("Institution"), inquiries.CRMInquiry.Institution },
                     {L("Street"), inquiries.CRMInquiry.Street },
                     {L("City"), inquiries.CRMInquiry.City },
                     {L("State"), inquiries.CRMInquiry.State }, 
                     {L("PreferedInTake"), inquiries.CRMInquiry.PreferedInTake }, 
                     {L("VisaType"), inquiries.CRMInquiry.VisaType },
                     {L("VisaExpiryDate"), inquiries.CRMInquiry.VisaExpiryDate } 
                 });
            }
            return CreateExcelPackage("CRMInquiries.xlsx", excelPackage);
                
        }
    }
}