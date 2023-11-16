using Zeta.AgentosCRM.CRMPartner.Promotion;
using Zeta.AgentosCRM.CRMProducts;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMPartner.Promotion.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMPartner.Promotion
{
    [AbpAuthorize(AppPermissions.Pages_PromotionProducts)]
    public class PromotionProductsAppService : AgentosCRMAppServiceBase, IPromotionProductsAppService
    {
        private readonly IRepository<PromotionProduct, long> _promotionProductRepository;
        private readonly IRepository<PartnerPromotion, long> _lookup_partnerPromotionRepository;
        private readonly IRepository<Product, long> _lookup_productRepository;

        public PromotionProductsAppService(IRepository<PromotionProduct, long> promotionProductRepository, IRepository<PartnerPromotion, long> lookup_partnerPromotionRepository, IRepository<Product, long> lookup_productRepository)
        {
            _promotionProductRepository = promotionProductRepository;
            _lookup_partnerPromotionRepository = lookup_partnerPromotionRepository;
            _lookup_productRepository = lookup_productRepository;

        }

        public async Task<PagedResultDto<GetPromotionProductForViewDto>> GetAll(GetAllPromotionProductsInput input)
        {

            var filteredPromotionProducts = _promotionProductRepository.GetAll()
                        .Include(e => e.PartnerPromotionFk)
                        .Include(e => e.ProductFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPromotionNameFilter), e => e.PartnerPromotionFk != null && e.PartnerPromotionFk.Name == input.PartnerPromotionNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProductNameFilter), e => e.ProductFk != null && e.ProductFk.Name == input.ProductNameFilter);

            var pagedAndFilteredPromotionProducts = filteredPromotionProducts
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var promotionProducts = from o in pagedAndFilteredPromotionProducts
                                    join o1 in _lookup_partnerPromotionRepository.GetAll() on o.PartnerPromotionId equals o1.Id into j1
                                    from s1 in j1.DefaultIfEmpty()

                                    join o2 in _lookup_productRepository.GetAll() on o.ProductId equals o2.Id into j2
                                    from s2 in j2.DefaultIfEmpty()

                                    select new
                                    {

                                        o.Name,
                                        Id = o.Id,
                                        PartnerPromotionName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                        ProductName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                                    };

            var totalCount = await filteredPromotionProducts.CountAsync();

            var dbList = await promotionProducts.ToListAsync();
            var results = new List<GetPromotionProductForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPromotionProductForViewDto()
                {
                    PromotionProduct = new PromotionProductDto
                    {

                        Name = o.Name,
                        Id = o.Id,
                    },
                    PartnerPromotionName = o.PartnerPromotionName,
                    ProductName = o.ProductName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPromotionProductForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetPromotionProductForViewDto> GetPromotionProductForView(long id)
        {
            var promotionProduct = await _promotionProductRepository.GetAsync(id);

            var output = new GetPromotionProductForViewDto { PromotionProduct = ObjectMapper.Map<PromotionProductDto>(promotionProduct) };

            if (output.PromotionProduct.PartnerPromotionId != null)
            {
                var _lookupPartnerPromotion = await _lookup_partnerPromotionRepository.FirstOrDefaultAsync((long)output.PromotionProduct.PartnerPromotionId);
                output.PartnerPromotionName = _lookupPartnerPromotion?.Name?.ToString();
            }

            if (output.PromotionProduct.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.PromotionProduct.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_PromotionProducts_Edit)]
        public async Task<GetPromotionProductForEditOutput> GetPromotionProductForEdit(EntityDto<long> input)
        {
            var promotionProduct = await _promotionProductRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPromotionProductForEditOutput { PromotionProduct = ObjectMapper.Map<CreateOrEditPromotionProductDto>(promotionProduct) };

            if (output.PromotionProduct.PartnerPromotionId != null)
            {
                var _lookupPartnerPromotion = await _lookup_partnerPromotionRepository.FirstOrDefaultAsync((long)output.PromotionProduct.PartnerPromotionId);
                output.PartnerPromotionName = _lookupPartnerPromotion?.Name?.ToString();
            }

            if (output.PromotionProduct.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.PromotionProduct.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPromotionProductDto input)
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

        [AbpAuthorize(AppPermissions.Pages_PromotionProducts_Create)]
        protected virtual async Task Create(CreateOrEditPromotionProductDto input)
        {
            var promotionProduct = ObjectMapper.Map<PromotionProduct>(input);

            if (AbpSession.TenantId != null)
            {
                promotionProduct.TenantId = (int)AbpSession.TenantId;
            }

            await _promotionProductRepository.InsertAsync(promotionProduct);

        }

        [AbpAuthorize(AppPermissions.Pages_PromotionProducts_Edit)]
        protected virtual async Task Update(CreateOrEditPromotionProductDto input)
        {
            var promotionProduct = await _promotionProductRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, promotionProduct);

        }

        [AbpAuthorize(AppPermissions.Pages_PromotionProducts_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _promotionProductRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_PromotionProducts)]
        public async Task<List<PromotionProductPartnerPromotionLookupTableDto>> GetAllPartnerPromotionForTableDropdown()
        {
            return await _lookup_partnerPromotionRepository.GetAll()
                .Select(partnerPromotion => new PromotionProductPartnerPromotionLookupTableDto
                {
                    Id = partnerPromotion.Id,
                    DisplayName = partnerPromotion == null || partnerPromotion.Name == null ? "" : partnerPromotion.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_PromotionProducts)]
        public async Task<List<PromotionProductProductLookupTableDto>> GetAllProductForTableDropdown()
        {
            return await _lookup_productRepository.GetAll()
                .Select(product => new PromotionProductProductLookupTableDto
                {
                    Id = product.Id,
                    DisplayName = product == null || product.Name == null ? "" : product.Name.ToString()
                }).ToListAsync();
        }

    }
}