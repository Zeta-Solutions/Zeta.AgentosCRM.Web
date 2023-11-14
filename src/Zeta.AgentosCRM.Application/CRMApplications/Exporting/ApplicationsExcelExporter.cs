using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMApplications.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;

namespace Zeta.AgentosCRM.CRMApplications.Exporting
{
    public class ApplicationsExcelExporter : MiniExcelExcelExporterBase, IApplicationsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ApplicationsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetApplicationForViewDto> applications)
        {
            var excelPackage= new List<Dictionary<string, object>>();

            foreach (var applicator in applications)
            {
                excelPackage.Add(new Dictionary<string, object> {
                    { (L("Client")) + L("FirstName"), applicator.ClientFirstName },
                    { (L("Workflow")) + L("Name"), applicator.WorkflowName },
                    { (L("Partner")) + L("PartnerName"), applicator.PartnerPartnerName },
                    { (L("Product")) + L("Name"), applicator.ProductName }
                });
            }

            return CreateExcelPackage( "Applications.xlsx", excelPackage );
        }
    }
}