using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel; 

namespace Zeta.AgentosCRM.CRMSetup.Exporting
{
    public class MasterCategoriesExcelExporter : MiniExcelExcelExporterBase, IMasterCategoriesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public MasterCategoriesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetMasterCategoryForViewDto> masterCategories)
        {
            var items = new List<Dictionary<string, object>>();

            foreach (var masterCategory in masterCategories)
            {
                items.Add(new Dictionary<string, object>()
                {
                    {L("Abbrivation"), masterCategory.MasterCategory.Abbrivation },
                    {L("Name"), masterCategory.MasterCategory.Name},
                    
                });
            }

            return CreateExcelPackage("MasterCategories.xlsx", items);
             
        }
    }
}