using Zeta.AgentosCRM.CRMProducts;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMProducts.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch.Dtos;
using Zeta.AgentosCRM.CRMPartner;

namespace Zeta.AgentosCRM.CRMProducts
{
    [AbpAuthorize(AppPermissions.Pages_ProductBranches)]
    public class ProductBranchesAppService : AgentosCRMAppServiceBase, IProductBranchesAppService
    {
        private readonly IRepository<ProductBranch> _productBranchRepository;
        private readonly IRepository<Product, long> _lookup_productRepository;
        private readonly IRepository<Branch, long> _lookup_branchRepository;

        public ProductBranchesAppService(IRepository<ProductBranch> productBranchRepository, IRepository<Product, long> lookup_productRepository, IRepository<Branch, long> lookup_branchRepository)
        {
            _productBranchRepository = productBranchRepository;
            _lookup_productRepository = lookup_productRepository;
            _lookup_branchRepository = lookup_branchRepository;

        }

        public async Task<PagedResultDto<GetProductBranchForViewDto>> GetAll(GetAllProductBranchesInput input)
        {

            var filteredProductBranches = _productBranchRepository.GetAll()
                        .Include(e => e.ProductFk)
                        .Include(e => e.BranchFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProductNameFilter), e => e.ProductFk != null && e.ProductFk.Name == input.ProductNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BranchNameFilter), e => e.BranchFk != null && e.BranchFk.Name == input.BranchNameFilter)
                         .WhereIf(input.BranchIdFilter.HasValue, e => false || e.BranchId == input.BranchIdFilter.Value)
                         .WhereIf(input.ProductIdFilter.HasValue, e => false || e.ProductId == input.ProductIdFilter.Value);
            var pagedAndFilteredProductBranches = filteredProductBranches
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var productBranches = from o in pagedAndFilteredProductBranches
                                  join o1 in _lookup_productRepository.GetAll() on o.ProductId equals o1.Id into j1
                                  from s1 in j1.DefaultIfEmpty()

                                  join o2 in _lookup_branchRepository.GetAll() on o.BranchId equals o2.Id into j2
                                  from s2 in j2.DefaultIfEmpty()

                                  select new
                                  {

                                      o.Name,
                                      Id = o.Id,
                                      ProductName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                      BranchName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                                  };

            var totalCount = await filteredProductBranches.CountAsync();

            var dbList = await productBranches.ToListAsync();
            var results = new List<GetProductBranchForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetProductBranchForViewDto()
                {
                    ProductBranch = new ProductBranchDto
                    {

                        Name = o.Name,
                        Id = o.Id,
                    },
                    ProductName = o.ProductName,
                    BranchName = o.BranchName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetProductBranchForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetProductBranchForViewDto> GetProductBranchForView(int id)
        {
            var productBranch = await _productBranchRepository.GetAsync(id);

            var output = new GetProductBranchForViewDto { ProductBranch = ObjectMapper.Map<ProductBranchDto>(productBranch) };

            if (output.ProductBranch.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.ProductBranch.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            if (output.ProductBranch.BranchId != null)
            {
                var _lookupBranch = await _lookup_branchRepository.FirstOrDefaultAsync((long)output.ProductBranch.BranchId);
                output.BranchName = _lookupBranch?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ProductBranches_Edit)]
        public async Task<GetProductBranchForEditOutput> GetProductBranchForEdit(EntityDto input)
        {
            var productBranch = await _productBranchRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetProductBranchForEditOutput { ProductBranch = ObjectMapper.Map<CreateOrEditProductBranchDto>(productBranch) };

            if (output.ProductBranch.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.ProductBranch.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            if (output.ProductBranch.BranchId != null)
            {
                var _lookupBranch = await _lookup_branchRepository.FirstOrDefaultAsync((long)output.ProductBranch.BranchId);
                output.BranchName = _lookupBranch?.Name?.ToString();
            }

            return output;
        }

        //public async Task<List<GetProductBranchForViewDto>> GetproductbybranchId(long branchid)
        //{
        //    var products = _productBranchRepository.GetAll().Where(t => branchid == t.BranchId);
        //    var pagedAndFilteredBranches = _lookup_productRepository.GetAll();

        //    var Products = from o in pagedAndFilteredBranches

        //                   join o2 in products on o.Id equals o2.ProductId

        //                   select new
        //                   {

        //                       Id = o.Id,
        //                       ProductName = o2 == null || o2.Name == null ? "" : o2.Name.ToString(),
        //                   };

        //    var dbList = await Products.ToListAsync();
        //    var results = new List<GetProductBranchForViewDto>();

        //    foreach (var o in dbList)
        //    {
        //        var res = new GetProductBranchForViewDto()
        //        {
        //            ProductBranch = new ProductBranchDto
        //            {

        //                Id = o.Id,
                       
        //            }
        //             ProductName = o.ProductName
        //        };

        //        results.Add(res);
        //    }

        //    return results;
        //}

        public async Task CreateOrEdit(CreateOrEditProductBranchDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ProductBranches_Create)]
        protected virtual async Task Create(CreateOrEditProductBranchDto input)
        {
            var productBranch = ObjectMapper.Map<ProductBranch>(input);

            if (AbpSession.TenantId != null)
            {
                productBranch.TenantId = (int)AbpSession.TenantId;
            }

            await _productBranchRepository.InsertAsync(productBranch);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductBranches_Edit)]
        protected virtual async Task Update(CreateOrEditProductBranchDto input)
        {
            var productBranch = await _productBranchRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, productBranch);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductBranches_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _productBranchRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_ProductBranches)]
        public async Task<List<ProductBranchProductLookupTableDto>> GetAllProductForTableDropdown()
        {
            return await _lookup_productRepository.GetAll()
                .Select(product => new ProductBranchProductLookupTableDto
                {
                    Id = product.Id,
                    DisplayName = product == null || product.Name == null ? "" : product.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ProductBranches)]
        public async Task<List<ProductBranchBranchLookupTableDto>> GetAllBranchForTableDropdown()
        {
            return await _lookup_branchRepository.GetAll()
                .Select(branch => new ProductBranchBranchLookupTableDto
                {
                    Id = branch.Id,
                    DisplayName = branch == null || branch.Name == null ? "" : branch.Name.ToString()
                }).ToListAsync();
        }

    }
}