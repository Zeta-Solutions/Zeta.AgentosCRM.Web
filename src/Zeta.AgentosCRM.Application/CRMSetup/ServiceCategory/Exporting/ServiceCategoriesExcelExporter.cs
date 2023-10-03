using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMSetup.ServiceCategory.Dtos;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup.ServiceCategory.Exporting
{
    public class ServiceCategoriesExcelExporter : MiniExcelExcelExporterBase, IServiceCategoriesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ServiceCategoriesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetServiceCategoryForViewDto> serviceCategories)
        {
            var item=new List<Dictionary<string, object>>();
            
            foreach (var serviceCategory in serviceCategories)
            {
                item.Add(new Dictionary<string, object>() 
                { 
                    {L("Abbrivation"), serviceCategory.ServiceCategory.Abbrivation },
                    {L("Name"), serviceCategory.ServiceCategory.Name },
                });
            }
            return CreateExcelPackage("ServiceCategories.xlsx",item);
        }
    }
}