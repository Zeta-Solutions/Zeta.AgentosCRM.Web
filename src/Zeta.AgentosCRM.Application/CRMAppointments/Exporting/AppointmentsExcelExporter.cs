using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone; 
using Zeta.AgentosCRM.CRMAppointments.Dtos;
using Zeta.AgentosCRM.Dto;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.DataExporting.Excel.MiniExcel;

namespace Zeta.AgentosCRM.CRMAppointments.Exporting
{
    public class AppointmentsExcelExporter : MiniExcelExcelExporterBase, IAppointmentsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public AppointmentsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetAppointmentForViewDto> appointments)
        {
            var excelPackage= new List<Dictionary<string, object>>();

            foreach (var appointment in appointments)
            {
                excelPackage.Add(new Dictionary<string, object> {
                    { L("RelatedTo"), appointment.Appointment.RelatedTo },
                    { L("Title"), appointment.Appointment.Title },
                    { L("Description"), appointment.Appointment.Description },
                    { L("AppointmentDate"), appointment.Appointment.AppointmentDate },
                    { L("AppointmentTime"), appointment.Appointment.AppointmentTime },
                    { L("TimeZone"), appointment.Appointment.TimeZone },
                });
            }

            return CreateExcelPackage( "Appointments.xlsx", excelPackage );
        }
    }
}