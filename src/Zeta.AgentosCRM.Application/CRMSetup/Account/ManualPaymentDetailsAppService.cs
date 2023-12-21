using Abp.Organizations;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.Account.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore; 

namespace Zeta.AgentosCRM.CRMSetup.Account
{
    [AbpAuthorize(AppPermissions.Pages_CRMSetup_ManualPaymentDetails)]
    public class ManualPaymentDetailsAppService : AgentosCRMAppServiceBase, IManualPaymentDetailsAppService
    {
        private readonly IRepository<ManualPaymentDetail> _manualPaymentDetailRepository;
        private readonly IRepository<OrganizationUnit, long> _lookup_organizationUnitRepository;
        private readonly IRepository<PaymentInvoiceType> _PaymentInvoiceTypeRepository;

        public ManualPaymentDetailsAppService(IRepository<ManualPaymentDetail> manualPaymentDetailRepository,IRepository<OrganizationUnit, long> lookup_organizationUnitRepository,IRepository<PaymentInvoiceType> paymentInvoiceTypeRepository)
        {
            _manualPaymentDetailRepository = manualPaymentDetailRepository;
            _lookup_organizationUnitRepository = lookup_organizationUnitRepository;
            _PaymentInvoiceTypeRepository = paymentInvoiceTypeRepository;
            _manualPaymentDetailRepository = manualPaymentDetailRepository;
        }

        public async Task<PagedResultDto<GetManualPaymentDetailForViewDto>> GetAll(GetAllManualPaymentDetailsInput input)
        {

            var filteredManualPaymentDetails = _manualPaymentDetailRepository.GetAll()
                        .Include(e => e.OrganizationUnitFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.PaymentDetail.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PaymentDetailFilter), e => e.PaymentDetail.Contains(input.PaymentDetailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.OrganizationUnitFk != null && e.OrganizationUnitFk.DisplayName == input.OrganizationUnitDisplayNameFilter)
                         .WhereIf(input.OrganizationUnitIdFilter.HasValue, e => false || e.OrganizationUnitId == input.OrganizationUnitIdFilter.Value);

            var pagedAndFilteredManualPaymentDetails = filteredManualPaymentDetails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var manualPaymentDetails = from o in pagedAndFilteredManualPaymentDetails
                                       join o1 in _lookup_organizationUnitRepository.GetAll() on o.OrganizationUnitId equals o1.Id into j1
                                       from s1 in j1.DefaultIfEmpty()

                                       select new
                                       {

                                           o.Name,
                                           o.PaymentDetail,
                                           Id = o.Id,
                                           OrganizationUnitDisplayName = s1 == null || s1.DisplayName == null ? "" : s1.DisplayName.ToString()
                                       };

            var totalCount = await filteredManualPaymentDetails.CountAsync();

            var dbList = await manualPaymentDetails.ToListAsync();
            var results = new List<GetManualPaymentDetailForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetManualPaymentDetailForViewDto()
                {
                    ManualPaymentDetail = new ManualPaymentDetailDto
                    {

                        Name = o.Name,
                        PaymentDetail = o.PaymentDetail,
                        Id = o.Id,
                    },
                    OrganizationUnitDisplayName = o.OrganizationUnitDisplayName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetManualPaymentDetailForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetManualPaymentDetailForViewDto> GetManualPaymentDetailForView(int id)
        {
            var manualPaymentDetail = await _manualPaymentDetailRepository.GetAsync(id);

            var output = new GetManualPaymentDetailForViewDto { ManualPaymentDetail = ObjectMapper.Map<ManualPaymentDetailDto>(manualPaymentDetail) };

            if (output.ManualPaymentDetail.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.ManualPaymentDetail.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_ManualPaymentDetails_Edit)]
        public async Task<GetManualPaymentDetailForEditOutput> GetManualPaymentDetailForEdit(EntityDto input)
        {
            var manualPaymentDetail = await _manualPaymentDetailRepository.FirstOrDefaultAsync(input.Id);
            var ManualPaymentDetailId = await _PaymentInvoiceTypeRepository.GetAllListAsync(p => p.ManualPaymentDetailId == input.Id);

            var output = new GetManualPaymentDetailForEditOutput
            {
                ManualPaymentDetail = ObjectMapper.Map<CreateOrEditManualPaymentDetailDto>(manualPaymentDetail),
                PaymentInvoiceType = ObjectMapper.Map<List<CreateOrEditPaymentInvoiceTypeDto>>(ManualPaymentDetailId)
            };

            if (output.ManualPaymentDetail.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.ManualPaymentDetail.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditManualPaymentDetailDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_ManualPaymentDetails_Create)]
        protected virtual async Task Create(CreateOrEditManualPaymentDetailDto input)
        {
            var manualPaymentDetail = ObjectMapper.Map<ManualPaymentDetail>(input);

            if (AbpSession.TenantId != null)
            {
                manualPaymentDetail.TenantId = (int)AbpSession.TenantId;
            }

            var manualPaymentDetailId = _manualPaymentDetailRepository.InsertAndGetIdAsync(manualPaymentDetail).Result;
            foreach (var item in input.PaymentInvoiceTypeRecord)
            {
                item.InvoiceTypeId=item.InvoiceTypeId;
                item.ManualPaymentDetailId = manualPaymentDetailId;
                var stepEntity = ObjectMapper.Map<PaymentInvoiceType>(item);
                await _PaymentInvoiceTypeRepository.InsertAsync(stepEntity);

            }
            CurrentUnitOfWork.SaveChanges();
           // await _manualPaymentDetailRepository.InsertAsync(manualPaymentDetail);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_ManualPaymentDetails_Edit)]
        protected virtual async Task Update(CreateOrEditManualPaymentDetailDto input)
        {
            var manualPaymentDetail = await _manualPaymentDetailRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, manualPaymentDetail);
            var ManualPaymentid = await _PaymentInvoiceTypeRepository.GetAllListAsync(p => p.ManualPaymentDetailId == input.Id);

            foreach (var item in ManualPaymentid)
            {
                await _PaymentInvoiceTypeRepository.DeleteAsync(item.Id);
            }
            foreach (var PaymentInvoice in input.PaymentInvoiceTypeRecord)
            {
                PaymentInvoice.InvoiceTypeId = PaymentInvoice.InvoiceTypeId;
                PaymentInvoice.ManualPaymentDetailId = manualPaymentDetail.Id; 
                var stepEntity = ObjectMapper.Map<PaymentInvoiceType>(PaymentInvoice);
                await _PaymentInvoiceTypeRepository.InsertAsync(stepEntity);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_ManualPaymentDetails_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _manualPaymentDetailRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_CRMSetup_ManualPaymentDetails)]
        public async Task<List<ManualPaymentDetailOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown()
        {
            return await _lookup_organizationUnitRepository.GetAll()
                .Select(organizationUnit => new ManualPaymentDetailOrganizationUnitLookupTableDto
                {
                    Id = organizationUnit.Id,
                    DisplayName = organizationUnit == null || organizationUnit.DisplayName == null ? "" : organizationUnit.DisplayName.ToString()
                }).ToListAsync();
        }

    }
}