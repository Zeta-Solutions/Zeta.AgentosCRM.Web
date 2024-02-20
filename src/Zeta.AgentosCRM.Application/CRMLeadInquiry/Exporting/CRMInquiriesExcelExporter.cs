using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Zeta.AgentosCRM.DataExporting.Excel.NPOI;
using Zeta.AgentosCRM.CRMLeadInquiry.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMLeadInquiry.Exporting
{
    public class CRMInquiriesExcelExporter : NpoiExcelExporterBase, ICRMInquiriesExcelExporter
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
            return CreateExcelPackage(
                "CRMInquiries.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("CRMInquiries"));

                    AddHeader(
                        sheet,
                        L("FirstName"),
                        L("LastName"),
                        L("DateofBirth"),
                        L("PhoneCode"),
                        L("PhoneNo"),
                        L("Email"),
                        L("SecondaryEmail"),
                        L("ContactPreference"),
                        L("Street"),
                        L("City"),
                        L("State"),
                        L("PostalCode"),
                        L("VisaType"),
                        L("VisaExpiryDate"),
                        L("PreferedInTake"),
                        L("DegreeTitle"),
                        L("Institution"),
                        L("CourseStartDate"),
                        L("CourseEndDate"),
                        L("AcademicScore"),
                        L("IsGpa"),
                        L("Toefl"),
                        L("Ielts"),
                        L("Pte"),
                        L("Sat1"),
                        L("Sat2"),
                        L("Gre"),
                        L("GMat"),
                        L("DocumentId"),
                        L("PictureId"),
                        L("Comments"),
                        L("Status"),
                        L("IsArchived"),
                        (L("Country")) + L("Name"),
                        (L("Country")) + L("Name"),
                        (L("DegreeLevel")) + L("Name"),
                        (L("Subject")) + L("Name"),
                        (L("SubjectArea")) + L("Name"),
                        (L("OrganizationUnit")) + L("DisplayName"),
                        (L("LeadSource")) + L("Name"),
                        (L("Tag")) + L("Name")
                        );

                    AddObjects(
                        sheet, crmInquiries,
                        _ => _.CRMInquiry.FirstName,
                        _ => _.CRMInquiry.LastName,
                        _ => _timeZoneConverter.Convert(_.CRMInquiry.DateofBirth, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.CRMInquiry.PhoneCode,
                        _ => _.CRMInquiry.PhoneNo,
                        _ => _.CRMInquiry.Email,
                        _ => _.CRMInquiry.SecondaryEmail,
                        _ => _.CRMInquiry.ContactPreference,
                        _ => _.CRMInquiry.Street,
                        _ => _.CRMInquiry.City,
                        _ => _.CRMInquiry.State,
                        _ => _.CRMInquiry.PostalCode,
                        _ => _.CRMInquiry.VisaType,
                        _ => _timeZoneConverter.Convert(_.CRMInquiry.VisaExpiryDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.CRMInquiry.PreferedInTake,
                        _ => _.CRMInquiry.DegreeTitle,
                        _ => _.CRMInquiry.Institution,
                        _ => _timeZoneConverter.Convert(_.CRMInquiry.CourseStartDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.CRMInquiry.CourseEndDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.CRMInquiry.AcademicScore,
                        _ => _.CRMInquiry.IsGpa,
                        _ => _.CRMInquiry.Toefl,
                        _ => _.CRMInquiry.Ielts,
                        _ => _.CRMInquiry.Pte,
                        _ => _.CRMInquiry.Sat1,
                        _ => _.CRMInquiry.Sat2,
                        _ => _.CRMInquiry.Gre,
                        _ => _.CRMInquiry.GMat,
                        _ => _.CRMInquiry.DocumentIdFileName,
                        _ => _.CRMInquiry.PictureIdFileName,
                        _ => _.CRMInquiry.Comments,
                        _ => _.CRMInquiry.Status,
                        _ => _.CRMInquiry.IsArchived,
                        _ => _.CountryName,
                        _ => _.CountryName2,
                        _ => _.DegreeLevelName,
                        _ => _.SubjectName,
                        _ => _.SubjectAreaName,
                        _ => _.OrganizationUnitDisplayName,
                        _ => _.LeadSourceName,
                        _ => _.TagName
                        );

                    for (var i = 1; i <= crmInquiries.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[3], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(3); for (var i = 1; i <= crmInquiries.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[14], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(14); for (var i = 1; i <= crmInquiries.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[18], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(18); for (var i = 1; i <= crmInquiries.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[19], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(19);
                });
        }
    }
}