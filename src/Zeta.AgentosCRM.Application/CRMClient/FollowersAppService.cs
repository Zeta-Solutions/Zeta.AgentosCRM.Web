using Zeta.AgentosCRM.Authorization.Users;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMClient.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMClient
{
    [AbpAuthorize(AppPermissions.Pages_Followers)]
    public class FollowersAppService : AgentosCRMAppServiceBase, IFollowersAppService
    {
        private readonly IRepository<Follower> _followerRepository;
        private readonly IRepository<Client, long> _lookup_clientRepository;
        private readonly IRepository<User, long> _lookup_userRepository;

        public FollowersAppService(IRepository<Follower> followerRepository, IRepository<Client, long> lookup_clientRepository, IRepository<User, long> lookup_userRepository)
        {
            _followerRepository = followerRepository;
            _lookup_clientRepository = lookup_clientRepository;
            _lookup_userRepository = lookup_userRepository;

        }

        public async Task<PagedResultDto<GetFollowerForViewDto>> GetAll(GetAllFollowersInput input)
        {

            var filteredFollowers = _followerRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .Include(e => e.UserFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientFirstNameFilter), e => e.ClientFk != null && e.ClientFk.FirstName == input.ClientFirstNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserFk != null && e.UserFk.Name == input.UserNameFilter);

            var pagedAndFilteredFollowers = filteredFollowers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var followers = from o in pagedAndFilteredFollowers
                            join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()

                            join o2 in _lookup_userRepository.GetAll() on o.UserId equals o2.Id into j2
                            from s2 in j2.DefaultIfEmpty()

                            select new
                            {

                                Id = o.Id,
                                ClientFirstName = s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString(),
                                UserName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                            };

            var totalCount = await filteredFollowers.CountAsync();

            var dbList = await followers.ToListAsync();
            var results = new List<GetFollowerForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetFollowerForViewDto()
                {
                    Follower = new FollowerDto
                    {

                        Id = o.Id,
                    },
                    ClientFirstName = o.ClientFirstName,
                    UserName = o.UserName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetFollowerForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetFollowerForViewDto> GetFollowerForView(int id)
        {
            var follower = await _followerRepository.GetAsync(id);

            var output = new GetFollowerForViewDto { Follower = ObjectMapper.Map<FollowerDto>(follower) };

            if (output.Follower.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.Follower.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            if (output.Follower.UserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.Follower.UserId);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            return output;
        }


        [AbpAuthorize(AppPermissions.Pages_Followers_Edit)]
        public async Task<GetFollowerForEditOutput> GetFollowerForEdit(EntityDto input)
        {
            var follower = await _followerRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetFollowerForEditOutput { Follower = ObjectMapper.Map<CreateOrEditFollowerDto>(follower) };

            if (output.Follower.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.Follower.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            if (output.Follower.UserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.Follower.UserId);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditFollowerDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Followers_Create)]
        protected virtual async Task Create(CreateOrEditFollowerDto input)
        {
            var follower = ObjectMapper.Map<Follower>(input);

            if (AbpSession.TenantId != null)
            {
                follower.TenantId = (int)AbpSession.TenantId;
            }

            await _followerRepository.InsertAsync(follower);

        }

        [AbpAuthorize(AppPermissions.Pages_Followers_Edit)]
        protected virtual async Task Update(CreateOrEditFollowerDto input)
        {
            var follower = await _followerRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, follower);

        }

        [AbpAuthorize(AppPermissions.Pages_Followers_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _followerRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_Followers)]
        public async Task<List<FollowerClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new FollowerClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = client == null || client.FirstName == null ? "" : client.FirstName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Followers)]
        public async Task<List<FollowerUserLookupTableDto>> GetAllUserForTableDropdown()
        {
            return await _lookup_userRepository.GetAll()
                .Select(user => new FollowerUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user == null || user.Name == null ? "" : user.Name.ToString()
                }).ToListAsync();
        }

    }
}