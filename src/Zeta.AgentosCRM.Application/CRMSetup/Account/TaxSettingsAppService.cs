using Abp.Organizations;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.Account.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup.Account
{
    [AbpAuthorize(AppPermissions.Pages_TaxSettings)]
    public class TaxSettingsAppService : AgentosCRMAppServiceBase, ITaxSettingsAppService
    {
        private readonly IRepository<TaxSetting> _taxSettingRepository;
        private readonly IRepository<OrganizationUnit, long> _lookup_organizationUnitRepository;

        public TaxSettingsAppService(IRepository<TaxSetting> taxSettingRepository, IRepository<OrganizationUnit, long> lookup_organizationUnitRepository)
        {
            _taxSettingRepository = taxSettingRepository;
            _lookup_organizationUnitRepository = lookup_organizationUnitRepository;

        }

        public async Task<PagedResultDto<GetTaxSettingForViewDto>> GetAll(GetAllTaxSettingsInput input)
        {

            var filteredTaxSettings = _taxSettingRepository.GetAll()
                        .Include(e => e.OrganizationUnitFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TaxCode.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TaxCodeFilter), e => e.TaxCode.Contains(input.TaxCodeFilter))
                        .WhereIf(input.MinTaxRateFilter != null, e => e.TaxRate >= input.MinTaxRateFilter)
                        .WhereIf(input.MaxTaxRateFilter != null, e => e.TaxRate <= input.MaxTaxRateFilter)
                        .WhereIf(input.IsDefaultFilter.HasValue && input.IsDefaultFilter > -1, e => (input.IsDefaultFilter == 1 && e.IsDefault) || (input.IsDefaultFilter == 0 && !e.IsDefault))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.OrganizationUnitFk != null && e.OrganizationUnitFk.DisplayName == input.OrganizationUnitDisplayNameFilter)
                         .WhereIf(input.OrganizationUnitIdFilter.HasValue, e => false || e.OrganizationUnitId == input.OrganizationUnitIdFilter.Value);


            var pagedAndFilteredTaxSettings = filteredTaxSettings
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var taxSettings = from o in pagedAndFilteredTaxSettings
                              join o1 in _lookup_organizationUnitRepository.GetAll() on o.OrganizationUnitId equals o1.Id into j1
                              from s1 in j1.DefaultIfEmpty()

                              select new
                              {

                                  o.TaxCode,
                                  o.TaxRate,
                                  o.IsDefault,
                                  Id = o.Id,
                                  OrganizationUnitDisplayName = s1 == null || s1.DisplayName == null ? "" : s1.DisplayName.ToString()
                              };

            var totalCount = await filteredTaxSettings.CountAsync();

            var dbList = await taxSettings.ToListAsync();
            var results = new List<GetTaxSettingForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTaxSettingForViewDto()
                {
                    TaxSetting = new TaxSettingDto
                    {

                        TaxCode = o.TaxCode,
                        TaxRate = o.TaxRate,
                        IsDefault = o.IsDefault,
                        Id = o.Id,
                    },
                    OrganizationUnitDisplayName = o.OrganizationUnitDisplayName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetTaxSettingForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetTaxSettingForViewDto> GetTaxSettingForView(int id)
        {
            var taxSetting = await _taxSettingRepository.GetAsync(id);

            var output = new GetTaxSettingForViewDto { TaxSetting = ObjectMapper.Map<TaxSettingDto>(taxSetting) };

            if (output.TaxSetting.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.TaxSetting.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_TaxSettings_Edit)]
        public async Task<GetTaxSettingForEditOutput> GetTaxSettingForEdit(EntityDto input)
        {
            var taxSetting = await _taxSettingRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTaxSettingForEditOutput { TaxSetting = ObjectMapper.Map<CreateOrEditTaxSettingDto>(taxSetting) };

            if (output.TaxSetting.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.TaxSetting.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditTaxSettingDto input)
        {
            if (input.Id == null || input.Id==0)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_TaxSettings_Create)]
        protected virtual async Task Create(CreateOrEditTaxSettingDto input)
        {
            var taxSetting = ObjectMapper.Map<TaxSetting>(input);

            if (AbpSession.TenantId != null)
            {
                taxSetting.TenantId = (int)AbpSession.TenantId;
            }

            await _taxSettingRepository.InsertAsync(taxSetting);

        }

        [AbpAuthorize(AppPermissions.Pages_TaxSettings_Edit)]
        protected virtual async Task Update(CreateOrEditTaxSettingDto input)
        {
            var taxSetting = await _taxSettingRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, taxSetting);

        }

        [AbpAuthorize(AppPermissions.Pages_TaxSettings_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _taxSettingRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_TaxSettings)]
        public async Task<List<TaxSettingOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown()
        {
            return await _lookup_organizationUnitRepository.GetAll()
                .Select(organizationUnit => new TaxSettingOrganizationUnitLookupTableDto
                {
                    Id = organizationUnit.Id,
                    DisplayName = organizationUnit == null || organizationUnit.DisplayName == null ? "" : organizationUnit.DisplayName.ToString()
                }).ToListAsync();
        }

    }
}