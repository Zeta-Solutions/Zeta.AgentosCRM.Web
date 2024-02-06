using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMClient.Dtos;
using Zeta.AgentosCRM.CRMClient.Exporting;
using Zeta.AgentosCRM.CRMLead.Dtos;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMLead.Exporting
{
    public class LeadDetailsExcelExporter : MiniExcelExcelExporterBase, ILeadDetailsExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public LeadDetailsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetLeadDetailForViewDto> LeadDetail)
        {
            var excelPackage = new List<Dictionary<string, object>>();

            foreach (var lead in LeadDetail)
            {
                excelPackage.Add(new Dictionary<string, object>
                {
                     {L("FirstName"), lead.LeadDetail.PropertyName },
                     {L("LastName"), lead.LeadDetail.Inputtype },
                     {L("Email"), lead.LeadDetail.Status },
                     {L("PhoneNo"), lead.LeadDetail.SectionName }                    
                     //{L("ClientPortal"),LeadDetail.Client.ClientPortal}
                 });
            }

            return CreateExcelPackage("Clients.xlsx", excelPackage);
        }
    }
}
