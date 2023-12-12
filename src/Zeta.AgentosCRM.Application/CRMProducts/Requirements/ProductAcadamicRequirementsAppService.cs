using Zeta.AgentosCRM.CRMSetup;

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
    [AbpAuthorize(AppPermissions.Pages_ProductAcadamicRequirements)]
    public class ProductAcadamicRequirementsAppService : AgentosCRMAppServiceBase, IProductAcadamicRequirementsAppService
    {
        private readonly IRepository<ProductAcadamicRequirement> _productAcadamicRequirementRepository;
        private readonly IRepository<DegreeLevel, int> _lookup_degreeLevelRepository;
        private readonly IRepository<Product, long> _lookup_productRepository;

        public ProductAcadamicRequirementsAppService(IRepository<ProductAcadamicRequirement> productAcadamicRequirementRepository, IRepository<DegreeLevel, int> lookup_degreeLevelRepository, IRepository<Product, long> lookup_productRepository)
        {
            _productAcadamicRequirementRepository = productAcadamicRequirementRepository;
            _lookup_degreeLevelRepository = lookup_degreeLevelRepository;
            _lookup_productRepository = lookup_productRepository;
        }

        public async Task<PagedResultDto<GetProductAcadamicRequirementForViewDto>> GetAll(GetAllProductAcadamicRequirementsInput input)
        {

            var filteredProductAcadamicRequirements = _productAcadamicRequirementRepository.GetAll()
                        .Include(e => e.DegreeLevelFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Title.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.Contains(input.TitleFilter))
                        .WhereIf(input.MinAcadamicScoreFilter != null, e => e.AcadamicScore >= input.MinAcadamicScoreFilter)
                        .WhereIf(input.MaxAcadamicScoreFilter != null, e => e.AcadamicScore <= input.MaxAcadamicScoreFilter)
                        .WhereIf(input.IsGPAFilter.HasValue && input.IsGPAFilter > -1, e => (input.IsGPAFilter == 1 && e.IsGPA) || (input.IsGPAFilter == 0 && !e.IsGPA))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DegreeLevelNameFilter), e => e.DegreeLevelFk != null && e.DegreeLevelFk.Name == input.DegreeLevelNameFilter)
                        .WhereIf(input.ProductIdFilter.HasValue, e => false || e.ProductId == input.ProductIdFilter.Value);
            var pagedAndFilteredProductAcadamicRequirements = filteredProductAcadamicRequirements
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var productAcadamicRequirements = from o in pagedAndFilteredProductAcadamicRequirements
                                              join o1 in _lookup_degreeLevelRepository.GetAll() on o.DegreeLevelId equals o1.Id into j1
                                              from s1 in j1.DefaultIfEmpty()

                                              select new
                                              {

                                                  o.Title,
                                                  o.AcadamicScore,
                                                  o.IsGPA,
                                                  Id = o.Id,
                                                  DegreeLevelName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                                              };

            var totalCount = await filteredProductAcadamicRequirements.CountAsync();

            var dbList = await productAcadamicRequirements.ToListAsync();
            var results = new List<GetProductAcadamicRequirementForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetProductAcadamicRequirementForViewDto()
                {
                    ProductAcadamicRequirement = new ProductAcadamicRequirementDto
                    {

                        Title = o.Title,
                        AcadamicScore = o.AcadamicScore,
                        IsGPA = o.IsGPA,
                        Id = o.Id,
                    },
                    DegreeLevelName = o.DegreeLevelName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetProductAcadamicRequirementForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetProductAcadamicRequirementForViewDto> GetProductAcadamicRequirementForView(int id)
        {
            var productAcadamicRequirement = await _productAcadamicRequirementRepository.GetAsync(id);

            var output = new GetProductAcadamicRequirementForViewDto { ProductAcadamicRequirement = ObjectMapper.Map<ProductAcadamicRequirementDto>(productAcadamicRequirement) };

            if (output.ProductAcadamicRequirement.DegreeLevelId != null)
            {
                var _lookupDegreeLevel = await _lookup_degreeLevelRepository.FirstOrDefaultAsync((int)output.ProductAcadamicRequirement.DegreeLevelId);
                output.DegreeLevelName = _lookupDegreeLevel?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ProductAcadamicRequirements_Edit)]
        public async Task<GetProductAcadamicRequirementForEditOutput> GetProductAcadamicRequirementForEdit(EntityDto input)
        {
            var productAcadamicRequirement = await _productAcadamicRequirementRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetProductAcadamicRequirementForEditOutput { ProductAcadamicRequirement = ObjectMapper.Map<CreateOrEditProductAcadamicRequirementDto>(productAcadamicRequirement) };

            if (output.ProductAcadamicRequirement.DegreeLevelId != null)
            {
                var _lookupDegreeLevel = await _lookup_degreeLevelRepository.FirstOrDefaultAsync((int)output.ProductAcadamicRequirement.DegreeLevelId);
                output.DegreeLevelName = _lookupDegreeLevel?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditProductAcadamicRequirementDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ProductAcadamicRequirements_Create)]
        protected virtual async Task Create(CreateOrEditProductAcadamicRequirementDto input)
        {
            var productAcadamicRequirement = ObjectMapper.Map<ProductAcadamicRequirement>(input);

            if (AbpSession.TenantId != null)
            {
                productAcadamicRequirement.TenantId = (int)AbpSession.TenantId;
            }

            await _productAcadamicRequirementRepository.InsertAsync(productAcadamicRequirement);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductAcadamicRequirements_Edit)]
        protected virtual async Task Update(CreateOrEditProductAcadamicRequirementDto input)
        {
            var productAcadamicRequirement = await _productAcadamicRequirementRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, productAcadamicRequirement);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductAcadamicRequirements_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _productAcadamicRequirementRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_ProductAcadamicRequirements)]
        public async Task<List<ProductAcadamicRequirementDegreeLevelLookupTableDto>> GetAllDegreeLevelForTableDropdown()
        {
            return await _lookup_degreeLevelRepository.GetAll()
                .Select(degreeLevel => new ProductAcadamicRequirementDegreeLevelLookupTableDto
                {
                    Id = degreeLevel.Id,
                    DisplayName = degreeLevel == null || degreeLevel.Name == null ? "" : degreeLevel.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ProductAcadamicRequirements)]
        public async Task<List<ProductAcadamicRequirementProductLookupTableDto>> GetAllProductForTableDropdown()
        {
            return await _lookup_productRepository.GetAll()
                .Select(product => new ProductAcadamicRequirementProductLookupTableDto
                {
                    Id = product.Id,
                    DisplayName = product == null || product.Name == null ? "" : product.Name.ToString()
                }).ToListAsync();
        }

    }
}