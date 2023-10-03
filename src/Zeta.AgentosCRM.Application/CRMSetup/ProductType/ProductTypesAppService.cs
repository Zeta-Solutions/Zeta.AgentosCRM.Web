using Zeta.AgentosCRM.CRMSetup;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.ProductType.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup.ProductType
{
    [AbpAuthorize(AppPermissions.Pages_ProductTypes)]
    public class ProductTypesAppService : AgentosCRMAppServiceBase, IProductTypesAppService
    {
        private readonly IRepository<ProductType> _productTypeRepository;
        private readonly IRepository<MasterCategory, int> _lookup_masterCategoryRepository;

        public ProductTypesAppService(IRepository<ProductType> productTypeRepository, IRepository<MasterCategory, int> lookup_masterCategoryRepository)
        {
            _productTypeRepository = productTypeRepository;
            _lookup_masterCategoryRepository = lookup_masterCategoryRepository;

        }

        public async Task<PagedResultDto<GetProductTypeForViewDto>> GetAll(GetAllProductTypesInput input)
        {

            var filteredProductTypes = _productTypeRepository.GetAll()
                        .Include(e => e.MasterCategoryFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivaion.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivaionFilter), e => e.Abbrivaion.Contains(input.AbbrivaionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MasterCategoryNameFilter), e => e.MasterCategoryFk != null && e.MasterCategoryFk.Name == input.MasterCategoryNameFilter);

            var pagedAndFilteredProductTypes = filteredProductTypes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var productTypes = from o in pagedAndFilteredProductTypes
                               join o1 in _lookup_masterCategoryRepository.GetAll() on o.MasterCategoryId equals o1.Id into j1
                               from s1 in j1.DefaultIfEmpty()

                               select new
                               {

                                   o.Abbrivaion,
                                   o.Name,
                                   Id = o.Id,
                                   MasterCategoryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                               };

            var totalCount = await filteredProductTypes.CountAsync();

            var dbList = await productTypes.ToListAsync();
            var results = new List<GetProductTypeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetProductTypeForViewDto()
                {
                    ProductType = new ProductTypeDto
                    {

                        Abbrivaion = o.Abbrivaion,
                        Name = o.Name,
                        Id = o.Id,
                    },
                    MasterCategoryName = o.MasterCategoryName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetProductTypeForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetProductTypeForViewDto> GetProductTypeForView(int id)
        {
            var productType = await _productTypeRepository.GetAsync(id);

            var output = new GetProductTypeForViewDto { ProductType = ObjectMapper.Map<ProductTypeDto>(productType) };

            if (output.ProductType.MasterCategoryId != null)
            {
                var _lookupMasterCategory = await _lookup_masterCategoryRepository.FirstOrDefaultAsync((int)output.ProductType.MasterCategoryId);
                output.MasterCategoryName = _lookupMasterCategory?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ProductTypes_Edit)]
        public async Task<GetProductTypeForEditOutput> GetProductTypeForEdit(EntityDto input)
        {
            var productType = await _productTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetProductTypeForEditOutput { ProductType = ObjectMapper.Map<CreateOrEditProductTypeDto>(productType) };

            if (output.ProductType.MasterCategoryId != null)
            {
                var _lookupMasterCategory = await _lookup_masterCategoryRepository.FirstOrDefaultAsync((int)output.ProductType.MasterCategoryId);
                output.MasterCategoryName = _lookupMasterCategory?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditProductTypeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ProductTypes_Create)]
        protected virtual async Task Create(CreateOrEditProductTypeDto input)
        {
            var productType = ObjectMapper.Map<ProductType>(input);

            if (AbpSession.TenantId != null)
            {
                productType.TenantId = (int)AbpSession.TenantId;
            }

            await _productTypeRepository.InsertAsync(productType);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductTypes_Edit)]
        protected virtual async Task Update(CreateOrEditProductTypeDto input)
        {
            var productType = await _productTypeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, productType);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _productTypeRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_ProductTypes)]
        public async Task<List<ProductTypeMasterCategoryLookupTableDto>> GetAllMasterCategoryForTableDropdown()
        {
            return await _lookup_masterCategoryRepository.GetAll()
                .Select(masterCategory => new ProductTypeMasterCategoryLookupTableDto
                {
                    Id = masterCategory.Id,
                    DisplayName = masterCategory == null || masterCategory.Name == null ? "" : masterCategory.Name.ToString()
                }).ToListAsync();
        }

    }
}