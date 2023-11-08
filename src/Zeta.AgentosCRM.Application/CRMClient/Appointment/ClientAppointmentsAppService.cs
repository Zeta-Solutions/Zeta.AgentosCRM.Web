using Zeta.AgentosCRM.CRMClient;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMClient.Appointment.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMClient.Appointment
{
    [AbpAuthorize(AppPermissions.Pages_ClientAppointments)]
    public class ClientAppointmentsAppService : AgentosCRMAppServiceBase, IClientAppointmentsAppService
    {
        private readonly IRepository<ClientAppointment, long> _clientAppointmentRepository;
        private readonly IRepository<Client, long> _lookup_clientRepository;

        public ClientAppointmentsAppService(IRepository<ClientAppointment, long> clientAppointmentRepository, IRepository<Client, long> lookup_clientRepository)
        {
            _clientAppointmentRepository = clientAppointmentRepository;
            _lookup_clientRepository = lookup_clientRepository;

        }

        public async Task<PagedResultDto<GetClientAppointmentForViewDto>> GetAll(GetAllClientAppointmentsInput input)
        {

            var filteredClientAppointments = _clientAppointmentRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TimeZone.Contains(input.Filter) || e.Title.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TimeZoneFilter), e => e.TimeZone.Contains(input.TimeZoneFilter))
                        .WhereIf(input.MinAppointmentDateFilter != null, e => e.AppointmentDate >= input.MinAppointmentDateFilter)
                        .WhereIf(input.MaxAppointmentDateFilter != null, e => e.AppointmentDate <= input.MaxAppointmentDateFilter)
                        .WhereIf(input.MinAppointmentTimeFilter != null, e => e.AppointmentTime >= input.MinAppointmentTimeFilter)
                        .WhereIf(input.MaxAppointmentTimeFilter != null, e => e.AppointmentTime <= input.MaxAppointmentTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.Contains(input.TitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientDisplayPropertyFilter), e => string.Format("{0} {1} {2}", e.ClientFk == null || e.ClientFk.FirstName == null ? "" : e.ClientFk.FirstName.ToString()
, e.ClientFk == null || e.ClientFk.LastName == null ? "" : e.ClientFk.LastName.ToString()
, e.ClientFk == null || e.ClientFk.Email == null ? "" : e.ClientFk.Email.ToString()
) == input.ClientDisplayPropertyFilter);

            var pagedAndFilteredClientAppointments = filteredClientAppointments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var clientAppointments = from o in pagedAndFilteredClientAppointments
                                     join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                                     from s1 in j1.DefaultIfEmpty()

                                     select new
                                     {

                                         o.TimeZone,
                                         o.AppointmentDate,
                                         o.AppointmentTime,
                                         o.Title,
                                         o.Description,
                                         Id = o.Id,
                                         ClientDisplayProperty = string.Format("{0} {1} {2}", s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString()
                     , s1 == null || s1.LastName == null ? "" : s1.LastName.ToString()
                     , s1 == null || s1.Email == null ? "" : s1.Email.ToString()
                     )
                                     };

            var totalCount = await filteredClientAppointments.CountAsync();

            var dbList = await clientAppointments.ToListAsync();
            var results = new List<GetClientAppointmentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetClientAppointmentForViewDto()
                {
                    ClientAppointment = new ClientAppointmentDto
                    {

                        TimeZone = o.TimeZone,
                        AppointmentDate = o.AppointmentDate,
                        AppointmentTime = o.AppointmentTime,
                        Title = o.Title,
                        Description = o.Description,
                        Id = o.Id,
                    },
                    ClientDisplayProperty = o.ClientDisplayProperty
                };

                results.Add(res);
            }

            return new PagedResultDto<GetClientAppointmentForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetClientAppointmentForViewDto> GetClientAppointmentForView(long id)
        {
            var clientAppointment = await _clientAppointmentRepository.GetAsync(id);

            var output = new GetClientAppointmentForViewDto { ClientAppointment = ObjectMapper.Map<ClientAppointmentDto>(clientAppointment) };

            if (output.ClientAppointment.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.ClientAppointment.ClientId);
                output.ClientDisplayProperty = string.Format("{0} {1} {2}", _lookupClient.FirstName, _lookupClient.LastName, _lookupClient.Email);
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ClientAppointments_Edit)]
        public async Task<GetClientAppointmentForEditOutput> GetClientAppointmentForEdit(EntityDto<long> input)
        {
            var clientAppointment = await _clientAppointmentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetClientAppointmentForEditOutput { ClientAppointment = ObjectMapper.Map<CreateOrEditClientAppointmentDto>(clientAppointment) };

            if (output.ClientAppointment.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.ClientAppointment.ClientId);
                output.ClientDisplayProperty = string.Format("{0} {1} {2}", _lookupClient.FirstName, _lookupClient.LastName, _lookupClient.Email);
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditClientAppointmentDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ClientAppointments_Create)]
        protected virtual async Task Create(CreateOrEditClientAppointmentDto input)
        {
            var clientAppointment = ObjectMapper.Map<ClientAppointment>(input);

            if (AbpSession.TenantId != null)
            {
                clientAppointment.TenantId = (int)AbpSession.TenantId;
            }

            await _clientAppointmentRepository.InsertAsync(clientAppointment);

        }

        [AbpAuthorize(AppPermissions.Pages_ClientAppointments_Edit)]
        protected virtual async Task Update(CreateOrEditClientAppointmentDto input)
        {
            var clientAppointment = await _clientAppointmentRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, clientAppointment);

        }

        [AbpAuthorize(AppPermissions.Pages_ClientAppointments_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _clientAppointmentRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_ClientAppointments)]
        public async Task<List<ClientAppointmentClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new ClientAppointmentClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = string.Format("{0} {1} {2}", client.FirstName, client.LastName, client.Email)
                }).ToListAsync();
        }

    }
}