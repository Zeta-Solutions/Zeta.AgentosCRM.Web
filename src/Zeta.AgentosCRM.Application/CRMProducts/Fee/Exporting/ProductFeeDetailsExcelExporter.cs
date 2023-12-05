using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMProducts.Fee.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;
using PayPalCheckoutSdk.Orders;

namespace Zeta.AgentosCRM.CRMProducts.Fee.Exporting
{
    public class ProductFeeDetailsExcelExporter : MiniExcelExcelExporterBase, IProductFeeDetailsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ProductFeeDetailsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetProductFeeDetailForViewDto> productFeeDetails)
        {
            var excelPackage= new List<Dictionary<string, object>>();

            foreach (var item in productFeeDetails)
            {
                excelPackage.Add(new Dictionary<string, object> {
                    { L("InstallmentAmount"), item.ProductFeeDetail.InstallmentAmount },
                    {   L("Installments"), item.ProductFeeDetail.Installments },
                    {   L("TotalFee"), item.ProductFeeDetail.TotalFee },
                    {   L("ClaimTerms"), item.ProductFeeDetail.ClaimTerms },
                    {   L("CommissionPer"), item.ProductFeeDetail.CommissionPer},
                    {   L("IsPayable"), item.ProductFeeDetail.IsPayable},
                    {   L("AddInQuotation"), item.ProductFeeDetail.AddInQuotation },
                     });
            }

            return CreateExcelPackage( "ProductFeeDetails.xlsx", excelPackage );
        }
    }
}