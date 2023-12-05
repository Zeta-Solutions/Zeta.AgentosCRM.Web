using Zeta.AgentosCRM.CRMSetup.FeeType;
using Zeta.AgentosCRM.CRMProducts.Fee;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMProducts.Fee.Exporting;
using Zeta.AgentosCRM.CRMProducts.Fee.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMProducts.Fee
{
    [AbpAuthorize(AppPermissions.Pages_ProductFeeDetails)]
    public class ProductFeeDetailsAppService : AgentosCRMAppServiceBase, IProductFeeDetailsAppService
    {
        private readonly IRepository<ProductFeeDetail, long> _productFeeDetailRepository;
        private readonly IProductFeeDetailsExcelExporter _productFeeDetailsExcelExporter;
        private readonly IRepository<FeeType, int> _lookup_feeTypeRepository;
        private readonly IRepository<ProductFee, int> _lookup_productFeeRepository;

        public ProductFeeDetailsAppService(IRepository<ProductFeeDetail, long> productFeeDetailRepository, IProductFeeDetailsExcelExporter productFeeDetailsExcelExporter, IRepository<FeeType, int> lookup_feeTypeRepository, IRepository<ProductFee, int> lookup_productFeeRepository)
        {
            _productFeeDetailRepository = productFeeDetailRepository;
            _productFeeDetailsExcelExporter = productFeeDetailsExcelExporter;
            _lookup_feeTypeRepository = lookup_feeTypeRepository;
            _lookup_productFeeRepository = lookup_productFeeRepository;

        }

        public async Task<PagedResultDto<GetProductFeeDetailForViewDto>> GetAll(GetAllProductFeeDetailsInput input)
        {

            var filteredProductFeeDetails = _productFeeDetailRepository.GetAll()
                        .Include(e => e.FeeTypeFk)
                        .Include(e => e.ProductFeeFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ClaimTerms.Contains(input.Filter))
                        .WhereIf(input.MinInstallmentAmountFilter != null, e => e.InstallmentAmount >= input.MinInstallmentAmountFilter)
                        .WhereIf(input.MaxInstallmentAmountFilter != null, e => e.InstallmentAmount <= input.MaxInstallmentAmountFilter)
                        .WhereIf(input.MinInstallmentsFilter != null, e => e.Installments >= input.MinInstallmentsFilter)
                        .WhereIf(input.MaxInstallmentsFilter != null, e => e.Installments <= input.MaxInstallmentsFilter)
                        .WhereIf(input.MinTotalFeeFilter != null, e => e.TotalFee >= input.MinTotalFeeFilter)
                        .WhereIf(input.MaxTotalFeeFilter != null, e => e.TotalFee <= input.MaxTotalFeeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClaimTermsFilter), e => e.ClaimTerms.Contains(input.ClaimTermsFilter))
                        .WhereIf(input.MinCommissionPerFilter != null, e => e.CommissionPer >= input.MinCommissionPerFilter)
                        .WhereIf(input.MaxCommissionPerFilter != null, e => e.CommissionPer <= input.MaxCommissionPerFilter)
                        .WhereIf(input.IsPayableFilter.HasValue && input.IsPayableFilter > -1, e => (input.IsPayableFilter == 1 && e.IsPayable) || (input.IsPayableFilter == 0 && !e.IsPayable))
                        .WhereIf(input.AddInQuotationFilter.HasValue && input.AddInQuotationFilter > -1, e => (input.AddInQuotationFilter == 1 && e.AddInQuotation) || (input.AddInQuotationFilter == 0 && !e.AddInQuotation))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FeeTypeNameFilter), e => e.FeeTypeFk != null && e.FeeTypeFk.Name == input.FeeTypeNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProductFeeNameFilter), e => e.ProductFeeFk != null && e.ProductFeeFk.Name == input.ProductFeeNameFilter);

            var pagedAndFilteredProductFeeDetails = filteredProductFeeDetails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var productFeeDetails = from o in pagedAndFilteredProductFeeDetails
                                    join o1 in _lookup_feeTypeRepository.GetAll() on o.FeeTypeId equals o1.Id into j1
                                    from s1 in j1.DefaultIfEmpty()

                                    join o2 in _lookup_productFeeRepository.GetAll() on o.ProductFeeId equals o2.Id into j2
                                    from s2 in j2.DefaultIfEmpty()

                                    select new
                                    {

                                        o.InstallmentAmount,
                                        o.Installments,
                                        o.TotalFee,
                                        o.ClaimTerms,
                                        o.CommissionPer,
                                        o.IsPayable,
                                        o.AddInQuotation,
                                        Id = o.Id,
                                        FeeTypeName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                        ProductFeeName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                                    };

            var totalCount = await filteredProductFeeDetails.CountAsync();

            var dbList = await productFeeDetails.ToListAsync();
            var results = new List<GetProductFeeDetailForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetProductFeeDetailForViewDto()
                {
                    ProductFeeDetail = new ProductFeeDetailDto
                    {

                        InstallmentAmount = o.InstallmentAmount,
                        Installments = o.Installments,
                        TotalFee = o.TotalFee,
                        ClaimTerms = o.ClaimTerms,
                        CommissionPer = o.CommissionPer,
                        IsPayable = o.IsPayable,
                        AddInQuotation = o.AddInQuotation,
                        Id = o.Id,
                    },
                    FeeTypeName = o.FeeTypeName,
                    ProductFeeName = o.ProductFeeName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetProductFeeDetailForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetProductFeeDetailForViewDto> GetProductFeeDetailForView(long id)
        {
            var productFeeDetail = await _productFeeDetailRepository.GetAsync(id);

            var output = new GetProductFeeDetailForViewDto { ProductFeeDetail = ObjectMapper.Map<ProductFeeDetailDto>(productFeeDetail) };

            if (output.ProductFeeDetail.FeeTypeId != null)
            {
                var _lookupFeeType = await _lookup_feeTypeRepository.FirstOrDefaultAsync((int)output.ProductFeeDetail.FeeTypeId);
                output.FeeTypeName = _lookupFeeType?.Name?.ToString();
            }

            if (output.ProductFeeDetail.ProductFeeId != null)
            {
                var _lookupProductFee = await _lookup_productFeeRepository.FirstOrDefaultAsync((int)output.ProductFeeDetail.ProductFeeId);
                output.ProductFeeName = _lookupProductFee?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ProductFeeDetails_Edit)]
        public async Task<GetProductFeeDetailForEditOutput> GetProductFeeDetailForEdit(EntityDto<long> input)
        {
            var productFeeDetail = await _productFeeDetailRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetProductFeeDetailForEditOutput { ProductFeeDetail = ObjectMapper.Map<CreateOrEditProductFeeDetailDto>(productFeeDetail) };

            if (output.ProductFeeDetail.FeeTypeId != null)
            {
                var _lookupFeeType = await _lookup_feeTypeRepository.FirstOrDefaultAsync((int)output.ProductFeeDetail.FeeTypeId);
                output.FeeTypeName = _lookupFeeType?.Name?.ToString();
            }

            if (output.ProductFeeDetail.ProductFeeId != null)
            {
                var _lookupProductFee = await _lookup_productFeeRepository.FirstOrDefaultAsync((int)output.ProductFeeDetail.ProductFeeId);
                output.ProductFeeName = _lookupProductFee?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditProductFeeDetailDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ProductFeeDetails_Create)]
        protected virtual async Task Create(CreateOrEditProductFeeDetailDto input)
        {
            var productFeeDetail = ObjectMapper.Map<ProductFeeDetail>(input);

            if (AbpSession.TenantId != null)
            {
                productFeeDetail.TenantId = (int)AbpSession.TenantId;
            }

            await _productFeeDetailRepository.InsertAsync(productFeeDetail);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductFeeDetails_Edit)]
        protected virtual async Task Update(CreateOrEditProductFeeDetailDto input)
        {
            var productFeeDetail = await _productFeeDetailRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, productFeeDetail);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductFeeDetails_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _productFeeDetailRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetProductFeeDetailsToExcel(GetAllProductFeeDetailsForExcelInput input)
        {

            var filteredProductFeeDetails = _productFeeDetailRepository.GetAll()
                        .Include(e => e.FeeTypeFk)
                        .Include(e => e.ProductFeeFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ClaimTerms.Contains(input.Filter))
                        .WhereIf(input.MinInstallmentAmountFilter != null, e => e.InstallmentAmount >= input.MinInstallmentAmountFilter)
                        .WhereIf(input.MaxInstallmentAmountFilter != null, e => e.InstallmentAmount <= input.MaxInstallmentAmountFilter)
                        .WhereIf(input.MinInstallmentsFilter != null, e => e.Installments >= input.MinInstallmentsFilter)
                        .WhereIf(input.MaxInstallmentsFilter != null, e => e.Installments <= input.MaxInstallmentsFilter)
                        .WhereIf(input.MinTotalFeeFilter != null, e => e.TotalFee >= input.MinTotalFeeFilter)
                        .WhereIf(input.MaxTotalFeeFilter != null, e => e.TotalFee <= input.MaxTotalFeeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClaimTermsFilter), e => e.ClaimTerms.Contains(input.ClaimTermsFilter))
                        .WhereIf(input.MinCommissionPerFilter != null, e => e.CommissionPer >= input.MinCommissionPerFilter)
                        .WhereIf(input.MaxCommissionPerFilter != null, e => e.CommissionPer <= input.MaxCommissionPerFilter)
                        .WhereIf(input.IsPayableFilter.HasValue && input.IsPayableFilter > -1, e => (input.IsPayableFilter == 1 && e.IsPayable) || (input.IsPayableFilter == 0 && !e.IsPayable))
                        .WhereIf(input.AddInQuotationFilter.HasValue && input.AddInQuotationFilter > -1, e => (input.AddInQuotationFilter == 1 && e.AddInQuotation) || (input.AddInQuotationFilter == 0 && !e.AddInQuotation))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FeeTypeNameFilter), e => e.FeeTypeFk != null && e.FeeTypeFk.Name == input.FeeTypeNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProductFeeNameFilter), e => e.ProductFeeFk != null && e.ProductFeeFk.Name == input.ProductFeeNameFilter);

            var query = (from o in filteredProductFeeDetails
                         join o1 in _lookup_feeTypeRepository.GetAll() on o.FeeTypeId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_productFeeRepository.GetAll() on o.ProductFeeId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetProductFeeDetailForViewDto()
                         {
                             ProductFeeDetail = new ProductFeeDetailDto
                             {
                                 InstallmentAmount = o.InstallmentAmount,
                                 Installments = o.Installments,
                                 TotalFee = o.TotalFee,
                                 ClaimTerms = o.ClaimTerms,
                                 CommissionPer = o.CommissionPer,
                                 IsPayable = o.IsPayable,
                                 AddInQuotation = o.AddInQuotation,
                                 Id = o.Id
                             },
                             FeeTypeName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                             ProductFeeName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                         });

            var productFeeDetailListDtos = await query.ToListAsync();

            return _productFeeDetailsExcelExporter.ExportToFile(productFeeDetailListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_ProductFeeDetails)]
        public async Task<List<ProductFeeDetailFeeTypeLookupTableDto>> GetAllFeeTypeForTableDropdown()
        {
            return await _lookup_feeTypeRepository.GetAll()
                .Select(feeType => new ProductFeeDetailFeeTypeLookupTableDto
                {
                    Id = feeType.Id,
                    DisplayName = feeType == null || feeType.Name == null ? "" : feeType.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ProductFeeDetails)]
        public async Task<List<ProductFeeDetailProductFeeLookupTableDto>> GetAllProductFeeForTableDropdown()
        {
            return await _lookup_productFeeRepository.GetAll()
                .Select(productFee => new ProductFeeDetailProductFeeLookupTableDto
                {
                    Id = productFee.Id,
                    DisplayName = productFee == null || productFee.Name == null ? "" : productFee.Name.ToString()
                }).ToListAsync();
        }

    }
}