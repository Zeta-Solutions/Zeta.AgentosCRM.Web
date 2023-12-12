using Zeta.AgentosCRM.CRMProducts;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMProducts.Requirements.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMProducts.Requirements
{
    [AbpAuthorize(AppPermissions.Pages_ProductOtherTestRequirements)]
    public class ProductOtherTestRequirementsAppService : AgentosCRMAppServiceBase, IProductOtherTestRequirementsAppService
    {
        private readonly IRepository<ProductOtherTestRequirement> _productOtherTestRequirementRepository;
        private readonly IRepository<Product, long> _lookup_productRepository;

        public ProductOtherTestRequirementsAppService(IRepository<ProductOtherTestRequirement> productOtherTestRequirementRepository, IRepository<Product, long> lookup_productRepository)
        {
            _productOtherTestRequirementRepository = productOtherTestRequirementRepository;
            _lookup_productRepository = lookup_productRepository;

        }

        public async Task<PagedResultDto<GetProductOtherTestRequirementForViewDto>> GetAll(GetAllProductOtherTestRequirementsInput input)
        {

            var filteredProductOtherTestRequirements = _productOtherTestRequirementRepository.GetAll()
                        .Include(e => e.ProductFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(input.MinTotalScoreFilter != null, e => e.TotalScore >= input.MinTotalScoreFilter)
                        .WhereIf(input.MaxTotalScoreFilter != null, e => e.TotalScore <= input.MaxTotalScoreFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProductNameFilter), e => e.ProductFk != null && e.ProductFk.Name == input.ProductNameFilter)
                         .WhereIf(input.ProductIdFilter.HasValue, e => false || e.ProductId == input.ProductIdFilter.Value);
            var pagedAndFilteredProductOtherTestRequirements = filteredProductOtherTestRequirements
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var productOtherTestRequirements = from o in pagedAndFilteredProductOtherTestRequirements
                                               join o1 in _lookup_productRepository.GetAll() on o.ProductId equals o1.Id into j1
                                               from s1 in j1.DefaultIfEmpty()

                                               select new
                                               {

                                                   o.Name,
                                                   o.TotalScore,
                                                   Id = o.Id,
                                                   ProductName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                                               };

            var totalCount = await filteredProductOtherTestRequirements.CountAsync();

            var dbList = await productOtherTestRequirements.ToListAsync();
            var results = new List<GetProductOtherTestRequirementForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetProductOtherTestRequirementForViewDto()
                {
                    ProductOtherTestRequirement = new ProductOtherTestRequirementDto
                    {

                        Name = o.Name,
                        TotalScore = o.TotalScore,
                        Id = o.Id,
                    },
                    ProductName = o.ProductName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetProductOtherTestRequirementForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetProductOtherTestRequirementForViewDto> GetProductOtherTestRequirementForView(int id)
        {
            var productOtherTestRequirement = await _productOtherTestRequirementRepository.GetAsync(id);

            var output = new GetProductOtherTestRequirementForViewDto { ProductOtherTestRequirement = ObjectMapper.Map<ProductOtherTestRequirementDto>(productOtherTestRequirement) };

            if (output.ProductOtherTestRequirement.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.ProductOtherTestRequirement.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ProductOtherTestRequirements_Edit)]
        public async Task<GetProductOtherTestRequirementForEditOutput> GetProductOtherTestRequirementForEdit(EntityDto input)
        {
            var productOtherTestRequirement = await _productOtherTestRequirementRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetProductOtherTestRequirementForEditOutput { ProductOtherTestRequirement = ObjectMapper.Map<CreateOrEditProductOtherTestRequirementDto>(productOtherTestRequirement) };

            if (output.ProductOtherTestRequirement.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.ProductOtherTestRequirement.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditProductOtherTestRequirementDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ProductOtherTestRequirements_Create)]
        protected virtual async Task Create(CreateOrEditProductOtherTestRequirementDto input)
        {
            var productOtherTestRequirement = ObjectMapper.Map<ProductOtherTestRequirement>(input);

            if (AbpSession.TenantId != null)
            {
                productOtherTestRequirement.TenantId = (int)AbpSession.TenantId;
            }

            await _productOtherTestRequirementRepository.InsertAsync(productOtherTestRequirement);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductOtherTestRequirements_Edit)]
        protected virtual async Task Update(CreateOrEditProductOtherTestRequirementDto input)
        {
            var productOtherTestRequirement = await _productOtherTestRequirementRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, productOtherTestRequirement);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductOtherTestRequirements_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _productOtherTestRequirementRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_ProductOtherTestRequirements)]
        public async Task<List<ProductOtherTestRequirementProductLookupTableDto>> GetAllProductForTableDropdown()
        {
            return await _lookup_productRepository.GetAll()
                .Select(product => new ProductOtherTestRequirementProductLookupTableDto
                {
                    Id = product.Id,
                    DisplayName = product == null || product.Name == null ? "" : product.Name.ToString()
                }).ToListAsync();
        }

    }
}