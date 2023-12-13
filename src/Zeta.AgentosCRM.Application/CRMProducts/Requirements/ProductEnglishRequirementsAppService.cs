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
using Zeta.AgentosCRM.CRMAppointments.Invitees;

namespace Zeta.AgentosCRM.CRMProducts.Requirements
{
    [AbpAuthorize(AppPermissions.Pages_ProductEnglishRequirements)]
    public class ProductEnglishRequirementsAppService : AgentosCRMAppServiceBase, IProductEnglishRequirementsAppService
    {
        private readonly IRepository<ProductEnglishRequirement> _productEnglishRequirementRepository;
        private readonly IRepository<Product, long> _lookup_productRepository;

        public ProductEnglishRequirementsAppService(IRepository<ProductEnglishRequirement> productEnglishRequirementRepository, IRepository<Product, long> lookup_productRepository)
        {
            _productEnglishRequirementRepository = productEnglishRequirementRepository;
            _lookup_productRepository = lookup_productRepository;

        }

        public async Task<PagedResultDto<GetProductEnglishRequirementForViewDto>> GetAll(GetAllProductEnglishRequirementsInput input)
        {

            var filteredProductEnglishRequirements = _productEnglishRequirementRepository.GetAll()
                        .Include(e => e.ProductFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(input.MinListeningFilter != null, e => e.Listening >= input.MinListeningFilter)
                        .WhereIf(input.MaxListeningFilter != null, e => e.Listening <= input.MaxListeningFilter)
                        .WhereIf(input.MinReadingFilter != null, e => e.Reading >= input.MinReadingFilter)
                        .WhereIf(input.MaxReadingFilter != null, e => e.Reading <= input.MaxReadingFilter)
                        .WhereIf(input.MinWritingFilter != null, e => e.Writing >= input.MinWritingFilter)
                        .WhereIf(input.MaxWritingFilter != null, e => e.Writing <= input.MaxWritingFilter)
                        .WhereIf(input.MinSpeakingFilter != null, e => e.Speaking >= input.MinSpeakingFilter)
                        .WhereIf(input.MaxSpeakingFilter != null, e => e.Speaking <= input.MaxSpeakingFilter)
                        .WhereIf(input.MinTotalScoreFilter != null, e => e.TotalScore >= input.MinTotalScoreFilter)
                        .WhereIf(input.MaxTotalScoreFilter != null, e => e.TotalScore <= input.MaxTotalScoreFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProductNameFilter), e => e.ProductFk != null && e.ProductFk.Name == input.ProductNameFilter)
              .WhereIf(input.ProductIdFilter.HasValue, e => false || e.ProductId == input.ProductIdFilter.Value);
            var pagedAndFilteredProductEnglishRequirements = filteredProductEnglishRequirements
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var productEnglishRequirements = from o in pagedAndFilteredProductEnglishRequirements
                                             join o1 in _lookup_productRepository.GetAll() on o.ProductId equals o1.Id into j1
                                             from s1 in j1.DefaultIfEmpty()

                                             select new
                                             {

                                                 o.Name,
                                                 o.Listening,
                                                 o.Reading,
                                                 o.Writing,
                                                 o.Speaking,
                                                 o.TotalScore,
                                                 Id = o.Id,
                                                 ProductName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                                             };

            var totalCount = await filteredProductEnglishRequirements.CountAsync();

            var dbList = await productEnglishRequirements.ToListAsync();
            var results = new List<GetProductEnglishRequirementForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetProductEnglishRequirementForViewDto()
                {
                    ProductEnglishRequirement = new ProductEnglishRequirementDto
                    {

                        Name = o.Name,
                        Listening = o.Listening,
                        Reading = o.Reading,
                        Writing = o.Writing,
                        Speaking = o.Speaking,
                        TotalScore = o.TotalScore,
                        Id = o.Id,
                    },
                    ProductName = o.ProductName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetProductEnglishRequirementForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetProductEnglishRequirementForViewDto> GetProductEnglishRequirementForView(int id)
        {
            var productEnglishRequirement = await _productEnglishRequirementRepository.GetAsync(id);

            var output = new GetProductEnglishRequirementForViewDto { ProductEnglishRequirement = ObjectMapper.Map<ProductEnglishRequirementDto>(productEnglishRequirement) };

            if (output.ProductEnglishRequirement.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.ProductEnglishRequirement.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ProductEnglishRequirements_Edit)]
        public async Task<GetProductEnglishRequirementForEditOutput> GetProductEnglishRequirementForEdit(EntityDto input)
        {
            var productEnglishRequirement = await _productEnglishRequirementRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetProductEnglishRequirementForEditOutput { ProductEnglishRequirement = ObjectMapper.Map<CreateOrEditProductEnglishRequirementDto>(productEnglishRequirement) };

            if (output.ProductEnglishRequirement.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.ProductEnglishRequirement.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditProductEnglishRequirementDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ProductEnglishRequirements_Create)]
        protected virtual async Task Create(CreateOrEditProductEnglishRequirementDto input)
        {
            var productEnglishRequirement = ObjectMapper.Map<ProductEnglishRequirement>(input);

            if (AbpSession.TenantId != null)
            {
                productEnglishRequirement.TenantId = (int)AbpSession.TenantId;
            }

            await _productEnglishRequirementRepository.InsertAsync(productEnglishRequirement);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductEnglishRequirements_Edit)]
        protected virtual async Task Update(CreateOrEditProductEnglishRequirementDto input)
        {
            var productEnglishRequirement = await _productEnglishRequirementRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, productEnglishRequirement);
            //var productEnglishRequirement = await _productEnglishRequirementRepository.GetAllListAsync(p => p.ProductId == input.Id);
            //foreach (var item in productEnglishRequirement)
            //{
            //    await _productEnglishRequirementRepository.DeleteAsync(item.Id);
            //} 

            //var productEnglishRequirementEntity = ObjectMapper.Map<ProductEnglishRequirement>(input);
            //await _productEnglishRequirementRepository.InsertAsync(productEnglishRequirementEntity);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductEnglishRequirements_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _productEnglishRequirementRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_ProductEnglishRequirements)]
        public async Task<List<ProductEnglishRequirementProductLookupTableDto>> GetAllProductForTableDropdown()
        {
            return await _lookup_productRepository.GetAll()
                .Select(product => new ProductEnglishRequirementProductLookupTableDto
                {
                    Id = product.Id,
                    DisplayName = product == null || product.Name == null ? "" : product.Name.ToString()
                }).ToListAsync();
        }

    }
}