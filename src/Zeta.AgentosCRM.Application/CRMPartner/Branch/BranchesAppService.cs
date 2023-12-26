using Zeta.AgentosCRM.CRMSetup.Countries; 
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage; 

namespace Zeta.AgentosCRM.CRMPartner.PartnerBranch
{
    [AbpAuthorize(AppPermissions.Pages_Branches)]
    public class BranchesAppService : AgentosCRMAppServiceBase, IBranchesAppService
    {
        private readonly IRepository<Branch, long> _branchRepository;
        private readonly IRepository<Country, int> _lookup_countryRepository;
        private readonly IRepository<Partner, long> _lookup_partnerRepository;

        public BranchesAppService(IRepository<Branch, long> branchRepository, IRepository<Country, int> lookup_countryRepository, IRepository<Partner, long> lookup_partnerRepository)
        {
            _branchRepository = branchRepository;
            _lookup_countryRepository = lookup_countryRepository;
            _lookup_partnerRepository = lookup_partnerRepository;
        }

        public async Task<PagedResultDto<GetBranchForViewDto>> GetAll(GetAllBranchesInput input)
        {

            var filteredBranches = _branchRepository.GetAll()
                        .Include(e => e.CountryFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.City.Contains(input.Filter) || e.State.Contains(input.Filter) || e.Street.Contains(input.Filter) || e.ZipCode.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.PhoneCode.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.Contains(input.EmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CityFilter), e => e.City.Contains(input.CityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StateFilter), e => e.State.Contains(input.StateFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StreetFilter), e => e.Street.Contains(input.StreetFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ZipCodeFilter), e => e.ZipCode.Contains(input.ZipCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo.Contains(input.PhoneNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneCodeFilter), e => e.PhoneCode.Contains(input.PhoneCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CountryNameFilter), e => e.CountryFk != null && e.CountryFk.Name == input.CountryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter)
                        .WhereIf(input.PartnerIdFilter.HasValue, e => false || e.PartnerId == input.PartnerIdFilter.Value);


            var pagedAndFilteredBranches = filteredBranches
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var branches = from o in pagedAndFilteredBranches
                           join o1 in _lookup_countryRepository.GetAll() on o.CountryId equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           join o2 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o2.Id into j2
                           from s2 in j2.DefaultIfEmpty()

                           select new
                           {

                               o.Name,
                               o.Email,
                               o.City,
                               o.State,
                               o.Street,
                               o.ZipCode,
                               o.PhoneNo,
                               o.PhoneCode,
                               Id = o.Id,
                               CountryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                               PartnerPartnerName = s2 == null || s2.PartnerName == null ? "" : s2.PartnerName.ToString()
                           };

            var totalCount = await filteredBranches.CountAsync();

            var dbList = await branches.ToListAsync();
            var results = new List<GetBranchForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetBranchForViewDto()
                {
                    Branch = new BranchDto
                    {

                        Name = o.Name,
                        Email = o.Email,
                        City = o.City,
                        State = o.State,
                        Street = o.Street,
                        ZipCode = o.ZipCode,
                        PhoneNo = o.PhoneNo,
                        PhoneCode = o.PhoneCode,
                        Id = o.Id,
                    },
                    CountryName = o.CountryName,
                    PartnerPartnerName = o.PartnerPartnerName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetBranchForViewDto>(
                totalCount,
                results
            );

        }


        public async Task<List<GetBranchForViewDto>> GetBranchbyWorkflowId(long workflowId)
        {
            var partner = _lookup_partnerRepository.GetAll().Where(t => workflowId == t.WorkflowId);
            var partnerIds = new List<long>();
             
            var pagedAndFilteredBranches = _branchRepository.GetAll();

            var branches = from o in pagedAndFilteredBranches

                           join o2 in partner on o.PartnerId equals o2.Id

                           select new
                           {

                               o.Name,
                               o.Email,
                               o.City,
                               o.State,
                               o.Street,
                               o.ZipCode,
                               o.PhoneNo,
                               o.PhoneCode,
                               o.PartnerId,
                               Id = o.Id, 
                               PartnerPartnerName = o2 == null || o2.PartnerName == null ? "" : o2.PartnerName.ToString()
                           };
              
            var dbList = await branches.ToListAsync();
            var results = new List<GetBranchForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetBranchForViewDto()
                {
                    Branch = new BranchDto
                    {
                        Name = o.Name, 
                        Id = o.Id, 
                        PartnerId= o.PartnerId,
                    },
                    PartnerPartnerName = o.PartnerPartnerName,
                };

                results.Add(res);
            }

            return results;
        }


        public async Task<GetBranchForViewDto> GetBranchForView(long id)
        {
            var branch = await _branchRepository.GetAsync(id);

            var output = new GetBranchForViewDto { Branch = ObjectMapper.Map<BranchDto>(branch) };

            if (output.Branch.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.Branch.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }
            if (output.Branch.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.Branch.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Branches_Edit)]
        public async Task<GetBranchForEditOutput> GetBranchForEdit(EntityDto<long> input)
        {
            var branch = await _branchRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetBranchForEditOutput { Branch = ObjectMapper.Map<CreateOrEditBranchDto>(branch) };

            if (output.Branch.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.Branch.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }
            if (output.Branch.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.Branch.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditBranchDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Branches_Create)]
        protected virtual async Task Create(CreateOrEditBranchDto input)
        {
            var branch = ObjectMapper.Map<Branch>(input);

            if (AbpSession.TenantId != null)
            {
                branch.TenantId = (int)AbpSession.TenantId;
            }

            await _branchRepository.InsertAsync(branch);

        }

        [AbpAuthorize(AppPermissions.Pages_Branches_Edit)]
        protected virtual async Task Update(CreateOrEditBranchDto input)
        {
            var branch = await _branchRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, branch);

        }

        [AbpAuthorize(AppPermissions.Pages_Branches_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _branchRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_Branches)]
        public async Task<List<BranchCountryLookupTableDto>> GetAllCountryForTableDropdown()
        {
            return await _lookup_countryRepository.GetAll()
                .Select(country => new BranchCountryLookupTableDto
                {
                    Id = country.Id,
                    DisplayName = country == null || country.Name == null ? "" : country.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Branches)]
        public async Task<List<BranchPartnerLookupTableDto>> GetAllPartnerForTableDropdown()
        {
            return await _lookup_partnerRepository.GetAll()
                .Select(partner => new BranchPartnerLookupTableDto
                {
                    Id = partner.Id,
                    DisplayName = partner == null || partner.PartnerName == null ? "" : partner.PartnerName.ToString()
                }).ToListAsync();
        }

    }
}