using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMPartner.Contact.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;

namespace Zeta.AgentosCRM.CRMPartner.Contact
{
    [AbpAuthorize(AppPermissions.Pages_PartnerContacts)]
    public class PartnerContactsAppService : AgentosCRMAppServiceBase, IPartnerContactsAppService
    {
        private readonly IRepository<PartnerContact, long> _partnerContactRepository;
        private readonly IRepository<Branch, long> _lookup_branchRepository;
        private readonly IRepository<Partner, long> _lookup_partnerRepository;

        public PartnerContactsAppService(IRepository<PartnerContact, long> partnerContactRepository, IRepository<Branch, long> lookup_branchRepository, IRepository<Partner, long> lookup_partnerRepository)
        {
            _partnerContactRepository = partnerContactRepository;
            _lookup_branchRepository = lookup_branchRepository;
            _lookup_partnerRepository = lookup_partnerRepository;

        }

        public async Task<PagedResultDto<GetPartnerContactForViewDto>> GetAll(GetAllPartnerContactsInput input)
        {

            var filteredPartnerContacts = _partnerContactRepository.GetAll()
                        .Include(e => e.BranchFk)
                        .Include(e => e.PartnerFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.PhoneCode.Contains(input.Filter) || e.Fax.Contains(input.Filter) || e.Department.Contains(input.Filter) || e.Position.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.Contains(input.EmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo.Contains(input.PhoneNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneCodeFilter), e => e.PhoneCode.Contains(input.PhoneCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FaxFilter), e => e.Fax.Contains(input.FaxFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DepartmentFilter), e => e.Department.Contains(input.DepartmentFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PositionFilter), e => e.Position.Contains(input.PositionFilter))
                        .WhereIf(input.PrimaryContactFilter.HasValue && input.PrimaryContactFilter > -1, e => (input.PrimaryContactFilter == 1 && e.PrimaryContact) || (input.PrimaryContactFilter == 0 && !e.PrimaryContact))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BranchNameFilter), e => e.BranchFk != null && e.BranchFk.Name == input.BranchNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter);

            var pagedAndFilteredPartnerContacts = filteredPartnerContacts
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var partnerContacts = from o in pagedAndFilteredPartnerContacts
                                  join o1 in _lookup_branchRepository.GetAll() on o.BranchId equals o1.Id into j1
                                  from s1 in j1.DefaultIfEmpty()

                                  join o2 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o2.Id into j2
                                  from s2 in j2.DefaultIfEmpty()

                                  select new
                                  {

                                      o.Name,
                                      o.Email,
                                      o.PhoneNo,
                                      o.PhoneCode,
                                      o.Fax,
                                      o.Department,
                                      o.Position,
                                      o.PrimaryContact,
                                      Id = o.Id,
                                      BranchName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                      PartnerPartnerName = s2 == null || s2.PartnerName == null ? "" : s2.PartnerName.ToString()
                                  };

            var totalCount = await filteredPartnerContacts.CountAsync();

            var dbList = await partnerContacts.ToListAsync();
            var results = new List<GetPartnerContactForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPartnerContactForViewDto()
                {
                    PartnerContact = new PartnerContactDto
                    {

                        Name = o.Name,
                        Email = o.Email,
                        PhoneNo = o.PhoneNo,
                        PhoneCode = o.PhoneCode,
                        Fax = o.Fax,
                        Department = o.Department,
                        Position = o.Position,
                        PrimaryContact = o.PrimaryContact,
                        Id = o.Id,
                    },
                    BranchName = o.BranchName,
                    PartnerPartnerName = o.PartnerPartnerName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPartnerContactForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetPartnerContactForViewDto> GetPartnerContactForView(long id)
        {
            var partnerContact = await _partnerContactRepository.GetAsync(id);

            var output = new GetPartnerContactForViewDto { PartnerContact = ObjectMapper.Map<PartnerContactDto>(partnerContact) };

            if (output.PartnerContact.BranchId != null)
            {
                var _lookupBranch = await _lookup_branchRepository.FirstOrDefaultAsync((long)output.PartnerContact.BranchId);
                output.BranchName = _lookupBranch?.Name?.ToString();
            }

            if (output.PartnerContact.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.PartnerContact.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_PartnerContacts_Edit)]
        public async Task<GetPartnerContactForEditOutput> GetPartnerContactForEdit(EntityDto<long> input)
        {
            var partnerContact = await _partnerContactRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPartnerContactForEditOutput { PartnerContact = ObjectMapper.Map<CreateOrEditPartnerContactDto>(partnerContact) };

            if (output.PartnerContact.BranchId != null)
            {
                var _lookupBranch = await _lookup_branchRepository.FirstOrDefaultAsync((long)output.PartnerContact.BranchId);
                output.BranchName = _lookupBranch?.Name?.ToString();
            }

            if (output.PartnerContact.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.PartnerContact.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPartnerContactDto input)
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

        [AbpAuthorize(AppPermissions.Pages_PartnerContacts_Create)]
        protected virtual async Task Create(CreateOrEditPartnerContactDto input)
        {
            var partnerContact = ObjectMapper.Map<PartnerContact>(input);

            if (AbpSession.TenantId != null)
            {
                partnerContact.TenantId = (int)AbpSession.TenantId;
            }

            await _partnerContactRepository.InsertAsync(partnerContact);

        }

        [AbpAuthorize(AppPermissions.Pages_PartnerContacts_Edit)]
        protected virtual async Task Update(CreateOrEditPartnerContactDto input)
        {
            var partnerContact = await _partnerContactRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, partnerContact);

        }

        [AbpAuthorize(AppPermissions.Pages_PartnerContacts_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _partnerContactRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_PartnerContacts)]
        public async Task<List<PartnerContactBranchLookupTableDto>> GetAllBranchForTableDropdown()
        {
            return await _lookup_branchRepository.GetAll()
                .Select(branch => new PartnerContactBranchLookupTableDto
                {
                    Id = branch.Id,
                    DisplayName = branch == null || branch.Name == null ? "" : branch.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_PartnerContacts)]
        public async Task<List<PartnerContactPartnerLookupTableDto>> GetAllPartnerForTableDropdown()
        {
            return await _lookup_partnerRepository.GetAll()
                .Select(partner => new PartnerContactPartnerLookupTableDto
                {
                    Id = partner.Id,
                    DisplayName = partner == null || partner.PartnerName == null ? "" : partner.PartnerName.ToString()
                }).ToListAsync();
        }

    }
}