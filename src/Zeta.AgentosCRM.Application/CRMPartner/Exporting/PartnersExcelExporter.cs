using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Zeta.AgentosCRM.DataExporting.Excel.NPOI;
using Zeta.AgentosCRM.CRMPartner.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMPartner.Exporting
{
    public class PartnersExcelExporter : NpoiExcelExporterBase, IPartnersExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PartnersExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPartnerForViewDto> partners)
        {
            return CreateExcelPackage(
                "Partners.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Partners"));

                    AddHeader(
                        sheet,
                        L("PartnerName"),
                        L("Street"),
                        L("City"),
                        L("State"),
                        L("ZipCode"),
                        L("PhoneNo"),
                        L("Email"),
                        L("Fax"),
                        L("Website"),
                        L("University"),
                        L("MarketingEmail"),
                        (L("BinaryObject")) + L("Description"),
                        (L("MasterCategory")) + L("Name"),
                        (L("PartnerType")) + L("Name"),
                        (L("Workflow")) + L("Name"),
                        (L("Country")) + L("Name"),
                        (L("Country")) + L("DisplayProperty")
                        );

                    AddObjects(
                        sheet, partners,
                        _ => _.Partner.PartnerName,
                        _ => _.Partner.Street,
                        _ => _.Partner.City,
                        _ => _.Partner.State,
                        _ => _.Partner.ZipCode,
                        _ => _.Partner.PhoneNo,
                        _ => _.Partner.Email,
                        _ => _.Partner.Fax,
                        _ => _.Partner.Website,
                        _ => _.Partner.University,
                        _ => _.Partner.MarketingEmail,
                        _ => _.BinaryObjectDescription,
                        _ => _.MasterCategoryName,
                        _ => _.PartnerTypeName,
                        _ => _.WorkflowName,
                        _ => _.CountryName,
                        _ => _.CountryDisplayProperty2
                        );

                });
        }
    }
}