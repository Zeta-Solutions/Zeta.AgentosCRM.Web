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
    [AbpAuthorize(AppPermissions.Pages_CRMSetup_MasterCategories)]
    public class MasterCategoriesAppService : AgentosCRMAppServiceBase, IMasterCategoriesAppService
    {
        private readonly IRepository<MasterCategory> _masterCategoryRepository;
        private readonly IMasterCategoriesExcelExporter _masterCategoriesExcelExporter;

        public MasterCategoriesAppService(IRepository<MasterCategory> masterCategoryRepository, IMasterCategoriesExcelExporter masterCategoriesExcelExporter)
        {
            _masterCategoryRepository = masterCategoryRepository;
            _masterCategoriesExcelExporter = masterCategoriesExcelExporter;

        }

        public async Task<PagedResultDto<GetMasterCategoryForViewDto>> GetAll(GetAllMasterCategoriesInput input)
        {

            var filteredMasterCategories = _masterCategoryRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var pagedAndFilteredMasterCategories = filteredMasterCategories
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var masterCategories = from o in pagedAndFilteredMasterCategories
                                   select new
                                   {

                                       o.Abbrivation,
                                       o.Name,
                                       Id = o.Id
                                   };

            var totalCount = await filteredMasterCategories.CountAsync();

            var dbList = await masterCategories.ToListAsync();
            var results = new List<GetMasterCategoryForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetMasterCategoryForViewDto()
                {
                    MasterCategory = new MasterCategoryDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetMasterCategoryForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetMasterCategoryForViewDto> GetMasterCategoryForView(int id)
        {
            var masterCategory = await _masterCategoryRepository.GetAsync(id);

            var output = new GetMasterCategoryForViewDto { MasterCategory = ObjectMapper.Map<MasterCategoryDto>(masterCategory) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_MasterCategories_Edit)]
        public async Task<GetMasterCategoryForEditOutput> GetMasterCategoryForEdit(EntityDto input)
        {
            var masterCategory = await _masterCategoryRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetMasterCategoryForEditOutput { MasterCategory = ObjectMapper.Map<CreateOrEditMasterCategoryDto>(masterCategory) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditMasterCategoryDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_MasterCategories_Create)]
        protected virtual async Task Create(CreateOrEditMasterCategoryDto input)
        {
            var masterCategory = ObjectMapper.Map<MasterCategory>(input);

            if (AbpSession.TenantId != null)
            {
                masterCategory.TenantId = (int)AbpSession.TenantId;
            }

            await _masterCategoryRepository.InsertAsync(masterCategory);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_MasterCategories_Edit)]
        protected virtual async Task Update(CreateOrEditMasterCategoryDto input)
        {
            var masterCategory = await _masterCategoryRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, masterCategory);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_MasterCategories_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _masterCategoryRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetMasterCategoriesToExcel(GetAllMasterCategoriesForExcelInput input)
        {

            var filteredMasterCategories = _masterCategoryRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var query = (from o in filteredMasterCategories
                         select new GetMasterCategoryForViewDto()
                         {
                             MasterCategory = new MasterCategoryDto
                             {
                                 Abbrivation = o.Abbrivation,
                                 Name = o.Name,
                                 Id = o.Id
                             }
                         });

            var masterCategoryListDtos = await query.ToListAsync();

            return _masterCategoriesExcelExporter.ExportToFile(masterCategoryListDtos);
        }

    }
}