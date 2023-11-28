﻿using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.CRMSetup.InstallmentType;
using Zeta.AgentosCRM.CRMSetup.FeeType;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMProducts.Fee.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore; 

namespace Zeta.AgentosCRM.CRMProducts.Fee
{
    [AbpAuthorize(AppPermissions.Pages_ProductFees)]
    public class ProductFeesAppService : AgentosCRMAppServiceBase, IProductFeesAppService
    {
        private readonly IRepository<ProductFee> _productFeeRepository;
        private readonly IRepository<Country, int> _lookup_countryRepository;
        private readonly IRepository<InstallmentType, int> _lookup_installmentTypeRepository;
        private readonly IRepository<FeeType, int> _lookup_feeTypeRepository;
        private readonly IRepository<Product, long> _lookup_productRepository;

        public ProductFeesAppService(IRepository<ProductFee> productFeeRepository, IRepository<Country, int> lookup_countryRepository, IRepository<InstallmentType, int> lookup_installmentTypeRepository, IRepository<FeeType, int> lookup_feeTypeRepository, IRepository<Product, long> lookup_productRepository)
        {
            _productFeeRepository = productFeeRepository;
            _lookup_countryRepository = lookup_countryRepository;
            _lookup_installmentTypeRepository = lookup_installmentTypeRepository;
            _lookup_feeTypeRepository = lookup_feeTypeRepository;
            _lookup_productRepository = lookup_productRepository;
        }

        public async Task<PagedResultDto<GetProductFeeForViewDto>> GetAll(GetAllProductFeesInput input)
        {

            var filteredProductFees = _productFeeRepository.GetAll()
                        .Include(e => e.CountryFk)
                        .Include(e => e.InstallmentTypeFk)
                        .Include(e => e.FeeTypeFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.ClaimTerms.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(input.MinInstallmentsFilter != null, e => e.Installments >= input.MinInstallmentsFilter)
                        .WhereIf(input.MaxInstallmentsFilter != null, e => e.Installments <= input.MaxInstallmentsFilter)
                        .WhereIf(input.MinInstallmentAmountFilter != null, e => e.InstallmentAmount >= input.MinInstallmentAmountFilter)
                        .WhereIf(input.MaxInstallmentAmountFilter != null, e => e.InstallmentAmount <= input.MaxInstallmentAmountFilter)
                        .WhereIf(input.MinTotalFeeFilter != null, e => e.TotalFee >= input.MinTotalFeeFilter)
                        .WhereIf(input.MaxTotalFeeFilter != null, e => e.TotalFee <= input.MaxTotalFeeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClaimTermsFilter), e => e.ClaimTerms.Contains(input.ClaimTermsFilter))
                        .WhereIf(input.MinCommissionPerFilter != null, e => e.CommissionPer >= input.MinCommissionPerFilter)
                        .WhereIf(input.MaxCommissionPerFilter != null, e => e.CommissionPer <= input.MaxCommissionPerFilter)
                        .WhereIf(input.AddInQuotationFilter.HasValue && input.AddInQuotationFilter > -1, e => (input.AddInQuotationFilter == 1 && e.AddInQuotation) || (input.AddInQuotationFilter == 0 && !e.AddInQuotation))
                        .WhereIf(input.MinNetTotalFilter != null, e => e.NetTotal >= input.MinNetTotalFilter)
                        .WhereIf(input.MaxNetTotalFilter != null, e => e.NetTotal <= input.MaxNetTotalFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CountryNameFilter), e => e.CountryFk != null && e.CountryFk.Name == input.CountryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.InstallmentTypeNameFilter), e => e.InstallmentTypeFk != null && e.InstallmentTypeFk.Name == input.InstallmentTypeNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FeeTypeNameFilter), e => e.FeeTypeFk != null && e.FeeTypeFk.Name == input.FeeTypeNameFilter);

            var pagedAndFilteredProductFees = filteredProductFees
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var productFees = from o in pagedAndFilteredProductFees
                              join o1 in _lookup_countryRepository.GetAll() on o.CountryId equals o1.Id into j1
                              from s1 in j1.DefaultIfEmpty()

                              join o2 in _lookup_installmentTypeRepository.GetAll() on o.InstallmentTypeId equals o2.Id into j2
                              from s2 in j2.DefaultIfEmpty()

                              join o3 in _lookup_feeTypeRepository.GetAll() on o.FeeTypeId equals o3.Id into j3
                              from s3 in j3.DefaultIfEmpty()

                              select new
                              {

                                  o.Name,
                                  o.Installments,
                                  o.InstallmentAmount,
                                  o.TotalFee,
                                  o.ClaimTerms,
                                  o.CommissionPer,
                                  o.AddInQuotation,
                                  o.NetTotal,
                                  Id = o.Id,
                                  CountryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                  InstallmentTypeName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                                  FeeTypeName = s3 == null || s3.Name == null ? "" : s3.Name.ToString()
                              };

            var totalCount = await filteredProductFees.CountAsync();

            var dbList = await productFees.ToListAsync();
            var results = new List<GetProductFeeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetProductFeeForViewDto()
                {
                    ProductFee = new ProductFeeDto
                    {

                        Name = o.Name,
                        Installments = o.Installments,
                        InstallmentAmount = o.InstallmentAmount,
                        TotalFee = o.TotalFee,
                        ClaimTerms = o.ClaimTerms,
                        CommissionPer = o.CommissionPer,
                        AddInQuotation = o.AddInQuotation,
                        NetTotal = o.NetTotal,
                        Id = o.Id,
                    },
                    CountryName = o.CountryName,
                    InstallmentTypeName = o.InstallmentTypeName,
                    FeeTypeName = o.FeeTypeName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetProductFeeForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetProductFeeForViewDto> GetProductFeeForView(int id)
        {
            var productFee = await _productFeeRepository.GetAsync(id);

            var output = new GetProductFeeForViewDto { ProductFee = ObjectMapper.Map<ProductFeeDto>(productFee) };

            if (output.ProductFee.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.ProductFee.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }

            if (output.ProductFee.InstallmentTypeId != null)
            {
                var _lookupInstallmentType = await _lookup_installmentTypeRepository.FirstOrDefaultAsync((int)output.ProductFee.InstallmentTypeId);
                output.InstallmentTypeName = _lookupInstallmentType?.Name?.ToString();
            }

            if (output.ProductFee.FeeTypeId != null)
            {
                var _lookupFeeType = await _lookup_feeTypeRepository.FirstOrDefaultAsync((int)output.ProductFee.FeeTypeId);
                output.FeeTypeName = _lookupFeeType?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ProductFees_Edit)]
        public async Task<GetProductFeeForEditOutput> GetProductFeeForEdit(EntityDto input)
        {
            var productFee = await _productFeeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetProductFeeForEditOutput { ProductFee = ObjectMapper.Map<CreateOrEditProductFeeDto>(productFee) };

            if (output.ProductFee.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.ProductFee.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }

            if (output.ProductFee.InstallmentTypeId != null)
            {
                var _lookupInstallmentType = await _lookup_installmentTypeRepository.FirstOrDefaultAsync((int)output.ProductFee.InstallmentTypeId);
                output.InstallmentTypeName = _lookupInstallmentType?.Name?.ToString();
            }

            if (output.ProductFee.FeeTypeId != null)
            {
                var _lookupFeeType = await _lookup_feeTypeRepository.FirstOrDefaultAsync((int)output.ProductFee.FeeTypeId);
                output.FeeTypeName = _lookupFeeType?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditProductFeeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ProductFees_Create)]
        protected virtual async Task Create(CreateOrEditProductFeeDto input)
        {
            var productFee = ObjectMapper.Map<ProductFee>(input);

            if (AbpSession.TenantId != null)
            {
                productFee.TenantId = (int?)AbpSession.TenantId;
            }

            await _productFeeRepository.InsertAsync(productFee);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductFees_Edit)]
        protected virtual async Task Update(CreateOrEditProductFeeDto input)
        {
            var productFee = await _productFeeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, productFee);

        }

        [AbpAuthorize(AppPermissions.Pages_ProductFees_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _productFeeRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_ProductFees)]
        public async Task<List<ProductFeeCountryLookupTableDto>> GetAllCountryForTableDropdown()
        {
            return await _lookup_countryRepository.GetAll()
                .Select(country => new ProductFeeCountryLookupTableDto
                {
                    Id = country.Id,
                    DisplayName = country == null || country.Name == null ? "" : country.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ProductFees)]
        public async Task<List<ProductFeeInstallmentTypeLookupTableDto>> GetAllInstallmentTypeForTableDropdown()
        {
            return await _lookup_installmentTypeRepository.GetAll()
                .Select(installmentType => new ProductFeeInstallmentTypeLookupTableDto
                {
                    Id = installmentType.Id,
                    DisplayName = installmentType == null || installmentType.Name == null ? "" : installmentType.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ProductFees)]
        public async Task<List<ProductFeeFeeTypeLookupTableDto>> GetAllFeeTypeForTableDropdown()
        {
            return await _lookup_feeTypeRepository.GetAll()
                .Select(feeType => new ProductFeeFeeTypeLookupTableDto
                {
                    Id = feeType.Id,
                    DisplayName = feeType == null || feeType.Name == null ? "" : feeType.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ProductFees)]
        public async Task<List<ProductFeeProductLookupTableDto>> GetAllProductForTableDropdown()
        {
            return await _lookup_productRepository.GetAll()
                .Select(product => new ProductFeeProductLookupTableDto
                {
                    Id = product.Id,
                    DisplayName = product == null || product.Name == null ? "" : product.Name.ToString()
                }).ToListAsync();
        }

    }
}