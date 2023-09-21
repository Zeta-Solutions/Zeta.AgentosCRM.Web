using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;

namespace Zeta.AgentosCRM.CRMSetup.Exporting
{
    public class PartnerTypesExcelExporter : MiniExcelExcelExporterBase, IPartnerTypesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PartnerTypesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPartnerTypeForViewDto> partnerTypes)
        {
            var item= new List<Dictionary<string, object>>();

            foreach (var partnerType in partnerTypes)
            {
                item.Add(new Dictionary<string, object>()
                {
                    {L("Abbrivation"),partnerType.PartnerType.Abbrivation },
                    {L("Name"),partnerType.PartnerType.Name },
                    {L("MasterCategoryName"),partnerType.MasterCategoryName },
                });
            }
             
            return CreateExcelPackage("PartnerTypes.xlsx", item);
        }
    }
}