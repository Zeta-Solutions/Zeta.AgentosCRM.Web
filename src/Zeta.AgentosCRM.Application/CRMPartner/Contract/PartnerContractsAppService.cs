using Zeta.AgentosCRM.CRMAgent;
using Zeta.AgentosCRM.CRMSetup.Regions;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMPartner.Contract.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMPartner.Contract
{
    [AbpAuthorize(AppPermissions.Pages_PartnerContracts)]
    public class PartnerContractsAppService : AgentosCRMAppServiceBase, IPartnerContractsAppService
    {
        private readonly IRepository<PartnerContract> _partnerContractRepository;
        private readonly IRepository<Agent, long> _lookup_agentRepository;
        private readonly IRepository<Region, int> _lookup_regionRepository;

        public PartnerContractsAppService(IRepository<PartnerContract> partnerContractRepository, IRepository<Agent, long> lookup_agentRepository, IRepository<Region, int> lookup_regionRepository)
        {
            _partnerContractRepository = partnerContractRepository;
            _lookup_agentRepository = lookup_agentRepository;
            _lookup_regionRepository = lookup_regionRepository;

        }

        public async Task<PagedResultDto<GetPartnerContractForViewDto>> GetAll(GetAllPartnerContractsInput input)
        {

            var filteredPartnerContracts = _partnerContractRepository.GetAll()
                        .Include(e => e.AgentFk)
                        .Include(e => e.RegionFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinContractExpiryDateFilter != null, e => e.ContractExpiryDate >= input.MinContractExpiryDateFilter)
                        .WhereIf(input.MaxContractExpiryDateFilter != null, e => e.ContractExpiryDate <= input.MaxContractExpiryDateFilter)
                        .WhereIf(input.MinCommissionPerFilter != null, e => e.CommissionPer >= input.MinCommissionPerFilter)
                        .WhereIf(input.MaxCommissionPerFilter != null, e => e.CommissionPer <= input.MaxCommissionPerFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AgentNameFilter), e => e.AgentFk != null && e.AgentFk.Name == input.AgentNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RegionNameFilter), e => e.RegionFk != null && e.RegionFk.Name == input.RegionNameFilter);

            var pagedAndFilteredPartnerContracts = filteredPartnerContracts
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var partnerContracts = from o in pagedAndFilteredPartnerContracts
                                   join o1 in _lookup_agentRepository.GetAll() on o.AgentId equals o1.Id into j1
                                   from s1 in j1.DefaultIfEmpty()

                                   join o2 in _lookup_regionRepository.GetAll() on o.RegionId equals o2.Id into j2
                                   from s2 in j2.DefaultIfEmpty()

                                   select new
                                   {

                                       o.ContractExpiryDate,
                                       o.CommissionPer,
                                       Id = o.Id,
                                       AgentName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                       RegionName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                                   };

            var totalCount = await filteredPartnerContracts.CountAsync();

            var dbList = await partnerContracts.ToListAsync();
            var results = new List<GetPartnerContractForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPartnerContractForViewDto()
                {
                    PartnerContract = new PartnerContractDto
                    {

                        ContractExpiryDate = o.ContractExpiryDate,
                        CommissionPer = o.CommissionPer,
                        Id = o.Id,
                    },
                    AgentName = o.AgentName,
                    RegionName = o.RegionName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPartnerContractForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetPartnerContractForViewDto> GetPartnerContractForView(int id)
        {
            var partnerContract = await _partnerContractRepository.GetAsync(id);

            var output = new GetPartnerContractForViewDto { PartnerContract = ObjectMapper.Map<PartnerContractDto>(partnerContract) };

            if (output.PartnerContract.AgentId != null)
            {
                var _lookupAgent = await _lookup_agentRepository.FirstOrDefaultAsync((long)output.PartnerContract.AgentId);
                output.AgentName = _lookupAgent?.Name?.ToString();
            }

            if (output.PartnerContract.RegionId != null)
            {
                var _lookupRegion = await _lookup_regionRepository.FirstOrDefaultAsync((int)output.PartnerContract.RegionId);
                output.RegionName = _lookupRegion?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_PartnerContracts_Edit)]
        public async Task<GetPartnerContractForEditOutput> GetPartnerContractForEdit(EntityDto input)
        {
            var partnerContract = await _partnerContractRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPartnerContractForEditOutput { PartnerContract = ObjectMapper.Map<CreateOrEditPartnerContractDto>(partnerContract) };

            if (output.PartnerContract.AgentId != null)
            {
                var _lookupAgent = await _lookup_agentRepository.FirstOrDefaultAsync((long)output.PartnerContract.AgentId);
                output.AgentName = _lookupAgent?.Name?.ToString();
            }

            if (output.PartnerContract.RegionId != null)
            {
                var _lookupRegion = await _lookup_regionRepository.FirstOrDefaultAsync((int)output.PartnerContract.RegionId);
                output.RegionName = _lookupRegion?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPartnerContractDto input)
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

        [AbpAuthorize(AppPermissions.Pages_PartnerContracts_Create)]
        protected virtual async Task Create(CreateOrEditPartnerContractDto input)
        {
            var partnerContract = ObjectMapper.Map<PartnerContract>(input);

            if (AbpSession.TenantId != null)
            {
                partnerContract.TenantId = (int)AbpSession.TenantId;
            }

            await _partnerContractRepository.InsertAsync(partnerContract);

        }

        [AbpAuthorize(AppPermissions.Pages_PartnerContracts_Edit)]
        protected virtual async Task Update(CreateOrEditPartnerContractDto input)
        {
            var partnerContract = await _partnerContractRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, partnerContract);

        }

        [AbpAuthorize(AppPermissions.Pages_PartnerContracts_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _partnerContractRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_PartnerContracts)]
        public async Task<List<PartnerContractAgentLookupTableDto>> GetAllAgentForTableDropdown()
        {
            return await _lookup_agentRepository.GetAll()
                .Select(agent => new PartnerContractAgentLookupTableDto
                {
                    Id = agent.Id,
                    DisplayName = agent == null || agent.Name == null ? "" : agent.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_PartnerContracts)]
        public async Task<List<PartnerContractRegionLookupTableDto>> GetAllRegionForTableDropdown()
        {
            return await _lookup_regionRepository.GetAll()
                .Select(region => new PartnerContractRegionLookupTableDto
                {
                    Id = region.Id,
                    DisplayName = region == null || region.Name == null ? "" : region.Name.ToString()
                }).ToListAsync();
        }

    }
}