using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.Authorization.Users;

using Zeta.AgentosCRM.CRMClient.CheckIn;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMClient.CheckIn.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMClient.CheckIn
{
    [AbpAuthorize(AppPermissions.Pages_CheckInLogs)]
    public class CheckInLogsAppService : AgentosCRMAppServiceBase, ICheckInLogsAppService
    {
        private readonly IRepository<CheckInLog, long> _checkInLogRepository;
        private readonly IRepository<Client, long> _lookup_clientRepository;
        private readonly IRepository<User, long> _lookup_userRepository;

        public CheckInLogsAppService(IRepository<CheckInLog, long> checkInLogRepository, IRepository<Client, long> lookup_clientRepository, IRepository<User, long> lookup_userRepository)
        {
            _checkInLogRepository = checkInLogRepository;
            _lookup_clientRepository = lookup_clientRepository;
            _lookup_userRepository = lookup_userRepository;

        }

        public async Task<PagedResultDto<GetCheckInLogForViewDto>> GetAll(GetAllCheckInLogsInput input)
        {
            var checkInStatusFilter = input.CheckInStatusFilter.HasValue
                        ? (CheckInStatus)input.CheckInStatusFilter
                        : default;

            var filteredCheckInLogs = _checkInLogRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .Include(e => e.CheckInAssigneeFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Title.Contains(input.Filter) || e.CheckInPurpose.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.Contains(input.TitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CheckInPurposeFilter), e => e.CheckInPurpose.Contains(input.CheckInPurposeFilter))
                        .WhereIf(input.CheckInStatusFilter.HasValue && input.CheckInStatusFilter > -1, e => e.CheckInStatus == checkInStatusFilter)
                        .WhereIf(input.MinCheckInDateFilter != null, e => e.CheckInDate >= input.MinCheckInDateFilter)
                        .WhereIf(input.MaxCheckInDateFilter != null, e => e.CheckInDate <= input.MaxCheckInDateFilter)
                        .WhereIf(input.MinStartTimeFilter != null, e => e.StartTime >= input.MinStartTimeFilter)
                        .WhereIf(input.MaxStartTimeFilter != null, e => e.StartTime <= input.MaxStartTimeFilter)
                        .WhereIf(input.MinEndTimeFilter != null, e => e.EndTime >= input.MinEndTimeFilter)
                        .WhereIf(input.MaxEndTimeFilter != null, e => e.EndTime <= input.MaxEndTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientDisplayPropertyFilter), e => string.Format("{0} {1}", e.ClientFk == null || e.ClientFk.FirstName == null ? "" : e.ClientFk.FirstName.ToString()
, e.ClientFk == null || e.ClientFk.LastName == null ? "" : e.ClientFk.LastName.ToString()
) == input.ClientDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.CheckInAssigneeFk != null && e.CheckInAssigneeFk.Name == input.UserNameFilter)
                        .WhereIf(input.ClientIdFilter.HasValue, e => false || e.ClientId == input.ClientIdFilter.Value);
            var pagedAndFilteredCheckInLogs = filteredCheckInLogs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var checkInLogs = from o in pagedAndFilteredCheckInLogs
                              join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                              from s1 in j1.DefaultIfEmpty()

                              join o2 in _lookup_userRepository.GetAll() on o.CheckInAssigneeId equals o2.Id into j2
                              from s2 in j2.DefaultIfEmpty()

                              select new
                              {

                                  o.Title,
                                  o.CheckInPurpose,
                                  o.CheckInStatus,
                                  o.CheckInDate,
                                  o.StartTime,
                                  o.EndTime,
                                  Id = o.Id,
                                  ClientDisplayProperty = string.Format("{0} {1}", s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString()
              , s1 == null || s1.LastName == null ? "" : s1.LastName.ToString()
              ),
                                  UserName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                              };

            var totalCount = await filteredCheckInLogs.CountAsync();

            var dbList = await checkInLogs.ToListAsync();
            var results = new List<GetCheckInLogForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCheckInLogForViewDto()
                {
                    CheckInLog = new CheckInLogDto
                    {

                        Title = o.Title,
                        CheckInPurpose = o.CheckInPurpose,
                        CheckInStatus = o.CheckInStatus,
                        CheckInDate = o.CheckInDate,
                        StartTime = o.StartTime,
                        EndTime = o.EndTime,
                        Id = o.Id,
                    },
                    ClientDisplayProperty = o.ClientDisplayProperty,
                    UserName = o.UserName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCheckInLogForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetCheckInLogForViewDto> GetCheckInLogForView(long id)
        {
            var checkInLog = await _checkInLogRepository.GetAsync(id);

            var output = new GetCheckInLogForViewDto { CheckInLog = ObjectMapper.Map<CheckInLogDto>(checkInLog) };

            if (output.CheckInLog.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.CheckInLog.ClientId);
                output.ClientDisplayProperty = string.Format("{0} {1}", _lookupClient.FirstName, _lookupClient.LastName);
            }

            if (output.CheckInLog.CheckInAssigneeId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.CheckInLog.CheckInAssigneeId);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CheckInLogs_Edit)]
        public async Task<GetCheckInLogForEditOutput> GetCheckInLogForEdit(EntityDto<long> input)
        {
            var checkInLog = await _checkInLogRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCheckInLogForEditOutput { CheckInLog = ObjectMapper.Map<CreateOrEditCheckInLogDto>(checkInLog) };

            if (output.CheckInLog.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.CheckInLog.ClientId);
                output.ClientDisplayProperty = string.Format("{0} {1}", _lookupClient.FirstName, _lookupClient.LastName);
            }

            if (output.CheckInLog.CheckInAssigneeId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.CheckInLog.CheckInAssigneeId);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCheckInLogDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CheckInLogs_Create)]
        protected virtual async Task Create(CreateOrEditCheckInLogDto input)
        {
            var checkInLog = ObjectMapper.Map<CheckInLog>(input);

            if (AbpSession.TenantId != null)
            {
                checkInLog.TenantId = (int)AbpSession.TenantId;
            }

            await _checkInLogRepository.InsertAsync(checkInLog);

        }

        [AbpAuthorize(AppPermissions.Pages_CheckInLogs_Edit)]
        protected virtual async Task Update(CreateOrEditCheckInLogDto input)
        {
            var checkInLog = await _checkInLogRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, checkInLog);

        }

        [AbpAuthorize(AppPermissions.Pages_CheckInLogs_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _checkInLogRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_CheckInLogs)]
        public async Task<List<CheckInLogClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new CheckInLogClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = string.Format("{0} {1}", client.FirstName, client.LastName)
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CheckInLogs)]
        public async Task<List<CheckInLogUserLookupTableDto>> GetAllUserForTableDropdown()
        {
            return await _lookup_userRepository.GetAll()
                .Select(user => new CheckInLogUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user == null || user.Name == null ? "" : user.Name.ToString()
                }).ToListAsync();
        }

    }
}