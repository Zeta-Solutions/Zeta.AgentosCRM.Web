using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMClient.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;

namespace Zeta.AgentosCRM.CRMClient.Exporting
{
    public class ClientsExcelExporter : MiniExcelExcelExporterBase, IClientsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ClientsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetClientForViewDto> clients)
        {
            var excelPackage = new List<Dictionary<string, object>>();

            foreach (var client in clients)
            {
                excelPackage.Add(new Dictionary<string, object> 
                {
                     {L("FirstName"), client.Client.FirstName },
                     {L("LastName"), client.Client.LastName },
                     {L("Email"), client.Client.Email },
                     {L("PhoneNo"), client.Client.PhoneNo },
                     {L("DateofBirth"), client.Client.DateofBirth },
                     {L("ContactPreferences"), client.Client.ContactPreferences },
                     {L("University"), client.Client.University },
                     {L("Street"), client.Client.Street },
                     {L("City"), client.Client.City },
                     {L("State"), client.Client.State },
                     {L("ZipCode"), client.Client.ZipCode },
                     {L("PreferedIntake"), client.Client.PreferedIntake },
                     {L("PassportNo"), client.Client.PassportNo },
                     {L("VisaType"), client.Client.VisaType },
                     {L("VisaExpiryDate"), client.Client.VisaExpiryDate },
                     {L("Rating"), client.Client.Rating },
                     {L("ClientPortal"),client.Client.ClientPortal}
                 });
            }

            return CreateExcelPackage("Clients.xlsx", excelPackage);
        }
    }
}