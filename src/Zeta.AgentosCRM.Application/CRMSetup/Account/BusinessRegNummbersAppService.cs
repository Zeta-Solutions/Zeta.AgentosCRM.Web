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
    [AbpAuthorize(AppPermissions.Pages_BusinessRegNummbers)]
    public class BusinessRegNummbersAppService : AgentosCRMAppServiceBase, IBusinessRegNummbersAppService
    {
        private readonly IRepository<BusinessRegNummber> _businessRegNummberRepository;
        private readonly IRepository<OrganizationUnit, long> _lookup_organizationUnitRepository;

        public BusinessRegNummbersAppService(IRepository<BusinessRegNummber> businessRegNummberRepository, IRepository<OrganizationUnit, long> lookup_organizationUnitRepository)
        {
            _businessRegNummberRepository = businessRegNummberRepository;
            _lookup_organizationUnitRepository = lookup_organizationUnitRepository;

        }

        public async Task<PagedResultDto<GetBusinessRegNummberForViewDto>> GetAll(GetAllBusinessRegNummbersInput input)
        {

            var filteredBusinessRegNummbers = _businessRegNummberRepository.GetAll()
                        .Include(e => e.OrganizationUnitFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.RegistrationNo.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RegistrationNoFilter), e => e.RegistrationNo.Contains(input.RegistrationNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.OrganizationUnitFk != null && e.OrganizationUnitFk.DisplayName == input.OrganizationUnitDisplayNameFilter)
                         .WhereIf(input.OrganizationUnitIdFilter.HasValue, e => false || e.OrganizationUnitId == input.OrganizationUnitIdFilter.Value);
            var pagedAndFilteredBusinessRegNummbers = filteredBusinessRegNummbers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var businessRegNummbers = from o in pagedAndFilteredBusinessRegNummbers
                                      join o1 in _lookup_organizationUnitRepository.GetAll() on o.OrganizationUnitId equals o1.Id into j1
                                      from s1 in j1.DefaultIfEmpty()

                                      select new
                                      {

                                          o.RegistrationNo,
                                          Id = o.Id,
                                          OrganizationUnitDisplayName = s1 == null || s1.DisplayName == null ? "" : s1.DisplayName.ToString()
                                      };

            var totalCount = await filteredBusinessRegNummbers.CountAsync();

            var dbList = await businessRegNummbers.ToListAsync();
            var results = new List<GetBusinessRegNummberForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetBusinessRegNummberForViewDto()
                {
                    BusinessRegNummber = new BusinessRegNummberDto
                    {

                        RegistrationNo = o.RegistrationNo,
                        Id = o.Id,
                    },
                    OrganizationUnitDisplayName = o.OrganizationUnitDisplayName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetBusinessRegNummberForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetBusinessRegNummberForViewDto> GetBusinessRegNummberForView(int id)
        {
            var businessRegNummber = await _businessRegNummberRepository.GetAsync(id);

            var output = new GetBusinessRegNummberForViewDto { BusinessRegNummber = ObjectMapper.Map<BusinessRegNummberDto>(businessRegNummber) };

            if (output.BusinessRegNummber.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.BusinessRegNummber.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_BusinessRegNummbers_Edit)]
        public async Task<GetBusinessRegNummberForEditOutput> GetBusinessRegNummberForEdit(EntityDto input)
        {
            var businessRegNummber = await _businessRegNummberRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetBusinessRegNummberForEditOutput { BusinessRegNummber = ObjectMapper.Map<CreateOrEditBusinessRegNummberDto>(businessRegNummber) };

            if (output.BusinessRegNummber.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.BusinessRegNummber.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditBusinessRegNummberDto input)
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

        [AbpAuthorize(AppPermissions.Pages_BusinessRegNummbers_Create)]
        protected virtual async Task Create(CreateOrEditBusinessRegNummberDto input)
        {
            var businessRegNummber = ObjectMapper.Map<BusinessRegNummber>(input);

            if (AbpSession.TenantId != null)
            {
                businessRegNummber.TenantId = (int)AbpSession.TenantId;
            }

            await _businessRegNummberRepository.InsertAsync(businessRegNummber);

        }

        [AbpAuthorize(AppPermissions.Pages_BusinessRegNummbers_Edit)]
        protected virtual async Task Update(CreateOrEditBusinessRegNummberDto input)
        {
            var businessRegNummber = await _businessRegNummberRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, businessRegNummber);

        }

        [AbpAuthorize(AppPermissions.Pages_BusinessRegNummbers_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _businessRegNummberRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_BusinessRegNummbers)]
        public async Task<List<BusinessRegNummberOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown()
        {
            return await _lookup_organizationUnitRepository.GetAll()
                .Select(organizationUnit => new BusinessRegNummberOrganizationUnitLookupTableDto
                {
                    Id = organizationUnit.Id,
                    DisplayName = organizationUnit == null || organizationUnit.DisplayName == null ? "" : organizationUnit.DisplayName.ToString()
                }).ToListAsync();
        }

    }
}