using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMProducts.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;
using Stripe;

namespace Zeta.AgentosCRM.CRMProducts.Exporting
{
    public class ProductsExcelExporter : MiniExcelExcelExporterBase, IProductsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ProductsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetProductForViewDto> products)
        {
            var excelPackage = new List<Dictionary<string, object>>();

            foreach (var product in products)
            {
                excelPackage.Add(new Dictionary<string, object>
                {
                    { L("Name"), product.Product.Name },
                    { L("Duration"), product.Product.Name },
                    { L("Description"), product.Product.Name },
                    { L("Note"), product.Product.Name },
                    { L("RevenueType"), product.Product.Name },
                    { L("IntakeMonth"),product.Product.Name }
                });
            }

            return CreateExcelPackage( "Products.xlsx", excelPackage );
        }
    }
}