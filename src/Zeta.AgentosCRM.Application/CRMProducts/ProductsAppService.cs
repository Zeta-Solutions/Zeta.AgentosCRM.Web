using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch; 
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMProducts.Exporting;
using Zeta.AgentosCRM.CRMProducts.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization; 
using Abp.Authorization;
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.CRMApplications;

namespace Zeta.AgentosCRM.CRMProducts
{
    [AbpAuthorize(AppPermissions.Pages_Products)]
    public class ProductsAppService : AgentosCRMAppServiceBase, IProductsAppService
    {
        private readonly IRepository<Product, long> _productRepository;
        private readonly IProductsExcelExporter _productsExcelExporter;
        private readonly IRepository<Partner, long> _lookup_partnerRepository;
        private readonly IRepository<PartnerType, int> _lookup_partnerTypeRepository;
        private readonly IRepository<Branch, long> _lookup_branchRepository;
        private readonly IRepository<ProductBranch> _productBranchRepository;
        private readonly IRepository<Application,long> _lookup_applicationRepository;
        public ProductsAppService(IRepository<Product, long> productRepository, 
            IProductsExcelExporter productsExcelExporter, IRepository<Partner, long> lookup_partnerRepository, 
            IRepository<PartnerType, int> lookup_partnerTypeRepository, IRepository<Branch, long> lookup_branchRepository, 
            IRepository<ProductBranch> productBranchRepository, IRepository<Application, long> lookup_applicationRepository)
        {
            _productRepository = productRepository;
            _productsExcelExporter = productsExcelExporter;
            _lookup_partnerRepository = lookup_partnerRepository;
            _lookup_partnerTypeRepository = lookup_partnerTypeRepository;
            _lookup_branchRepository = lookup_branchRepository;
            _productBranchRepository = productBranchRepository;
            _lookup_applicationRepository = lookup_applicationRepository;
        }

        public async Task<PagedResultDto<GetProductForViewDto>> GetAll(GetAllProductsInput input)
        {
            var intakeMonthFilter = input.IntakeMonthFilter.HasValue
                        ? (Months)input.IntakeMonthFilter
                        : default;

            var filteredProducts = _productRepository.GetAll()
                        .Include(e => e.PartnerFk)
                        .Include(e => e.PartnerTypeFk)
                        .Include(e => e.BranchFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Duration.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Note.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DurationFilter), e => e.Duration.Contains(input.DurationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NoteFilter), e => e.Note.Contains(input.NoteFilter))
                        .WhereIf(input.RevenueTypeFilter.HasValue && input.RevenueTypeFilter > -1, e => (input.RevenueTypeFilter == 1 && e.RevenueType) || (input.RevenueTypeFilter == 0 && !e.RevenueType))
                        .WhereIf(input.IntakeMonthFilter.HasValue && input.IntakeMonthFilter > -1, e => e.IntakeMonth == intakeMonthFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerTypeNameFilter), e => e.PartnerTypeFk != null && e.PartnerTypeFk.Name == input.PartnerTypeNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BranchNameFilter), e => e.BranchFk != null && e.BranchFk.Name == input.BranchNameFilter)
                         .WhereIf(input.PartnerIdFilter.HasValue, e => false || e.PartnerId == input.PartnerIdFilter.Value);
            var pagedAndFilteredProducts = filteredProducts
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var products = from o in pagedAndFilteredProducts
                           join o1 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           join o2 in _lookup_partnerTypeRepository.GetAll() on o.PartnerTypeId equals o2.Id into j2
                           from s2 in j2.DefaultIfEmpty()

                           join o3 in _lookup_branchRepository.GetAll() on o.BranchId equals o3.Id into j3
                           from s3 in j3.DefaultIfEmpty()


                           let ProgressCount = (from p in _lookup_applicationRepository.GetAll()
                                               where o.Id == p.ProductId && p.IsDiscontinue == false
                                                select p.Id
                                             ).Count()
                           let EnrolledCount = (from p in _lookup_applicationRepository.GetAll()
                                               where o.Id == p.ProductId
                                               select p.Id
                                             ).Count()

                           select new
                           { 
                               o.Name,
                               o.Duration,
                               o.Description,
                               o.Note,
                               o.RevenueType,
                               o.IntakeMonth,
                               Id = o.Id,
                               o.PartnerId,
                               productCount= ProgressCount,
                               EnrolledCount,
                               PartnerPartnerName = s1 == null || s1.PartnerName == null ? "" : s1.PartnerName.ToString(),
                               PartnerTypeName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                               BranchName = s3 == null || s3.Name == null ? "" : s3.Name.ToString()
                           };

            var totalCount = await filteredProducts.CountAsync();

            var dbList = await products.ToListAsync();
            var results = new List<GetProductForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetProductForViewDto()
                {
                    Product = new ProductDto
                    {

                        Name = o.Name,
                        Duration = o.Duration,
                        Description = o.Description,
                        Note = o.Note,
                        RevenueType = o.RevenueType,
                        IntakeMonth = o.IntakeMonth,
                        Id = o.Id,
                        PartnerId = o.PartnerId,
                        ProductCount=o.productCount,
                        EnrolledCount = o.EnrolledCount,
                    },
                    PartnerPartnerName = o.PartnerPartnerName,
                    PartnerTypeName = o.PartnerTypeName,
                    BranchName = o.BranchName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetProductForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<List<GetProductForViewDto>> GetProductsByPartnerId(List<long> partnerIds)
        {
            var products =  _productRepository.GetAll().Where(t => partnerIds.Contains(t.PartnerId));
            var dbList = await products.ToListAsync();
            var results = new List<GetProductForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetProductForViewDto()
                {
                    Product = new ProductDto
                    { 
                        Name = o.Name,
                        Duration = o.Duration,
                        Description = o.Description,
                        Note = o.Note,
                        RevenueType = o.RevenueType,
                        IntakeMonth = o.IntakeMonth,
                        Id = o.Id,
                        PartnerId = o.PartnerId,
                    } 
                };

                results.Add(res);
            }

            return results;
        }
        

        public async Task<GetProductForViewDto> GetProductForView(long id)
        {
            var product = await _productRepository.GetAsync(id);

            var output = new GetProductForViewDto { Product = ObjectMapper.Map<ProductDto>(product) };

            if (output.Product.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.Product.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.Product.PartnerTypeId != null)
            {
                var _lookupPartnerType = await _lookup_partnerTypeRepository.FirstOrDefaultAsync((int)output.Product.PartnerTypeId);
                output.PartnerTypeName = _lookupPartnerType?.Name?.ToString();
            }

            if (output.Product.BranchId != null)
            {
                var _lookupBranch = await _lookup_branchRepository.FirstOrDefaultAsync((long)output.Product.BranchId);
                output.BranchName = _lookupBranch?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Products_Edit)]
        public async Task<GetProductForEditOutput> GetProductForEdit(EntityDto<long> input)
        {
            var product = await _productRepository.FirstOrDefaultAsync(input.Id);
            var productbranch = await _productBranchRepository.GetAllListAsync(p => p.ProductId == input.Id);
            var output = new GetProductForEditOutput 
            {
                Product = ObjectMapper.Map<CreateOrEditProductDto>(product),
                Branches = ObjectMapper.Map<List<CreateOrEditProductBranchDto>>(productbranch)
            };

            if (output.Product.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.Product.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.Product.PartnerTypeId != null)
            {
                var _lookupPartnerType = await _lookup_partnerTypeRepository.FirstOrDefaultAsync((int)output.Product.PartnerTypeId);
                output.PartnerTypeName = _lookupPartnerType?.Name?.ToString();
            }

            if (output.Product.BranchId != null)
            {
                var _lookupBranch = await _lookup_branchRepository.FirstOrDefaultAsync((long)output.Product.BranchId);
                output.BranchName = _lookupBranch?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditProductDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Products_Create)]
        protected virtual async Task Create([FromBody] CreateOrEditProductDto input)
        {
            var product = ObjectMapper.Map<Product>(input);

            if (AbpSession.TenantId != null)
            {
                product.TenantId = (int)AbpSession.TenantId;
            }
            var branchId = _productRepository.InsertAndGetIdAsync(product).Result;
            foreach (var step in input.Branches)
            {
                step.ProductId = branchId;
                var stepEntity = ObjectMapper.Map<ProductBranch>(step);
                await _productBranchRepository.InsertAsync(stepEntity);
            }
            CurrentUnitOfWork.SaveChanges();
           // await _productRepository.InsertAsync(product);

        }

        [AbpAuthorize(AppPermissions.Pages_Products_Edit)]
        protected virtual async Task Update(CreateOrEditProductDto input)
        {
            var product = await _productRepository.FirstOrDefaultAsync((long)input.Id);
            input.ProfilePictureId = product.ProfilePictureId;
            ObjectMapper.Map(input, product);
            var productbranch = await _productBranchRepository.GetAllListAsync(p => p.ProductId == input.Id);
            foreach (var item in productbranch)
            {
                await _productBranchRepository.DeleteAsync(item.Id);
            }
            foreach (var step in input.Branches)
            {
                step.ProductId = product.Id;
                var stepEntity = ObjectMapper.Map<ProductBranch>(step);
                await _productBranchRepository.InsertAsync(stepEntity);
            }
            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(AppPermissions.Pages_Products_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _productRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetProductsToExcel(GetAllProductsForExcelInput input)
        {
            var intakeMonthFilter = input.IntakeMonthFilter.HasValue
                        ? (Months)input.IntakeMonthFilter
                        : default;

            var filteredProducts = _productRepository.GetAll()
                        .Include(e => e.PartnerFk)
                        .Include(e => e.PartnerTypeFk)
                        .Include(e => e.BranchFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Duration.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.Note.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DurationFilter), e => e.Duration.Contains(input.DurationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NoteFilter), e => e.Note.Contains(input.NoteFilter))
                        .WhereIf(input.RevenueTypeFilter.HasValue && input.RevenueTypeFilter > -1, e => (input.RevenueTypeFilter == 1 && e.RevenueType) || (input.RevenueTypeFilter == 0 && !e.RevenueType))
                        .WhereIf(input.IntakeMonthFilter.HasValue && input.IntakeMonthFilter > -1, e => e.IntakeMonth == intakeMonthFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerTypeNameFilter), e => e.PartnerTypeFk != null && e.PartnerTypeFk.Name == input.PartnerTypeNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BranchNameFilter), e => e.BranchFk != null && e.BranchFk.Name == input.BranchNameFilter);

            var query = (from o in filteredProducts
                         join o1 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_partnerTypeRepository.GetAll() on o.PartnerTypeId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _lookup_branchRepository.GetAll() on o.BranchId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         select new GetProductForViewDto()
                         {
                             Product = new ProductDto
                             {
                                 Name = o.Name,
                                 Duration = o.Duration,
                                 Description = o.Description,
                                 Note = o.Note,
                                 RevenueType = o.RevenueType,
                                 IntakeMonth = o.IntakeMonth,
                                 Id = o.Id
                             },
                             PartnerPartnerName = s1 == null || s1.PartnerName == null ? "" : s1.PartnerName.ToString(),
                             PartnerTypeName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                             BranchName = s3 == null || s3.Name == null ? "" : s3.Name.ToString()
                         });

            var productListDtos = await query.ToListAsync();

            return _productsExcelExporter.ExportToFile(productListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Products)]
        public async Task<List<ProductPartnerLookupTableDto>> GetAllPartnerForTableDropdown()
        {
            return await _lookup_partnerRepository.GetAll()
                .Select(partner => new ProductPartnerLookupTableDto
                {
                    Id = partner.Id,
                    DisplayName = partner == null || partner.PartnerName == null ? "" : partner.PartnerName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Products)]
        public async Task<List<ProductPartnerTypeLookupTableDto>> GetAllPartnerTypeForTableDropdown()
        {
            return await _lookup_partnerTypeRepository.GetAll()
                .Select(partnerType => new ProductPartnerTypeLookupTableDto
                {
                    Id = partnerType.Id,
                    DisplayName = partnerType == null || partnerType.Name == null ? "" : partnerType.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Products)]
        public async Task<List<ProductBranchLookupTableDto>> GetAllBranchForTableDropdown()
        {
            return await _lookup_branchRepository.GetAll()
                .Select(branch => new ProductBranchLookupTableDto
                {
                    Id = branch.Id,
                    DisplayName = branch == null || branch.Name == null ? "" : branch.Name.ToString()
                }).ToListAsync();
        }

    }
}