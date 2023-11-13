using System.Collections.Generic;
using Zeta.AgentosCRM.CRMAppointments.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMAppointments.Exporting
{
    public interface IAppointmentsExcelExporter
    {
        FileDto ExportToFile(List<GetAppointmentForViewDto> appointments);
    }
}