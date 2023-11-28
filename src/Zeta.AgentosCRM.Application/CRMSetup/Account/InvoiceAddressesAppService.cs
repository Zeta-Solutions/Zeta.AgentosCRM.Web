using Zeta.AgentosCRM.CRMSetup.Countries;
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
    [AbpAuthorize(AppPermissions.Pages_InvoiceAddresses)]
    public class InvoiceAddressesAppService : AgentosCRMAppServiceBase, IInvoiceAddressesAppService
    {
        private readonly IRepository<InvoiceAddress> _invoiceAddressRepository;
        private readonly IRepository<Country, int> _lookup_countryRepository;
        private readonly IRepository<OrganizationUnit, long> _lookup_organizationUnitRepository;

        public InvoiceAddressesAppService(IRepository<InvoiceAddress> invoiceAddressRepository, IRepository<Country, int> lookup_countryRepository, IRepository<OrganizationUnit, long> lookup_organizationUnitRepository)
        {
            _invoiceAddressRepository = invoiceAddressRepository;
            _lookup_countryRepository = lookup_countryRepository;
            _lookup_organizationUnitRepository = lookup_organizationUnitRepository;

        }

        public async Task<PagedResultDto<GetInvoiceAddressForViewDto>> GetAll(GetAllInvoiceAddressesInput input)
        {

            var filteredInvoiceAddresses = _invoiceAddressRepository.GetAll()
                        .Include(e => e.CountryFk)
                        .Include(e => e.OrganizationUnitFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Street.Contains(input.Filter) || e.City.Contains(input.Filter) || e.State.Contains(input.Filter) || e.ZipCode.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StreetFilter), e => e.Street.Contains(input.StreetFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CityFilter), e => e.City.Contains(input.CityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StateFilter), e => e.State.Contains(input.StateFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ZipCodeFilter), e => e.ZipCode.Contains(input.ZipCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CountryNameFilter), e => e.CountryFk != null && e.CountryFk.Name == input.CountryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.OrganizationUnitFk != null && e.OrganizationUnitFk.DisplayName == input.OrganizationUnitDisplayNameFilter)
                         .WhereIf(input.OrganizationUnitIdFilter.HasValue, e => false || e.OrganizationUnitId == input.OrganizationUnitIdFilter.Value);

            var pagedAndFilteredInvoiceAddresses = filteredInvoiceAddresses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var invoiceAddresses = from o in pagedAndFilteredInvoiceAddresses
                                   join o1 in _lookup_countryRepository.GetAll() on o.CountryId equals o1.Id into j1
                                   from s1 in j1.DefaultIfEmpty()

                                   join o2 in _lookup_organizationUnitRepository.GetAll() on o.OrganizationUnitId equals o2.Id into j2
                                   from s2 in j2.DefaultIfEmpty()

                                   select new
                                   {

                                       o.Street,
                                       o.City,
                                       o.State,
                                       o.ZipCode,
                                       Id = o.Id,
                                       o.CountryId,
                                       CountryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                       OrganizationUnitDisplayName = s2 == null || s2.DisplayName == null ? "" : s2.DisplayName.ToString()
                                   };

            var totalCount = await filteredInvoiceAddresses.CountAsync();

            var dbList = await invoiceAddresses.ToListAsync();
            var results = new List<GetInvoiceAddressForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetInvoiceAddressForViewDto()
                {
                    InvoiceAddress = new InvoiceAddressDto
                    {

                        Street = o.Street,
                        City = o.City,
                        State = o.State,
                        ZipCode = o.ZipCode,
                        Id = o.Id,
                        CountryId = o.CountryId,
                    },
                    CountryName = o.CountryName,
                    OrganizationUnitDisplayName = o.OrganizationUnitDisplayName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetInvoiceAddressForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetInvoiceAddressForViewDto> GetInvoiceAddressForView(int id)
        {
            var invoiceAddress = await _invoiceAddressRepository.GetAsync(id);

            var output = new GetInvoiceAddressForViewDto { InvoiceAddress = ObjectMapper.Map<InvoiceAddressDto>(invoiceAddress) };

            if (output.InvoiceAddress.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.InvoiceAddress.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }

            if (output.InvoiceAddress.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.InvoiceAddress.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_InvoiceAddresses_Edit)]
        public async Task<GetInvoiceAddressForEditOutput> GetInvoiceAddressForEdit(EntityDto input)
        {
            var invoiceAddress = await _invoiceAddressRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetInvoiceAddressForEditOutput { InvoiceAddress = ObjectMapper.Map<CreateOrEditInvoiceAddressDto>(invoiceAddress) };

            if (output.InvoiceAddress.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.InvoiceAddress.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }

            if (output.InvoiceAddress.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.InvoiceAddress.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditInvoiceAddressDto input)
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

        [AbpAuthorize(AppPermissions.Pages_InvoiceAddresses_Create)]
        protected virtual async Task Create(CreateOrEditInvoiceAddressDto input)
        {
            var invoiceAddress = ObjectMapper.Map<InvoiceAddress>(input);

            if (AbpSession.TenantId != null)
            {
                invoiceAddress.TenantId = (int)AbpSession.TenantId;
            }

            await _invoiceAddressRepository.InsertAsync(invoiceAddress);

        }

        [AbpAuthorize(AppPermissions.Pages_InvoiceAddresses_Edit)]
        protected virtual async Task Update(CreateOrEditInvoiceAddressDto input)
        {
            var invoiceAddress = await _invoiceAddressRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, invoiceAddress);

        }

        [AbpAuthorize(AppPermissions.Pages_InvoiceAddresses_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _invoiceAddressRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_InvoiceAddresses)]
        public async Task<List<InvoiceAddressCountryLookupTableDto>> GetAllCountryForTableDropdown()
        {
            return await _lookup_countryRepository.GetAll()
                .Select(country => new InvoiceAddressCountryLookupTableDto
                {
                    Id = country.Id,
                    DisplayName = country == null || country.Name == null ? "" : country.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_InvoiceAddresses)]
        public async Task<List<InvoiceAddressOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown()
        {
            return await _lookup_organizationUnitRepository.GetAll()
                .Select(organizationUnit => new InvoiceAddressOrganizationUnitLookupTableDto
                {
                    Id = organizationUnit.Id,
                    DisplayName = organizationUnit == null || organizationUnit.DisplayName == null ? "" : organizationUnit.DisplayName.ToString()
                }).ToListAsync();
        }

    }
}