using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.Authorization.Users;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMAppointments.Exporting;
using Zeta.AgentosCRM.CRMAppointments.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMAppointments.Invitees;
using Zeta.AgentosCRM.CRMPartner.Promotion;
using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.CRMPartner.Promotion.Dtos;
using Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos;

namespace Zeta.AgentosCRM.CRMAppointments
{
    [AbpAuthorize(AppPermissions.Pages_Appointments)]
    public class AppointmentsAppService : AgentosCRMAppServiceBase, IAppointmentsAppService
    {
        private readonly IRepository<Appointment, long> _appointmentRepository;
        private readonly IAppointmentsExcelExporter _appointmentsExcelExporter;
        private readonly IRepository<Client, long> _lookup_clientRepository;
        private readonly IRepository<Partner, long> _lookup_partnerRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<AppointmentInvitee, long> _appointmentInviteeRepository;

        public AppointmentsAppService(IRepository<Appointment, long> appointmentRepository, IAppointmentsExcelExporter appointmentsExcelExporter, IRepository<Client, long> lookup_clientRepository, IRepository<Partner, long> lookup_partnerRepository, IRepository<User, long> lookup_userRepository, IRepository<AppointmentInvitee, long> appointmentInviteeRepository)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentsExcelExporter = appointmentsExcelExporter;
            _lookup_clientRepository = lookup_clientRepository;
            _lookup_partnerRepository = lookup_partnerRepository;
            _lookup_userRepository = lookup_userRepository;
            _appointmentInviteeRepository = appointmentInviteeRepository;

        }

        public async Task<PagedResultDto<GetAppointmentForViewDto>> GetAll(GetAllAppointmentsInput input)
        {

            var filteredAppointments = _appointmentRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .Include(e => e.PartnerFk)
                        .Include(e => e.AddedByFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Title.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.TimeZone.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.Contains(input.TitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(input.MinAppointmentDateFilter != null, e => e.AppointmentDate >= input.MinAppointmentDateFilter)
                        .WhereIf(input.MaxAppointmentDateFilter != null, e => e.AppointmentDate <= input.MaxAppointmentDateFilter)
                        .WhereIf(input.MinAppointmentTimeFilter != null, e => e.AppointmentTime >= input.MinAppointmentTimeFilter)
                        .WhereIf(input.MaxAppointmentTimeFilter != null, e => e.AppointmentTime <= input.MaxAppointmentTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TimeZoneFilter), e => e.TimeZone.Contains(input.TimeZoneFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientDisplayPropertyFilter), e => string.Format("{0} {1}", e.ClientFk == null || e.ClientFk.FirstName == null ? "" : e.ClientFk.FirstName.ToString()
                            , e.ClientFk == null || e.ClientFk.LastName == null ? "" : e.ClientFk.LastName.ToString()
                            ) == input.ClientDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.AddedByFk != null && e.AddedByFk.Name == input.UserNameFilter)
                        .WhereIf(input.ClientIdFilter.HasValue, e => false || e.ClientId == input.ClientIdFilter.Value)
                        .WhereIf(input.PartnerIdFilter.HasValue, e => false || e.PartnerId == input.ClientIdFilter.Value);
            var pagedAndFilteredAppointments = filteredAppointments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var appointments = from o in pagedAndFilteredAppointments
                               join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                               from s1 in j1.DefaultIfEmpty()

                               join o2 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o2.Id into j2
                               from s2 in j2.DefaultIfEmpty()

                               join o3 in _lookup_userRepository.GetAll() on o.AddedById equals o3.Id into j3
                               from s3 in j3.DefaultIfEmpty()

                               select new
                               {

                                   o.RelatedTo,
                                   o.Title,
                                   o.Description,
                                   o.AppointmentDate,
                                   o.AppointmentTime,
                                   o.TimeZone,
                                   Id = o.Id,
                                   ClientDisplayProperty = string.Format("{0} {1}", s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString()
               , s1 == null || s1.LastName == null ? "" : s1.LastName.ToString()
               ),
                                   PartnerPartnerName = s2 == null || s2.PartnerName == null ? "" : s2.PartnerName.ToString(),
                                   UserName = s3 == null || s3.Name == null ? "" : s3.Name.ToString()
                               };

            var totalCount = await filteredAppointments.CountAsync();

            var dbList = await appointments.ToListAsync();
            var results = new List<GetAppointmentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetAppointmentForViewDto()
                {
                    Appointment = new AppointmentDto
                    {

                        RelatedTo = o.RelatedTo,
                        Title = o.Title,
                        Description = o.Description,
                        AppointmentDate = o.AppointmentDate,
                        AppointmentTime = o.AppointmentTime,
                        TimeZone = o.TimeZone,
                        Id = o.Id,
                    },
                    ClientDisplayProperty = o.ClientDisplayProperty,
                    PartnerPartnerName = o.PartnerPartnerName,
                    UserName = o.UserName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetAppointmentForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetAppointmentForViewDto> GetAppointmentForView(long id)
        {
            var appointment = await _appointmentRepository.GetAsync(id);

            var output = new GetAppointmentForViewDto { Appointment = ObjectMapper.Map<AppointmentDto>(appointment) };

            if (output.Appointment.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.Appointment.ClientId);
                output.ClientDisplayProperty = string.Format("{0} {1}", _lookupClient.FirstName, _lookupClient.LastName);
            }

            if (output.Appointment.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.Appointment.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.Appointment.AddedById != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.Appointment.AddedById);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Appointments_Edit)]
        public async Task<GetAppointmentForEditOutput> GetAppointmentForEdit(EntityDto<long> input)
        {
            var appointment = await _appointmentRepository.FirstOrDefaultAsync(input.Id);
            var appointmentinvitees = await _appointmentInviteeRepository.GetAllListAsync(p => p.AppointmentId == input.Id);
            var output = new GetAppointmentForEditOutput
            {
                Appointment = ObjectMapper.Map<CreateOrEditAppointmentDto>(appointment),
               Appointmentinvitees = ObjectMapper.Map<List<CreateOrEditAppointmentInviteeDto>>(appointmentinvitees)
            };

            if (output.Appointment.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.Appointment.ClientId);
                output.ClientDisplayProperty = string.Format("{0} {1}", _lookupClient.FirstName, _lookupClient.LastName);
            }

            if (output.Appointment.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.Appointment.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.Appointment.AddedById != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.Appointment.AddedById);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditAppointmentDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Appointments_Create)]
        protected virtual async Task Create([FromBody] CreateOrEditAppointmentDto input)
        {
            var appointment = ObjectMapper.Map<Appointment>(input);

            if (AbpSession.TenantId != null)
            {
                appointment.TenantId = (int)AbpSession.TenantId;
            }
            var appointmentId = _appointmentRepository.InsertAndGetIdAsync(appointment).Result;
            foreach (var step in input.Steps)
            {
                step.AppointmentId = appointmentId;
                var stepEntity = ObjectMapper.Map<AppointmentInvitee>(step);
                await _appointmentInviteeRepository.InsertAsync(stepEntity);
            }
            CurrentUnitOfWork.SaveChanges();
            // await _appointmentRepository.InsertAsync(appointment);

        }

        [AbpAuthorize(AppPermissions.Pages_Appointments_Edit)]
        protected virtual async Task Update(CreateOrEditAppointmentDto input)
        {
            var appointment = await _appointmentRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, appointment);
            var appointmentinvitees = await _appointmentInviteeRepository.GetAllListAsync(p => p.AppointmentId == input.Id);
            foreach (var item in appointmentinvitees)
            {
                await _appointmentInviteeRepository.DeleteAsync(item.Id);
            }
            foreach (var step in input.Steps)
            {
                step.AppointmentId = appointment.Id;
                var stepEntity = ObjectMapper.Map<AppointmentInvitee>(step);
                await _appointmentInviteeRepository.InsertAsync(stepEntity);
            }
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(AppPermissions.Pages_Appointments_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _appointmentRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetAppointmentsToExcel(GetAllAppointmentsForExcelInput input)
        {

            var filteredAppointments = _appointmentRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .Include(e => e.PartnerFk)
                        .Include(e => e.AddedByFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Title.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.TimeZone.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.Contains(input.TitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(input.MinAppointmentDateFilter != null, e => e.AppointmentDate >= input.MinAppointmentDateFilter)
                        .WhereIf(input.MaxAppointmentDateFilter != null, e => e.AppointmentDate <= input.MaxAppointmentDateFilter)
                        .WhereIf(input.MinAppointmentTimeFilter != null, e => e.AppointmentTime >= input.MinAppointmentTimeFilter)
                        .WhereIf(input.MaxAppointmentTimeFilter != null, e => e.AppointmentTime <= input.MaxAppointmentTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TimeZoneFilter), e => e.TimeZone.Contains(input.TimeZoneFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientDisplayPropertyFilter), e => string.Format("{0} {1}", e.ClientFk == null || e.ClientFk.FirstName == null ? "" : e.ClientFk.FirstName.ToString()
, e.ClientFk == null || e.ClientFk.LastName == null ? "" : e.ClientFk.LastName.ToString()
) == input.ClientDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.AddedByFk != null && e.AddedByFk.Name == input.UserNameFilter);

            var query = (from o in filteredAppointments
                         join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _lookup_userRepository.GetAll() on o.AddedById equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         select new GetAppointmentForViewDto()
                         {
                             Appointment = new AppointmentDto
                             {
                                 RelatedTo = o.RelatedTo,
                                 Title = o.Title,
                                 Description = o.Description,
                                 AppointmentDate = o.AppointmentDate,
                                 AppointmentTime = o.AppointmentTime,
                                 TimeZone = o.TimeZone,
                                 Id = o.Id
                             },
                             ClientDisplayProperty = string.Format("{0} {1}", s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString()
, s1 == null || s1.LastName == null ? "" : s1.LastName.ToString()
),
                             PartnerPartnerName = s2 == null || s2.PartnerName == null ? "" : s2.PartnerName.ToString(),
                             UserName = s3 == null || s3.Name == null ? "" : s3.Name.ToString()
                         });

            var appointmentListDtos = await query.ToListAsync();

            return _appointmentsExcelExporter.ExportToFile(appointmentListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Appointments)]
        public async Task<List<AppointmentClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new AppointmentClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = string.Format("{0} {1}", client.FirstName, client.LastName)
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Appointments)]
        public async Task<List<AppointmentPartnerLookupTableDto>> GetAllPartnerForTableDropdown()
        {
            return await _lookup_partnerRepository.GetAll()
                .Select(partner => new AppointmentPartnerLookupTableDto
                {
                    Id = partner.Id,
                    DisplayName = partner == null || partner.PartnerName == null ? "" : partner.PartnerName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Appointments)]
        public async Task<List<AppointmentUserLookupTableDto>> GetAllUserForTableDropdown()
        {
            return await _lookup_userRepository.GetAll()
                .Select(user => new AppointmentUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user == null || user.Name == null ? "" : user.Name.ToString()
                }).ToListAsync();
        }

    }
}