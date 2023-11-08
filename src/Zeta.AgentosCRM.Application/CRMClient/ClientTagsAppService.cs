using Zeta.AgentosCRM.CRMSetup.Tag;
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
    [AbpAuthorize(AppPermissions.Pages_ClientTags)]
    public class ClientTagsAppService : AgentosCRMAppServiceBase, IClientTagsAppService
    {
        private readonly IRepository<ClientTag> _clientTagRepository;
        private readonly IRepository<Client, long> _lookup_clientRepository;
        private readonly IRepository<Tag, int> _lookup_tagRepository;

        public ClientTagsAppService(IRepository<ClientTag> clientTagRepository, IRepository<Client, long> lookup_clientRepository, IRepository<Tag, int> lookup_tagRepository)
        {
            _clientTagRepository = clientTagRepository;
            _lookup_clientRepository = lookup_clientRepository;
            _lookup_tagRepository = lookup_tagRepository;

        }

        public async Task<PagedResultDto<GetClientTagForViewDto>> GetAll(GetAllClientTagsInput input)
        {

            var filteredClientTags = _clientTagRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .Include(e => e.TagFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientFirstNameFilter), e => e.ClientFk != null && e.ClientFk.FirstName == input.ClientFirstNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TagNameFilter), e => e.TagFk != null && e.TagFk.Name == input.TagNameFilter);

            var pagedAndFilteredClientTags = filteredClientTags
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var clientTags = from o in pagedAndFilteredClientTags
                             join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                             from s1 in j1.DefaultIfEmpty()

                             join o2 in _lookup_tagRepository.GetAll() on o.TagId equals o2.Id into j2
                             from s2 in j2.DefaultIfEmpty()

                             select new
                             {

                                 Id = o.Id,
                                 ClientFirstName = s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString(),
                                 TagName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                             };

            var totalCount = await filteredClientTags.CountAsync();

            var dbList = await clientTags.ToListAsync();
            var results = new List<GetClientTagForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetClientTagForViewDto()
                {
                    ClientTag = new ClientTagDto
                    {

                        Id = o.Id,
                    },
                    ClientFirstName = o.ClientFirstName,
                    TagName = o.TagName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetClientTagForViewDto>(
                totalCount,
                results
            );

        }


        public async Task<GetClientTagForViewDto> GetClientTagForView(int id)
        {
            var clientTag = await _clientTagRepository.GetAsync(id);

            var output = new GetClientTagForViewDto { ClientTag = ObjectMapper.Map<ClientTagDto>(clientTag) };

            if (output.ClientTag.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.ClientTag.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            if (output.ClientTag.TagId != null)
            {
                var _lookupTag = await _lookup_tagRepository.FirstOrDefaultAsync((int)output.ClientTag.TagId);
                output.TagName = _lookupTag?.Name?.ToString();
            }

            return output;
        }


        [AbpAuthorize(AppPermissions.Pages_ClientTags_Edit)]
        public async Task<GetClientTagForEditOutput> GetClientTagForEdit(EntityDto input)
        {
            var clientTag = await _clientTagRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetClientTagForEditOutput { ClientTag = ObjectMapper.Map<CreateOrEditClientTagDto>(clientTag) };

            if (output.ClientTag.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.ClientTag.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            if (output.ClientTag.TagId != null)
            {
                var _lookupTag = await _lookup_tagRepository.FirstOrDefaultAsync((int)output.ClientTag.TagId);
                output.TagName = _lookupTag?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditClientTagDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ClientTags_Create)]
        protected virtual async Task Create(CreateOrEditClientTagDto input)
        {
            var clientTag = ObjectMapper.Map<ClientTag>(input);

            if (AbpSession.TenantId != null)
            {
                clientTag.TenantId = (int)AbpSession.TenantId;
            }

            await _clientTagRepository.InsertAsync(clientTag);

        }

        [AbpAuthorize(AppPermissions.Pages_ClientTags_Edit)]
        protected virtual async Task Update(CreateOrEditClientTagDto input)
        {
            var clientTag = await _clientTagRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, clientTag);

        }

        [AbpAuthorize(AppPermissions.Pages_ClientTags_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _clientTagRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_ClientTags)]
        public async Task<List<ClientTagClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new ClientTagClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = client == null || client.FirstName == null ? "" : client.FirstName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ClientTags)]
        public async Task<List<ClientTagTagLookupTableDto>> GetAllTagForTableDropdown()
        {
            return await _lookup_tagRepository.GetAll()
                .Select(tag => new ClientTagTagLookupTableDto
                {
                    Id = tag.Id,
                    DisplayName = tag == null || tag.Name == null ? "" : tag.Name.ToString()
                }).ToListAsync();
        }

    }
}