using Zeta.AgentosCRM.CRMSetup.Regions;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.Countries.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup.Countries
{
    [AbpAuthorize(AppPermissions.Pages_Countries)]
    public class CountriesAppService : AgentosCRMAppServiceBase, ICountriesAppService
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Region, int> _lookup_regionRepository;

        public CountriesAppService(IRepository<Country> countryRepository, IRepository<Region, int> lookup_regionRepository)
        {
            _countryRepository = countryRepository;
            _lookup_regionRepository = lookup_regionRepository;

        }

        public async Task<PagedResultDto<GetCountryForViewDto>> GetAll(GetAllCountriesInput input)
        {

            var filteredCountries = _countryRepository.GetAll()
                        .Include(e => e.RegionFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter) || e.Icon.Contains(input.Filter) || e.Code.Contains(input.Filter)||e.RegionFk.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IconFilter), e => e.Icon.Contains(input.IconFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter), e => e.Code.Contains(input.CodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RegionNameFilter), e => e.RegionFk != null && e.RegionFk.Name == input.RegionNameFilter);

            var pagedAndFilteredCountries = filteredCountries
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var countries = from o in pagedAndFilteredCountries
                            join o1 in _lookup_regionRepository.GetAll() on o.RegionId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()

                            select new
                            {

                                o.Abbrivation,
                                o.Name,
                                o.Icon,
                                o.Code,
                                Id = o.Id,
                                RegionName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                            };

            var totalCount = await filteredCountries.CountAsync();

            var dbList = await countries.ToListAsync();
            var results = new List<GetCountryForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCountryForViewDto()
                {
                    Country = new CountryDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Icon = o.Icon,
                        Code = o.Code,
                        Id = o.Id,
                    },
                    RegionName = o.RegionName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCountryForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetCountryForViewDto> GetCountryForView(int id)
        {
            var country = await _countryRepository.GetAsync(id);

            var output = new GetCountryForViewDto { Country = ObjectMapper.Map<CountryDto>(country) };

            if (output.Country.RegionId != null)
            {
                var _lookupRegion = await _lookup_regionRepository.FirstOrDefaultAsync((int)output.Country.RegionId);
                output.RegionName = _lookupRegion?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Countries_Edit)]
        public async Task<GetCountryForEditOutput> GetCountryForEdit(EntityDto input)
        {
            var country = await _countryRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCountryForEditOutput { Country = ObjectMapper.Map<CreateOrEditCountryDto>(country) };

            if (output.Country.RegionId != null)
            {
                var _lookupRegion = await _lookup_regionRepository.FirstOrDefaultAsync((int)output.Country.RegionId);
                output.RegionName = _lookupRegion?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCountryDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Countries_Create)]
        protected virtual async Task Create(CreateOrEditCountryDto input)
        {
            var country = ObjectMapper.Map<Country>(input);

            if (AbpSession.TenantId != null)
            {
                country.TenantId = (int)AbpSession.TenantId;
            }

            await _countryRepository.InsertAsync(country);

        }

        [AbpAuthorize(AppPermissions.Pages_Countries_Edit)]
        protected virtual async Task Update(CreateOrEditCountryDto input)
        {
            var country = await _countryRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, country);

        }

        [AbpAuthorize(AppPermissions.Pages_Countries_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _countryRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_Countries)]
        public async Task<List<CountryRegionLookupTableDto>> GetAllRegionForTableDropdown()
        {
            return await _lookup_regionRepository.GetAll()
                .Select(region => new CountryRegionLookupTableDto
                {
                    Id = region.Id,
                    DisplayName = region == null || region.Name == null ? "" : region.Name.ToString()
                }).ToListAsync();
        }

    }
}