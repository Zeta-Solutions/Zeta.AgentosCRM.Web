using Zeta.AgentosCRM.CRMSetup;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.Exporting;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup
{
    [AbpAuthorize(AppPermissions.Pages_CRMSetup_PartnerTypes)]
    public class PartnerTypesAppService : AgentosCRMAppServiceBase, IPartnerTypesAppService
    {
        private readonly IRepository<PartnerType> _partnerTypeRepository;
        private readonly IPartnerTypesExcelExporter _partnerTypesExcelExporter;
        private readonly IRepository<MasterCategory, int> _lookup_masterCategoryRepository;

        public PartnerTypesAppService(IRepository<PartnerType> partnerTypeRepository, IPartnerTypesExcelExporter partnerTypesExcelExporter, IRepository<MasterCategory, int> lookup_masterCategoryRepository)
        {
            _partnerTypeRepository = partnerTypeRepository;
            _partnerTypesExcelExporter = partnerTypesExcelExporter;
            _lookup_masterCategoryRepository = lookup_masterCategoryRepository;

        }

        public async Task<PagedResultDto<GetPartnerTypeForViewDto>> GetAll(GetAllPartnerTypesInput input)
        {

            var filteredPartnerTypes = _partnerTypeRepository.GetAll()
                        .Include(e => e.MasterCategoryFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MasterCategoryNameFilter), e => e.MasterCategoryFk != null && e.MasterCategoryFk.Name == input.MasterCategoryNameFilter);

            var pagedAndFilteredPartnerTypes = filteredPartnerTypes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var partnerTypes = from o in pagedAndFilteredPartnerTypes
                               join o1 in _lookup_masterCategoryRepository.GetAll() on o.MasterCategoryId equals o1.Id into j1
                               from s1 in j1.DefaultIfEmpty()

                               select new
                               {

                                   o.Abbrivation,
                                   o.Name,
                                   Id = o.Id,
                                   MasterCategoryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                               };

            var totalCount = await filteredPartnerTypes.CountAsync();

            var dbList = await partnerTypes.ToListAsync();
            var results = new List<GetPartnerTypeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPartnerTypeForViewDto()
                {
                    PartnerType = new PartnerTypeDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                    },
                    MasterCategoryName = o.MasterCategoryName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPartnerTypeForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetPartnerTypeForViewDto> GetPartnerTypeForView(int id)
        {
            var partnerType = await _partnerTypeRepository.GetAsync(id);

            var output = new GetPartnerTypeForViewDto { PartnerType = ObjectMapper.Map<PartnerTypeDto>(partnerType) };

            if (output.PartnerType.MasterCategoryId != null)
            {
                var _lookupMasterCategory = await _lookup_masterCategoryRepository.FirstOrDefaultAsync((int)output.PartnerType.MasterCategoryId);
                output.MasterCategoryName = _lookupMasterCategory?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_PartnerTypes_Edit)]
        public async Task<GetPartnerTypeForEditOutput> GetPartnerTypeForEdit(EntityDto input)
        {
            var partnerType = await _partnerTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPartnerTypeForEditOutput { PartnerType = ObjectMapper.Map<CreateOrEditPartnerTypeDto>(partnerType) };

            if (output.PartnerType.MasterCategoryId != null)
            {
                var _lookupMasterCategory = await _lookup_masterCategoryRepository.FirstOrDefaultAsync((int)output.PartnerType.MasterCategoryId);
                output.MasterCategoryName = _lookupMasterCategory?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPartnerTypeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_PartnerTypes_Create)]
        protected virtual async Task Create(CreateOrEditPartnerTypeDto input)
        {
            var partnerType = ObjectMapper.Map<PartnerType>(input);

            if (AbpSession.TenantId != null)
            {
                partnerType.TenantId = (int)AbpSession.TenantId;
            }

            await _partnerTypeRepository.InsertAsync(partnerType);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_PartnerTypes_Edit)]
        protected virtual async Task Update(CreateOrEditPartnerTypeDto input)
        {
            var partnerType = await _partnerTypeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, partnerType);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_PartnerTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _partnerTypeRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetPartnerTypesToExcel(GetAllPartnerTypesForExcelInput input)
        {

            var filteredPartnerTypes = _partnerTypeRepository.GetAll()
                        .Include(e => e.MasterCategoryFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MasterCategoryNameFilter), e => e.MasterCategoryFk != null && e.MasterCategoryFk.Name == input.MasterCategoryNameFilter);

            var query = (from o in filteredPartnerTypes
                         join o1 in _lookup_masterCategoryRepository.GetAll() on o.MasterCategoryId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetPartnerTypeForViewDto()
                         {
                             PartnerType = new PartnerTypeDto
                             {
                                 Abbrivation = o.Abbrivation,
                                 Name = o.Name,
                                 Id = o.Id
                             },
                             MasterCategoryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                         });

            var partnerTypeListDtos = await query.ToListAsync();

            return _partnerTypesExcelExporter.ExportToFile(partnerTypeListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_PartnerTypes)]
        public async Task<List<PartnerTypeMasterCategoryLookupTableDto>> GetAllMasterCategoryForTableDropdown()
        {
            return await _lookup_masterCategoryRepository.GetAll()
                .Select(masterCategory => new PartnerTypeMasterCategoryLookupTableDto
                {
                    Id = masterCategory.Id,
                    DisplayName = masterCategory == null || masterCategory.Name == null ? "" : masterCategory.Name.ToString()
                }).ToListAsync();
        }

    }
}