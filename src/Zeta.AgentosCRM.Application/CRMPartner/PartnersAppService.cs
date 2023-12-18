﻿using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.CRMSetup.CRMCurrency;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMPartner.Exporting;
using Zeta.AgentosCRM.CRMPartner.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMPartner
{
    [AbpAuthorize(AppPermissions.Pages_Partners)]
    public class PartnersAppService : AgentosCRMAppServiceBase, IPartnersAppService
    {
        private readonly IRepository<Partner, long> _partnerRepository;
        private readonly IPartnersExcelExporter _partnersExcelExporter;
        private readonly IRepository<BinaryObject, Guid> _lookup_binaryObjectRepository;
        private readonly IRepository<MasterCategory, int> _lookup_masterCategoryRepository;
        private readonly IRepository<PartnerType, int> _lookup_partnerTypeRepository;
        private readonly IRepository<Workflow, int> _lookup_workflowRepository;
        private readonly IRepository<Country, int> _lookup_countryRepository;
        private readonly IRepository<CRMCurrency, int> _lookup_crmCurrencyRepository;

        public PartnersAppService(IRepository<Partner, long> partnerRepository, IPartnersExcelExporter partnersExcelExporter, IRepository<BinaryObject, Guid> lookup_binaryObjectRepository, IRepository<MasterCategory, int> lookup_masterCategoryRepository, IRepository<PartnerType, int> lookup_partnerTypeRepository, IRepository<Workflow, int> lookup_workflowRepository, IRepository<Country, int> lookup_countryRepository, IRepository<CRMCurrency, int> lookup_crmCurrencyRepository)
        {
            _partnerRepository = partnerRepository;
            _partnersExcelExporter = partnersExcelExporter;
            _lookup_binaryObjectRepository = lookup_binaryObjectRepository;
            _lookup_masterCategoryRepository = lookup_masterCategoryRepository;
            _lookup_partnerTypeRepository = lookup_partnerTypeRepository;
            _lookup_workflowRepository = lookup_workflowRepository;
            _lookup_countryRepository = lookup_countryRepository;
            _lookup_crmCurrencyRepository = lookup_crmCurrencyRepository;

        }

        public async Task<PagedResultDto<GetPartnerForViewDto>> GetAll(GetAllPartnersInput input)
        {

            var filteredPartners = _partnerRepository.GetAll()
                        .Include(e => e.ProfilePictureFk)
                        .Include(e => e.MasterCategoryFk)
                        .Include(e => e.PartnerTypeFk)
                        .Include(e => e.WorkflowFk)
                        .Include(e => e.CountryFk)
                        .Include(e => e.CurrencyFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PartnerName.Contains(input.Filter) || e.Street.Contains(input.Filter) || e.City.Contains(input.Filter) || e.State.Contains(input.Filter) || e.ZipCode.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.Fax.Contains(input.Filter) || e.Website.Contains(input.Filter) || e.University.Contains(input.Filter) || e.MarketingEmail.Contains(input.Filter) || e.BusinessRegNo.Contains(input.Filter) || e.PhoneCode.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerNameFilter), e => e.PartnerName.Contains(input.PartnerNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StreetFilter), e => e.Street.Contains(input.StreetFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CityFilter), e => e.City.Contains(input.CityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StateFilter), e => e.State.Contains(input.StateFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ZipCodeFilter), e => e.ZipCode.Contains(input.ZipCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo.Contains(input.PhoneNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.Contains(input.EmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FaxFilter), e => e.Fax.Contains(input.FaxFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WebsiteFilter), e => e.Website.Contains(input.WebsiteFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityFilter), e => e.University.Contains(input.UniversityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MarketingEmailFilter), e => e.MarketingEmail.Contains(input.MarketingEmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BusinessRegNoFilter), e => e.BusinessRegNo.Contains(input.BusinessRegNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneCodeFilter), e => e.PhoneCode.Contains(input.PhoneCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BinaryObjectDescriptionFilter), e => e.ProfilePictureFk != null && e.ProfilePictureFk.Description == input.BinaryObjectDescriptionFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MasterCategoryNameFilter), e => e.MasterCategoryFk != null && e.MasterCategoryFk.Name == input.MasterCategoryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerTypeNameFilter), e => e.PartnerTypeFk != null && e.PartnerTypeFk.Name == input.PartnerTypeNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkflowNameFilter), e => e.WorkflowFk != null && e.WorkflowFk.Name == input.WorkflowNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CountryNameFilter), e => e.CountryFk != null && e.CountryFk.Name == input.CountryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CRMCurrencyNameFilter), e => e.CurrencyFk != null && e.CurrencyFk.Name == input.CRMCurrencyNameFilter);

            var pagedAndFilteredPartners = filteredPartners
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var partners = from o in pagedAndFilteredPartners
                           join o1 in _lookup_binaryObjectRepository.GetAll() on o.ProfilePictureId equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           join o2 in _lookup_masterCategoryRepository.GetAll() on o.MasterCategoryId equals o2.Id into j2
                           from s2 in j2.DefaultIfEmpty()

                           join o3 in _lookup_partnerTypeRepository.GetAll() on o.PartnerTypeId equals o3.Id into j3
                           from s3 in j3.DefaultIfEmpty()

                           join o4 in _lookup_workflowRepository.GetAll() on o.WorkflowId equals o4.Id into j4
                           from s4 in j4.DefaultIfEmpty()

                           join o5 in _lookup_countryRepository.GetAll() on o.CountryId equals o5.Id into j5
                           from s5 in j5.DefaultIfEmpty()

                           join o6 in _lookup_crmCurrencyRepository.GetAll() on o.CurrencyId equals o6.Id into j6
                           from s6 in j6.DefaultIfEmpty()

                           select new
                           {

                               o.PartnerName,
                               o.Street,
                               o.City,
                               o.State,
                               o.ZipCode,
                               o.PhoneNo,
                               o.Email,
                               o.Fax,
                               o.Website,
                               o.University,
                               o.MarketingEmail,
                               o.BusinessRegNo,
                               o.PhoneCode,
                               o.ProfilePictureId,
                               Id = o.Id,
                               BinaryObjectDescription = s1 == null || s1.Description == null ? "" : s1.Description.ToString(),
                               ImageBytes = s3 == null || s1.Bytes == null ? "" : Convert.ToBase64String(s1.Bytes),
                               MasterCategoryName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                               PartnerTypeName = s3 == null || s3.Name == null ? "" : s3.Name.ToString(),
                               WorkflowName = s4 == null || s4.Name == null ? "" : s4.Name.ToString(),
                               CountryName = s5 == null || s5.Name == null ? "" : s5.Name.ToString(),
                               CRMCurrencyName = s6 == null || s6.Name == null ? "" : s6.Name.ToString()
                           };

            var totalCount = await filteredPartners.CountAsync();

            var dbList = await partners.ToListAsync();
            var results = new List<GetPartnerForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPartnerForViewDto()
                {
                    Partner = new PartnerDto
                    {

                        PartnerName = o.PartnerName,
                        Street = o.Street,
                        City = o.City,
                        State = o.State,
                        ZipCode = o.ZipCode,
                        PhoneNo = o.PhoneNo,
                        Email = o.Email,
                        Fax = o.Fax,
                        Website = o.Website,
                        University = o.University,
                        MarketingEmail = o.MarketingEmail,
                        BusinessRegNo = o.BusinessRegNo,
                        PhoneCode = o.PhoneCode,
                        Id = o.Id,
                    },
                    BinaryObjectDescription = o.BinaryObjectDescription,
                    MasterCategoryName = o.MasterCategoryName,
                    PartnerTypeName = o.PartnerTypeName,
                    WorkflowName = o.WorkflowName,
                    CountryName = o.CountryName,
                    CRMCurrencyName = o.CRMCurrencyName,
                    ImageBytes = o.ImageBytes,
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPartnerForViewDto>(
                totalCount,
                results
            );

        }


        public async Task<GetPartnerForViewDto> GetPartnerForView(long id)
        {
            var Partner = await _partnerRepository.GetAsync(id);

            var output = new GetPartnerForViewDto { Partner = ObjectMapper.Map<PartnerDto>(Partner) };

            if (output.Partner.ProfilePictureId != null)
            {
                var _lookupBinaryObject = await _lookup_binaryObjectRepository.FirstOrDefaultAsync((Guid)output.Partner.ProfilePictureId);
                output.BinaryObjectDescription = _lookupBinaryObject?.Description?.ToString();
            }

            if (output.Partner.MasterCategoryId != null)
            {
                var _lookupMasterCategory = await _lookup_masterCategoryRepository.FirstOrDefaultAsync((int)output.Partner.MasterCategoryId);
                output.MasterCategoryName = _lookupMasterCategory?.Name?.ToString();
            }

            if (output.Partner.PartnerTypeId != null)
            {
                var _lookupPartnerType = await _lookup_partnerTypeRepository.FirstOrDefaultAsync((int)output.Partner.PartnerTypeId);
                output.PartnerTypeName = _lookupPartnerType?.Name?.ToString();
            }

            if (output.Partner.WorkflowId != null)
            {
                var _lookupWorkflow = await _lookup_workflowRepository.FirstOrDefaultAsync((int)output.Partner.WorkflowId);
                output.WorkflowName = _lookupWorkflow?.Name?.ToString();
            }

            if (output.Partner.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.Partner.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }

            if (output.Partner.CurrencyId != null)
            {
                var _lookupCRMCurrency = await _lookup_crmCurrencyRepository.FirstOrDefaultAsync((int)output.Partner.CurrencyId);
                output.CRMCurrencyName = _lookupCRMCurrency?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Partners_Edit)]
        public async Task<GetPartnerForEditOutput> GetPartnerForEdit(EntityDto<long> input)
        {
            var partner = await _partnerRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPartnerForEditOutput { Partner = ObjectMapper.Map<CreateOrEditPartnerDto>(partner) };

            if (output.Partner.ProfilePictureId != null)
            {
                var _lookupBinaryObject = await _lookup_binaryObjectRepository.FirstOrDefaultAsync((Guid)output.Partner.ProfilePictureId);
                output.BinaryObjectDescription = _lookupBinaryObject?.Description?.ToString();
            }

            if (output.Partner.MasterCategoryId != null)
            {
                var _lookupMasterCategory = await _lookup_masterCategoryRepository.FirstOrDefaultAsync((int)output.Partner.MasterCategoryId);
                output.MasterCategoryName = _lookupMasterCategory?.Name?.ToString();
            }

            if (output.Partner.PartnerTypeId != null)
            {
                var _lookupPartnerType = await _lookup_partnerTypeRepository.FirstOrDefaultAsync((int)output.Partner.PartnerTypeId);
                output.PartnerTypeName = _lookupPartnerType?.Name?.ToString();
            }

            if (output.Partner.WorkflowId != null)
            {
                var _lookupWorkflow = await _lookup_workflowRepository.FirstOrDefaultAsync((int)output.Partner.WorkflowId);
                output.WorkflowName = _lookupWorkflow?.Name?.ToString();
            }

            if (output.Partner.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.Partner.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }

            if (output.Partner.CurrencyId != null)
            {
                var _lookupCRMCurrency = await _lookup_crmCurrencyRepository.FirstOrDefaultAsync((int)output.Partner.CurrencyId);
                output.CRMCurrencyName = _lookupCRMCurrency?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPartnerDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Partners_Create)]
        protected virtual async Task Create(CreateOrEditPartnerDto input)
        {
            var partner = ObjectMapper.Map<Partner>(input);

            if (AbpSession.TenantId != null)
            {
                partner.TenantId = (int)AbpSession.TenantId;
            }

            await _partnerRepository.InsertAsync(partner);

        }

        [AbpAuthorize(AppPermissions.Pages_Partners_Edit)]
        protected virtual async Task Update(CreateOrEditPartnerDto input)
        {
            var partner = await _partnerRepository.FirstOrDefaultAsync((long)input.Id);
            input.ProfilePictureId = partner.ProfilePictureId;
            ObjectMapper.Map(input, partner);

        }

        [AbpAuthorize(AppPermissions.Pages_Partners_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _partnerRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetPartnersToExcel(GetAllPartnersForExcelInput input)
        {

            var filteredPartners = _partnerRepository.GetAll()
                        .Include(e => e.ProfilePictureFk)
                        .Include(e => e.MasterCategoryFk)
                        .Include(e => e.PartnerTypeFk)
                        .Include(e => e.WorkflowFk)
                        .Include(e => e.CountryFk)
                        .Include(e => e.CurrencyFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PartnerName.Contains(input.Filter) || e.Street.Contains(input.Filter) || e.City.Contains(input.Filter) || e.State.Contains(input.Filter) || e.ZipCode.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.Fax.Contains(input.Filter) || e.Website.Contains(input.Filter) || e.University.Contains(input.Filter) || e.MarketingEmail.Contains(input.Filter) || e.BusinessRegNo.Contains(input.Filter) || e.PhoneCode.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerNameFilter), e => e.PartnerName.Contains(input.PartnerNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StreetFilter), e => e.Street.Contains(input.StreetFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CityFilter), e => e.City.Contains(input.CityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StateFilter), e => e.State.Contains(input.StateFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ZipCodeFilter), e => e.ZipCode.Contains(input.ZipCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo.Contains(input.PhoneNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.Contains(input.EmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FaxFilter), e => e.Fax.Contains(input.FaxFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WebsiteFilter), e => e.Website.Contains(input.WebsiteFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityFilter), e => e.University.Contains(input.UniversityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MarketingEmailFilter), e => e.MarketingEmail.Contains(input.MarketingEmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BusinessRegNoFilter), e => e.BusinessRegNo.Contains(input.BusinessRegNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneCodeFilter), e => e.PhoneCode.Contains(input.PhoneCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BinaryObjectDescriptionFilter), e => e.ProfilePictureFk != null && e.ProfilePictureFk.Description == input.BinaryObjectDescriptionFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MasterCategoryNameFilter), e => e.MasterCategoryFk != null && e.MasterCategoryFk.Name == input.MasterCategoryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerTypeNameFilter), e => e.PartnerTypeFk != null && e.PartnerTypeFk.Name == input.PartnerTypeNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkflowNameFilter), e => e.WorkflowFk != null && e.WorkflowFk.Name == input.WorkflowNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CountryNameFilter), e => e.CountryFk != null && e.CountryFk.Name == input.CountryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CRMCurrencyNameFilter), e => e.CurrencyFk != null && e.CurrencyFk.Name == input.CRMCurrencyNameFilter);

            var query = (from o in filteredPartners
                         join o1 in _lookup_binaryObjectRepository.GetAll() on o.ProfilePictureId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_masterCategoryRepository.GetAll() on o.MasterCategoryId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _lookup_partnerTypeRepository.GetAll() on o.PartnerTypeId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         join o4 in _lookup_workflowRepository.GetAll() on o.WorkflowId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()

                         join o5 in _lookup_countryRepository.GetAll() on o.CountryId equals o5.Id into j5
                         from s5 in j5.DefaultIfEmpty()

                         join o6 in _lookup_crmCurrencyRepository.GetAll() on o.CurrencyId equals o6.Id into j6
                         from s6 in j6.DefaultIfEmpty()

                         select new GetPartnerForViewDto()
                         {
                             Partner = new PartnerDto
                             {
                                 PartnerName = o.PartnerName,
                                 Street = o.Street,
                                 City = o.City,
                                 State = o.State,
                                 ZipCode = o.ZipCode,
                                 PhoneNo = o.PhoneNo,
                                 Email = o.Email,
                                 Fax = o.Fax,
                                 Website = o.Website,
                                 University = o.University,
                                 MarketingEmail = o.MarketingEmail,
                                 BusinessRegNo = o.BusinessRegNo,
                                 PhoneCode = o.PhoneCode,
                                 Id = o.Id
                             },
                             BinaryObjectDescription = s1 == null || s1.Description == null ? "" : s1.Description.ToString(),
                             MasterCategoryName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                             PartnerTypeName = s3 == null || s3.Name == null ? "" : s3.Name.ToString(),
                             WorkflowName = s4 == null || s4.Name == null ? "" : s4.Name.ToString(),
                             CountryName = s5 == null || s5.Name == null ? "" : s5.Name.ToString(),
                             CRMCurrencyName = s6 == null || s6.Name == null ? "" : s6.Name.ToString()
                         });

            var partnerListDtos = await query.ToListAsync();

            return _partnersExcelExporter.ExportToFile(partnerListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Partners)]
        public async Task<List<PartnerBinaryObjectLookupTableDto>> GetAllBinaryObjectForTableDropdown()
        {
            return await _lookup_binaryObjectRepository.GetAll()
                .Select(binaryObject => new PartnerBinaryObjectLookupTableDto
                {
                    Id = binaryObject.Id.ToString(),
                    DisplayName = binaryObject == null || binaryObject.Description == null ? "" : binaryObject.Description.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Partners)]
        public async Task<List<PartnerMasterCategoryLookupTableDto>> GetAllMasterCategoryForTableDropdown()
        {
            return await _lookup_masterCategoryRepository.GetAll()
                .Select(masterCategory => new PartnerMasterCategoryLookupTableDto
                {
                    Id = masterCategory.Id,
                    DisplayName = masterCategory == null || masterCategory.Name == null ? "" : masterCategory.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Partners)]
        public async Task<List<PartnerPartnerTypeLookupTableDto>> GetAllPartnerTypeForTableDropdown()
        {
            return await _lookup_partnerTypeRepository.GetAll()
                .Select(partnerType => new PartnerPartnerTypeLookupTableDto
                {
                    Id = partnerType.Id,
                    DisplayName = partnerType == null || partnerType.Name == null ? "" : partnerType.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Partners)]
        public async Task<List<PartnerWorkflowLookupTableDto>> GetAllWorkflowForTableDropdown()
        {
            return await _lookup_workflowRepository.GetAll()
                .Select(workflow => new PartnerWorkflowLookupTableDto
                {
                    Id = workflow.Id,
                    DisplayName = workflow == null || workflow.Name == null ? "" : workflow.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Partners)]
        public async Task<List<PartnerCountryLookupTableDto>> GetAllCountryForTableDropdown()
        {
            return await _lookup_countryRepository.GetAll()
                .Select(country => new PartnerCountryLookupTableDto
                {
                    Id = country.Id,
                    DisplayName = country == null || country.Name == null ? "" : country.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Partners)]
        public async Task<List<PartnerCRMCurrencyLookupTableDto>> GetAllCRMCurrencyForTableDropdown()
        {
            return await _lookup_crmCurrencyRepository.GetAll()
                .Select(crmCurrency => new PartnerCRMCurrencyLookupTableDto
                {
                    Id = crmCurrency.Id,
                    DisplayName = crmCurrency == null || crmCurrency.Name == null ? "" : crmCurrency.Name.ToString()
                }).ToListAsync();
        }

    }
}