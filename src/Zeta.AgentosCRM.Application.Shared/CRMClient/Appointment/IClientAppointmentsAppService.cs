using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMClient.Appointment.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMClient.Appointment
{
    public interface IClientAppointmentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetClientAppointmentForViewDto>> GetAll(GetAllClientAppointmentsInput input);

        Task<GetClientAppointmentForViewDto> GetClientAppointmentForView(long id);

        Task<GetClientAppointmentForEditOutput> GetClientAppointmentForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditClientAppointmentDto input);

        Task Delete(EntityDto<long> input);

        Task<List<ClientAppointmentClientLookupTableDto>> GetAllClientForTableDropdown();

    }
}