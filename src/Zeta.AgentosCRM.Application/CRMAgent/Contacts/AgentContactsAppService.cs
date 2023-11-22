using Zeta.AgentosCRM.CRMAgent;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMAgent.Contacts.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMAgent.Contacts
{
    [AbpAuthorize(AppPermissions.Pages_AgentContacts)]
    public class AgentContactsAppService : AgentosCRMAppServiceBase, IAgentContactsAppService
    {
        private readonly IRepository<AgentContact, long> _agentContactRepository;
        private readonly IRepository<Agent, long> _lookup_agentRepository;

        public AgentContactsAppService(IRepository<AgentContact, long> agentContactRepository, IRepository<Agent, long> lookup_agentRepository)
        {
            _agentContactRepository = agentContactRepository;
            _lookup_agentRepository = lookup_agentRepository;

        }

        public async Task<PagedResultDto<GetAgentContactForViewDto>> GetAll(GetAllAgentContactsInput input)
        {

            var filteredAgentContacts = _agentContactRepository.GetAll()
                        .Include(e => e.AgentFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.PhoneCode.Contains(input.Filter) || e.Email.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo.Contains(input.PhoneNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneCodeFilter), e => e.PhoneCode.Contains(input.PhoneCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.Contains(input.EmailFilter))
                        .WhereIf(input.IsPrimaryFilter.HasValue && input.IsPrimaryFilter > -1, e => (input.IsPrimaryFilter == 1 && e.IsPrimary) || (input.IsPrimaryFilter == 0 && !e.IsPrimary))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AgentNameFilter), e => e.AgentFk != null && e.AgentFk.Name == input.AgentNameFilter);

            var pagedAndFilteredAgentContacts = filteredAgentContacts
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var agentContacts = from o in pagedAndFilteredAgentContacts
                                join o1 in _lookup_agentRepository.GetAll() on o.AgentId equals o1.Id into j1
                                from s1 in j1.DefaultIfEmpty()

                                select new
                                {

                                    o.Name,
                                    o.PhoneNo,
                                    o.PhoneCode,
                                    o.Email,
                                    o.IsPrimary,
                                    Id = o.Id,
                                    AgentName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                                };

            var totalCount = await filteredAgentContacts.CountAsync();

            var dbList = await agentContacts.ToListAsync();
            var results = new List<GetAgentContactForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetAgentContactForViewDto()
                {
                    AgentContact = new AgentContactDto
                    {

                        Name = o.Name,
                        PhoneNo = o.PhoneNo,
                        PhoneCode = o.PhoneCode,
                        Email = o.Email,
                        IsPrimary = o.IsPrimary,
                        Id = o.Id,
                    },
                    AgentName = o.AgentName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetAgentContactForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetAgentContactForViewDto> GetAgentContactForView(long id)
        {
            var agentContact = await _agentContactRepository.GetAsync(id);

            var output = new GetAgentContactForViewDto { AgentContact = ObjectMapper.Map<AgentContactDto>(agentContact) };

            if (output.AgentContact.AgentId != null)
            {
                var _lookupAgent = await _lookup_agentRepository.FirstOrDefaultAsync((long)output.AgentContact.AgentId);
                output.AgentName = _lookupAgent?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_AgentContacts_Edit)]
        public async Task<GetAgentContactForEditOutput> GetAgentContactForEdit(EntityDto<long> input)
        {
            var agentContact = await _agentContactRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetAgentContactForEditOutput { AgentContact = ObjectMapper.Map<CreateOrEditAgentContactDto>(agentContact) };

            if (output.AgentContact.AgentId != null)
            {
                var _lookupAgent = await _lookup_agentRepository.FirstOrDefaultAsync((long)output.AgentContact.AgentId);
                output.AgentName = _lookupAgent?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditAgentContactDto input)
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

        [AbpAuthorize(AppPermissions.Pages_AgentContacts_Create)]
        protected virtual async Task Create(CreateOrEditAgentContactDto input)
        {
            var agentContact = ObjectMapper.Map<AgentContact>(input);

            if (AbpSession.TenantId != null)
            {
                agentContact.TenantId = (int)AbpSession.TenantId;
            }

            await _agentContactRepository.InsertAsync(agentContact);

        }

        [AbpAuthorize(AppPermissions.Pages_AgentContacts_Edit)]
        protected virtual async Task Update(CreateOrEditAgentContactDto input)
        {
            var agentContact = await _agentContactRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, agentContact);

        }

        [AbpAuthorize(AppPermissions.Pages_AgentContacts_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _agentContactRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_AgentContacts)]
        public async Task<List<AgentContactAgentLookupTableDto>> GetAllAgentForTableDropdown()
        {
            return await _lookup_agentRepository.GetAll()
                .Select(agent => new AgentContactAgentLookupTableDto
                {
                    Id = agent.Id,
                    DisplayName = agent == null || agent.Name == null ? "" : agent.Name.ToString()
                }).ToListAsync();
        }

    }
}