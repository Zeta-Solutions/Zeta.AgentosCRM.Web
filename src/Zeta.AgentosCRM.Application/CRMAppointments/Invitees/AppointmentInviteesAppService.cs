using Zeta.AgentosCRM.CRMAppointments;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.Authorization.Users;

namespace Zeta.AgentosCRM.CRMAppointments.Invitees
{
    [AbpAuthorize(AppPermissions.Pages_AppointmentInvitees)]
    public class AppointmentInviteesAppService : AgentosCRMAppServiceBase, IAppointmentInviteesAppService
    {
        private readonly IRepository<AppointmentInvitee, long> _appointmentInviteeRepository;
        private readonly IRepository<Appointment, long> _lookup_appointmentRepository;
        private readonly IRepository<User, long> _lookup_userRepository;

        public AppointmentInviteesAppService(IRepository<AppointmentInvitee, long> appointmentInviteeRepository, IRepository<Appointment, long> lookup_appointmentRepository, IRepository<User, long> lookup_userRepository)
        {
            _appointmentInviteeRepository = appointmentInviteeRepository;
            _lookup_appointmentRepository = lookup_appointmentRepository;
            _lookup_userRepository = lookup_userRepository;
        }

        public async Task<PagedResultDto<GetAppointmentInviteeForViewDto>> GetAll(GetAllAppointmentInviteesInput input)
        {

            var filteredAppointmentInvitees = _appointmentInviteeRepository.GetAll()
                        .Include(e => e.AppointmentFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AppointmentTitleFilter), e => e.AppointmentFk != null && e.AppointmentFk.Title == input.AppointmentTitleFilter);

            var pagedAndFilteredAppointmentInvitees = filteredAppointmentInvitees
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var appointmentInvitees = from o in pagedAndFilteredAppointmentInvitees
                                      join o1 in _lookup_appointmentRepository.GetAll() on o.AppointmentId equals o1.Id into j1
                                      from s1 in j1.DefaultIfEmpty()

                                      select new
                                      {

                                          Id = o.Id,
                                          AppointmentTitle = s1 == null || s1.Title == null ? "" : s1.Title.ToString()
                                      };

            var totalCount = await filteredAppointmentInvitees.CountAsync();

            var dbList = await appointmentInvitees.ToListAsync();
            var results = new List<GetAppointmentInviteeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetAppointmentInviteeForViewDto()
                {
                    AppointmentInvitee = new AppointmentInviteeDto
                    {

                        Id = o.Id,
                    },
                    AppointmentTitle = o.AppointmentTitle
                };

                results.Add(res);
            }

            return new PagedResultDto<GetAppointmentInviteeForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetAppointmentInviteeForViewDto> GetAppointmentInviteeForView(long id)
        {
            var appointmentInvitee = await _appointmentInviteeRepository.GetAsync(id);

            var output = new GetAppointmentInviteeForViewDto { AppointmentInvitee = ObjectMapper.Map<AppointmentInviteeDto>(appointmentInvitee) };

            if (output.AppointmentInvitee.AppointmentId != null)
            {
                var _lookupAppointment = await _lookup_appointmentRepository.FirstOrDefaultAsync((long)output.AppointmentInvitee.AppointmentId);
                output.AppointmentTitle = _lookupAppointment?.Title?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_AppointmentInvitees_Edit)]
        public async Task<GetAppointmentInviteeForEditOutput> GetAppointmentInviteeForEdit(EntityDto<long> input)
        {
            var appointmentInvitee = await _appointmentInviteeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetAppointmentInviteeForEditOutput { AppointmentInvitee = ObjectMapper.Map<CreateOrEditAppointmentInviteeDto>(appointmentInvitee) };

            if (output.AppointmentInvitee.AppointmentId != null)
            {
                var _lookupAppointment = await _lookup_appointmentRepository.FirstOrDefaultAsync((long)output.AppointmentInvitee.AppointmentId);
                output.AppointmentTitle = _lookupAppointment?.Title?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditAppointmentInviteeDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_AppointmentInvitees_Create)]
        protected virtual async Task Create(CreateOrEditAppointmentInviteeDto input)
        {
            var appointmentInvitee = ObjectMapper.Map<AppointmentInvitee>(input);

            if (AbpSession.TenantId != null)
            {
                appointmentInvitee.TenantId = (int)AbpSession.TenantId;
            }

            await _appointmentInviteeRepository.InsertAsync(appointmentInvitee);

        }

        [AbpAuthorize(AppPermissions.Pages_AppointmentInvitees_Edit)]
        protected virtual async Task Update(CreateOrEditAppointmentInviteeDto input)
        {
            var appointmentInvitee = await _appointmentInviteeRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, appointmentInvitee);

        }

        [AbpAuthorize(AppPermissions.Pages_AppointmentInvitees_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _appointmentInviteeRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_AppointmentInvitees)]
        public async Task<List<AppointmentInviteeAppointmentLookupTableDto>> GetAllAppointmentForTableDropdown()
        {
            return await _lookup_appointmentRepository.GetAll()
                .Select(appointment => new AppointmentInviteeAppointmentLookupTableDto
                {
                    Id = appointment.Id,
                    DisplayName = appointment == null || appointment.Title == null ? "" : appointment.Title.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_AppointmentInvitees)]
        public async Task<List<AppointmentInviteeUserLookupTableDto>> GetAllUserForTableDropdown()
        {
            return await _lookup_userRepository.GetAll()
                .Select(user => new AppointmentInviteeUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user == null || user.Name == null ? "" : user.Name.ToString()
                }).ToListAsync();
        }

    }
}