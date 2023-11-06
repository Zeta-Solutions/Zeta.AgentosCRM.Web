using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMPartner.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;

namespace Zeta.AgentosCRM.CRMPartner.Exporting
{
    public class PartnersExcelExporter : MiniExcelExcelExporterBase, IPartnersExcelExporter
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
            var excelPackage = new List<Dictionary<string, object>>();

            foreach (var partner in partners)
            {
                excelPackage.Add(new Dictionary<string, object>()
     {
         { L("PartnerName"),partner.Partner.PartnerName},
          {   L("Street"),partner.Partner.Street},
           {  L("City"),partner.Partner.City},
            { L("State"),partner.Partner.State},
             { L("ZipCode"),partner.Partner.ZipCode},
            { L("PhoneNo"),partner.Partner.PhoneNo},
             { L("Email"),partner.Partner.Email},
          {   L("Fax"),partner.Partner.Fax},
           {  L("Website"),partner.Partner.Website},
            { L("University"),partner.Partner.University},
             { L("MarketingEmail"),partner.Partner.MarketingEmail},
     });

            }

            return CreateExcelPackage("Partners.xlsx", excelPackage);
        }
    }
}