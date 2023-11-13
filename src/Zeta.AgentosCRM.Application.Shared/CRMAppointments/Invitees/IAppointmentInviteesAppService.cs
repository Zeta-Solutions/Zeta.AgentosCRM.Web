using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMAppointments.Invitees
{
    public interface IAppointmentInviteesAppService : IApplicationService
    {
        Task<PagedResultDto<GetAppointmentInviteeForViewDto>> GetAll(GetAllAppointmentInviteesInput input);

        Task<GetAppointmentInviteeForViewDto> GetAppointmentInviteeForView(long id);

        Task<GetAppointmentInviteeForEditOutput> GetAppointmentInviteeForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditAppointmentInviteeDto input);

        Task Delete(EntityDto<long> input);

        Task<List<AppointmentInviteeAppointmentLookupTableDto>> GetAllAppointmentForTableDropdown();

    }
}