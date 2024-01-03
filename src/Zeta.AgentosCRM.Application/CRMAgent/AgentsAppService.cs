using Zeta.AgentosCRM.CRMSetup.Countries;
using Abp.Organizations;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMAgent.Exporting;
using Zeta.AgentosCRM.CRMAgent.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMApplications;

namespace Zeta.AgentosCRM.CRMAgent
{
    [AbpAuthorize(AppPermissions.Pages_Agents)]
    public class AgentsAppService : AgentosCRMAppServiceBase, IAgentsAppService
    {
        private readonly IRepository<Agent, long> _agentRepository;
        private readonly IAgentsExcelExporter _agentsExcelExporter;
        private readonly IRepository<Country, int> _lookup_countryRepository;
        private readonly IRepository<OrganizationUnit, long> _lookup_organizationUnitRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<BinaryObject, Guid> _lookup_binaryObjectRepository;
        private readonly IRepository<Client,long> _lookup_clientRepository;
        private readonly IRepository<Application,long> _lookup_applicationRepository;
        public AgentsAppService(
            IRepository<Agent, long> agentRepository, IAgentsExcelExporter agentsExcelExporter,
            IRepository<Country, int> lookup_countryRepository, IRepository<OrganizationUnit, long> lookup_organizationUnitRepository,
            ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager,
            IRepository<BinaryObject, Guid> lookup_binaryObjectRepository, IRepository<Client, long> lookup_clientRepository
, IRepository<Application, long> lookup_applicationRepository)
        {
            _agentRepository = agentRepository;
            _agentsExcelExporter = agentsExcelExporter;
            _lookup_countryRepository = lookup_countryRepository;
            _lookup_organizationUnitRepository = lookup_organizationUnitRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            _lookup_binaryObjectRepository = lookup_binaryObjectRepository;
            _lookup_clientRepository = lookup_clientRepository;
            _lookup_applicationRepository = lookup_applicationRepository;
        }

        public async Task<PagedResultDto<GetAgentForViewDto>> GetAll(GetAllAgentsInput input)
        {

            var filteredAgents = _agentRepository.GetAll()
                        .Include(e => e.CountryFk)
                        .Include(e => e.OrganizationUnitFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.PhoneCode.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.City.Contains(input.Filter) || e.Street.Contains(input.Filter) || e.State.Contains(input.Filter) || e.PrimaryContactName.Contains(input.Filter) || e.TaxNo.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(input.IsSuperAgentFilter.HasValue && input.IsSuperAgentFilter > -1, e => (input.IsSuperAgentFilter == 1 && e.IsSuperAgent) || (input.IsSuperAgentFilter == 0 && !e.IsSuperAgent))
                        .WhereIf(input.IsBusinessFilter.HasValue && input.IsBusinessFilter > -1, e => (input.IsBusinessFilter == 1 && e.IsBusiness) || (input.IsBusinessFilter == 0 && !e.IsBusiness))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo.Contains(input.PhoneNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.Contains(input.EmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CityFilter), e => e.City.Contains(input.CityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StreetFilter), e => e.Street.Contains(input.StreetFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StateFilter), e => e.State.Contains(input.StateFilter))
                        .WhereIf(input.MinIncomeSharingPerFilter != null, e => e.IncomeSharingPer >= input.MinIncomeSharingPerFilter)
                        .WhereIf(input.MaxIncomeSharingPerFilter != null, e => e.IncomeSharingPer <= input.MaxIncomeSharingPerFilter)
                        .WhereIf(input.MinTaxFilter != null, e => e.Tax >= input.MinTaxFilter)
                        .WhereIf(input.MaxTaxFilter != null, e => e.Tax <= input.MaxTaxFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PrimaryContactNameFilter), e => e.PrimaryContactName.Contains(input.PrimaryContactNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TaxNoFilter), e => e.TaxNo.Contains(input.TaxNoFilter))
                        .WhereIf(input.MinContractExpiryDateFilter != null, e => e.ContractExpiryDate >= input.MinContractExpiryDateFilter)
                        .WhereIf(input.MaxContractExpiryDateFilter != null, e => e.ContractExpiryDate <= input.MaxContractExpiryDateFilter)
                        .WhereIf(input.MinClaimRevenuePerFilter != null, e => e.ClaimRevenuePer >= input.MinClaimRevenuePerFilter)
                        .WhereIf(input.MaxClaimRevenuePerFilter != null, e => e.ClaimRevenuePer <= input.MaxClaimRevenuePerFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CountryNameFilter), e => e.CountryFk != null && e.CountryFk.Name == input.CountryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.OrganizationUnitFk != null && e.OrganizationUnitFk.DisplayName == input.OrganizationUnitDisplayNameFilter);

            var pagedAndFilteredAgents = filteredAgents
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var agents = from o in pagedAndFilteredAgents
                         join o1 in _lookup_countryRepository.GetAll() on o.CountryId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_organizationUnitRepository.GetAll() on o.OrganizationUnitId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_binaryObjectRepository.GetAll() on o.ProfilePictureId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                          
                         let clientCount = (from p in _lookup_clientRepository.GetAll()
                                             where o.Id == p.AgentId
                                             select p.Id
                                           ).Count()


                         let ApplicationCount = (
                         from o1 in _lookup_clientRepository.GetAll()
                         where o.Id == o1.AgentId
                         join o2 in _lookup_applicationRepository.GetAll() on o1.Id equals o2.ClientId into j2
                         from s2 in j2.DefaultIfEmpty()
                         where o1.Id == s2.ClientId
                         select s2.Id
                         //from a in _lookup_applicationRepository.GetAll()
                         //                        where o.Id == a.PartnerId && a.IsDiscontinue == false
                         //                        select a.Id
                                                 )
                                 .Count()
                         select new
                         {

                            o.Name,
                             o.IsSuperAgent,
                             o.IsBusiness,
                             o.PhoneNo,
                             o.PhoneCode,
                             o.Email,
                             o.City,
                             o.Street,
                             o.State,
                             o.IncomeSharingPer,
                             o.Tax,
                             o.ProfilePictureId,
                             o.PrimaryContactName,
                             o.TaxNo,
                             o.ContractExpiryDate,
                             o.ClaimRevenuePer,
                             Id = o.Id,
                             clientCount,
                             ApplicationCount,
                             CountryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                             OrganizationUnitDisplayName = s2 == null || s2.DisplayName == null ? "" : s2.DisplayName.ToString(),
                             ImageBytes = s3 == null || s3.Bytes == null ? "" : Convert.ToBase64String(s3.Bytes),
                         };

            var totalCount = await filteredAgents.CountAsync();

            var dbList = await agents.ToListAsync();
            var results = new List<GetAgentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetAgentForViewDto()
                {
                    Agent = new AgentDto
                    {

                        Name = o.Name,
                        IsSuperAgent = o.IsSuperAgent,
                        IsBusiness = o.IsBusiness,
                        PhoneNo = o.PhoneNo,
                        PhoneCode = o.PhoneCode,
                        Email = o.Email,
                        City = o.City,
                        Street = o.Street,
                        State = o.State,
                        IncomeSharingPer = o.IncomeSharingPer,
                        Tax = o.Tax,
                        ProfilePictureId = o.ProfilePictureId,
                        PrimaryContactName = o.PrimaryContactName,
                        TaxNo = o.TaxNo,
                        ContractExpiryDate = o.ContractExpiryDate,
                        ClaimRevenuePer = o.ClaimRevenuePer,
                        Id = o.Id,
                        clientCount =o.clientCount,
                        ApplicationCount = o.ApplicationCount,
                    },
                    CountryName = o.CountryName,
                    OrganizationUnitDisplayName = o.OrganizationUnitDisplayName,
                    ImageBytes = o.ImageBytes,
                };
                res.Agent.ProfilePictureIdFileName = await GetBinaryFileName(o.ProfilePictureId);

                results.Add(res);
            }

            return new PagedResultDto<GetAgentForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetAgentForViewDto> GetAgentForView(long id)
        {
            var agent = await _agentRepository.GetAsync(id);

            var output = new GetAgentForViewDto { Agent = ObjectMapper.Map<AgentDto>(agent) };

            if (output.Agent.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.Agent.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }

            if (output.Agent.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.Agent.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            output.Agent.ProfilePictureIdFileName = await GetBinaryFileName(agent.ProfilePictureId);

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Agents_Edit)]
        public async Task<GetAgentForEditOutput> GetAgentForEdit(EntityDto<long> input)
        {
            var agent = await _agentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetAgentForEditOutput { Agent = ObjectMapper.Map<CreateOrEditAgentDto>(agent) };

            if (output.Agent.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.Agent.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }

            if (output.Agent.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.Agent.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            output.ProfilePictureIdFileName = await GetBinaryFileName(agent.ProfilePictureId);

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditAgentDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Agents_Create)]
        protected virtual async Task Create(CreateOrEditAgentDto input)
        {
            var agent = ObjectMapper.Map<Agent>(input);

            if (AbpSession.TenantId != null)
            {
                agent.TenantId = (int)AbpSession.TenantId;
            }

            await _agentRepository.InsertAsync(agent);
            //agent.ProfilePictureId = await GetBinaryObjectFromCache(input.ProfilePictureIdToken);

        }

        [AbpAuthorize(AppPermissions.Pages_Agents_Edit)]
        protected virtual async Task Update(CreateOrEditAgentDto input)
        {
            var agent = await _agentRepository.FirstOrDefaultAsync((long)input.Id);
            input.ProfilePictureId = agent.ProfilePictureId;
            ObjectMapper.Map(input, agent);
            //agent.ProfilePictureId = await GetBinaryObjectFromCache(input.ProfilePictureIdToken);

        }

        [AbpAuthorize(AppPermissions.Pages_Agents_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _agentRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetAgentsToExcel(GetAllAgentsForExcelInput input)
        {

            var filteredAgents = _agentRepository.GetAll()
                        .Include(e => e.CountryFk)
                        .Include(e => e.OrganizationUnitFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.PhoneCode.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.City.Contains(input.Filter) || e.Street.Contains(input.Filter) || e.State.Contains(input.Filter) || e.PrimaryContactName.Contains(input.Filter) || e.TaxNo.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(input.IsSuperAgentFilter.HasValue && input.IsSuperAgentFilter > -1, e => (input.IsSuperAgentFilter == 1 && e.IsSuperAgent) || (input.IsSuperAgentFilter == 0 && !e.IsSuperAgent))
                        .WhereIf(input.IsBusinessFilter.HasValue && input.IsBusinessFilter > -1, e => (input.IsBusinessFilter == 1 && e.IsBusiness) || (input.IsBusinessFilter == 0 && !e.IsBusiness))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo.Contains(input.PhoneNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.Contains(input.EmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CityFilter), e => e.City.Contains(input.CityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StreetFilter), e => e.Street.Contains(input.StreetFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StateFilter), e => e.State.Contains(input.StateFilter))
                        .WhereIf(input.MinIncomeSharingPerFilter != null, e => e.IncomeSharingPer >= input.MinIncomeSharingPerFilter)
                        .WhereIf(input.MaxIncomeSharingPerFilter != null, e => e.IncomeSharingPer <= input.MaxIncomeSharingPerFilter)
                        .WhereIf(input.MinTaxFilter != null, e => e.Tax >= input.MinTaxFilter)
                        .WhereIf(input.MaxTaxFilter != null, e => e.Tax <= input.MaxTaxFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PrimaryContactNameFilter), e => e.PrimaryContactName.Contains(input.PrimaryContactNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TaxNoFilter), e => e.TaxNo.Contains(input.TaxNoFilter))
                        .WhereIf(input.MinContractExpiryDateFilter != null, e => e.ContractExpiryDate >= input.MinContractExpiryDateFilter)
                        .WhereIf(input.MaxContractExpiryDateFilter != null, e => e.ContractExpiryDate <= input.MaxContractExpiryDateFilter)
                        .WhereIf(input.MinClaimRevenuePerFilter != null, e => e.ClaimRevenuePer >= input.MinClaimRevenuePerFilter)
                        .WhereIf(input.MaxClaimRevenuePerFilter != null, e => e.ClaimRevenuePer <= input.MaxClaimRevenuePerFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CountryNameFilter), e => e.CountryFk != null && e.CountryFk.Name == input.CountryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.OrganizationUnitFk != null && e.OrganizationUnitFk.DisplayName == input.OrganizationUnitDisplayNameFilter);

            var query = (from o in filteredAgents
                         join o1 in _lookup_countryRepository.GetAll() on o.CountryId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_organizationUnitRepository.GetAll() on o.OrganizationUnitId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetAgentForViewDto()
                         {
                             Agent = new AgentDto
                             {
                                 Name = o.Name,
                                 IsSuperAgent = o.IsSuperAgent,
                                 IsBusiness = o.IsBusiness,
                                 PhoneNo = o.PhoneNo,
                                 PhoneCode = o.PhoneCode,
                                 Email = o.Email,
                                 City = o.City,
                                 Street = o.Street,
                                 State = o.State,
                                 IncomeSharingPer = o.IncomeSharingPer,
                                 Tax = o.Tax,
                                 ProfilePictureId = o.ProfilePictureId,
                                 PrimaryContactName = o.PrimaryContactName,
                                 TaxNo = o.TaxNo,
                                 ContractExpiryDate = o.ContractExpiryDate,
                                 ClaimRevenuePer = o.ClaimRevenuePer,
                                 Id = o.Id
                             },
                             CountryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                             OrganizationUnitDisplayName = s2 == null || s2.DisplayName == null ? "" : s2.DisplayName.ToString()
                         });

            var agentListDtos = await query.ToListAsync();

            return _agentsExcelExporter.ExportToFile(agentListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Agents)]
        public async Task<List<AgentCountryLookupTableDto>> GetAllCountryForTableDropdown()
        {
            return await _lookup_countryRepository.GetAll()
                .Select(country => new AgentCountryLookupTableDto
                {
                    Id = country.Id,
                    DisplayName = country == null || country.Name == null ? "" : country.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Agents)]
        public async Task<List<AgentOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown()
        {
            return await _lookup_organizationUnitRepository.GetAll()
                .Select(organizationUnit => new AgentOrganizationUnitLookupTableDto
                {
                    Id = organizationUnit.Id,
                    DisplayName = organizationUnit == null || organizationUnit.DisplayName == null ? "" : organizationUnit.DisplayName.ToString()
                }).ToListAsync();
        }

        private async Task<Guid?> GetBinaryObjectFromCache(string fileToken)
        {
            if (fileToken.IsNullOrWhiteSpace())
            {
                return null;
            }

            var fileCache = _tempFileCacheManager.GetFileInfo(fileToken);

            if (fileCache == null)
            {
                throw new UserFriendlyException("There is no such file with the token: " + fileToken);
            }

            var storedFile = new BinaryObject(AbpSession.TenantId, fileCache.File, fileCache.FileName);
            await _binaryObjectManager.SaveAsync(storedFile);

            return storedFile.Id;
        }

        private async Task<string> GetBinaryFileName(Guid? fileId)
        {
            if (!fileId.HasValue)
            {
                return null;
            }

            var file = await _binaryObjectManager.GetOrNullAsync(fileId.Value);
            return file?.Description;
        }

        [AbpAuthorize(AppPermissions.Pages_Agents_Edit)]
        public async Task RemoveProfilePictureIdFile(EntityDto<long> input)
        {
            var agent = await _agentRepository.FirstOrDefaultAsync(input.Id);
            if (agent == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!agent.ProfilePictureId.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(agent.ProfilePictureId.Value);
            agent.ProfilePictureId = null;
        }

    }
}