using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMAppointments.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMAppointments
{
    public interface IAppointmentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetAppointmentForViewDto>> GetAll(GetAllAppointmentsInput input);

        Task<GetAppointmentForViewDto> GetAppointmentForView(long id);

        Task<GetAppointmentForEditOutput> GetAppointmentForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditAppointmentDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetAppointmentsToExcel(GetAllAppointmentsForExcelInput input);

        Task<List<AppointmentClientLookupTableDto>> GetAllClientForTableDropdown();

        Task<List<AppointmentPartnerLookupTableDto>> GetAllPartnerForTableDropdown();

        Task<List<AppointmentUserLookupTableDto>> GetAllUserForTableDropdown();

    }
}