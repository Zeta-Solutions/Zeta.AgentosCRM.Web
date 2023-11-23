using Zeta.AgentosCRM.CRMSetup.Account;

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
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup.Account
{
    [AbpAuthorize(AppPermissions.Pages_PaymentInvoiceTypes)]
    public class PaymentInvoiceTypesAppService : AgentosCRMAppServiceBase, IPaymentInvoiceTypesAppService
    {
        private readonly IRepository<PaymentInvoiceType> _paymentInvoiceTypeRepository;
        private readonly IRepository<InvoiceType, int> _lookup_invoiceTypeRepository;
        private readonly IRepository<ManualPaymentDetail, int> _lookup_manualPaymentDetailRepository;

        public PaymentInvoiceTypesAppService(IRepository<PaymentInvoiceType> paymentInvoiceTypeRepository, IRepository<InvoiceType, int> lookup_invoiceTypeRepository, IRepository<ManualPaymentDetail, int> lookup_manualPaymentDetailRepository)
        {
            _paymentInvoiceTypeRepository = paymentInvoiceTypeRepository;
            _lookup_invoiceTypeRepository = lookup_invoiceTypeRepository;
            _lookup_manualPaymentDetailRepository = lookup_manualPaymentDetailRepository;

        }

        public async Task<PagedResultDto<GetPaymentInvoiceTypeForViewDto>> GetAll(GetAllPaymentInvoiceTypesInput input)
        {

            var filteredPaymentInvoiceTypes = _paymentInvoiceTypeRepository.GetAll()
                        .Include(e => e.InvoiceTypeFk)
                        .Include(e => e.ManualPaymentDetailFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.InvoiceTypeNameFilter), e => e.InvoiceTypeFk != null && e.InvoiceTypeFk.Name == input.InvoiceTypeNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ManualPaymentDetailNameFilter), e => e.ManualPaymentDetailFk != null && e.ManualPaymentDetailFk.Name == input.ManualPaymentDetailNameFilter);

            var pagedAndFilteredPaymentInvoiceTypes = filteredPaymentInvoiceTypes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var paymentInvoiceTypes = from o in pagedAndFilteredPaymentInvoiceTypes
                                      join o1 in _lookup_invoiceTypeRepository.GetAll() on o.InvoiceTypeId equals o1.Id into j1
                                      from s1 in j1.DefaultIfEmpty()

                                      join o2 in _lookup_manualPaymentDetailRepository.GetAll() on o.ManualPaymentDetailId equals o2.Id into j2
                                      from s2 in j2.DefaultIfEmpty()

                                      select new
                                      {

                                          o.Name,
                                          Id = o.Id,
                                          InvoiceTypeName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                          ManualPaymentDetailName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                                      };

            var totalCount = await filteredPaymentInvoiceTypes.CountAsync();

            var dbList = await paymentInvoiceTypes.ToListAsync();
            var results = new List<GetPaymentInvoiceTypeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPaymentInvoiceTypeForViewDto()
                {
                    PaymentInvoiceType = new PaymentInvoiceTypeDto
                    {

                        Name = o.Name,
                        Id = o.Id,
                    },
                    InvoiceTypeName = o.InvoiceTypeName,
                    ManualPaymentDetailName = o.ManualPaymentDetailName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPaymentInvoiceTypeForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetPaymentInvoiceTypeForViewDto> GetPaymentInvoiceTypeForView(int id)
        {
            var paymentInvoiceType = await _paymentInvoiceTypeRepository.GetAsync(id);

            var output = new GetPaymentInvoiceTypeForViewDto { PaymentInvoiceType = ObjectMapper.Map<PaymentInvoiceTypeDto>(paymentInvoiceType) };

            if (output.PaymentInvoiceType.InvoiceTypeId != null)
            {
                var _lookupInvoiceType = await _lookup_invoiceTypeRepository.FirstOrDefaultAsync((int)output.PaymentInvoiceType.InvoiceTypeId);
                output.InvoiceTypeName = _lookupInvoiceType?.Name?.ToString();
            }

            if (output.PaymentInvoiceType.ManualPaymentDetailId != null)
            {
                var _lookupManualPaymentDetail = await _lookup_manualPaymentDetailRepository.FirstOrDefaultAsync((int)output.PaymentInvoiceType.ManualPaymentDetailId);
                output.ManualPaymentDetailName = _lookupManualPaymentDetail?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_PaymentInvoiceTypes_Edit)]
        public async Task<GetPaymentInvoiceTypeForEditOutput> GetPaymentInvoiceTypeForEdit(EntityDto input)
        {
            var paymentInvoiceType = await _paymentInvoiceTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPaymentInvoiceTypeForEditOutput { PaymentInvoiceType = ObjectMapper.Map<CreateOrEditPaymentInvoiceTypeDto>(paymentInvoiceType) };

            if (output.PaymentInvoiceType.InvoiceTypeId != null)
            {
                var _lookupInvoiceType = await _lookup_invoiceTypeRepository.FirstOrDefaultAsync((int)output.PaymentInvoiceType.InvoiceTypeId);
                output.InvoiceTypeName = _lookupInvoiceType?.Name?.ToString();
            }

            if (output.PaymentInvoiceType.ManualPaymentDetailId != null)
            {
                var _lookupManualPaymentDetail = await _lookup_manualPaymentDetailRepository.FirstOrDefaultAsync((int)output.PaymentInvoiceType.ManualPaymentDetailId);
                output.ManualPaymentDetailName = _lookupManualPaymentDetail?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPaymentInvoiceTypeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_PaymentInvoiceTypes_Create)]
        protected virtual async Task Create(CreateOrEditPaymentInvoiceTypeDto input)
        {
            var paymentInvoiceType = ObjectMapper.Map<PaymentInvoiceType>(input);

            if (AbpSession.TenantId != null)
            {
                paymentInvoiceType.TenantId = (int)AbpSession.TenantId;
            }

            await _paymentInvoiceTypeRepository.InsertAsync(paymentInvoiceType);

        }

        [AbpAuthorize(AppPermissions.Pages_PaymentInvoiceTypes_Edit)]
        protected virtual async Task Update(CreateOrEditPaymentInvoiceTypeDto input)
        {
            var paymentInvoiceType = await _paymentInvoiceTypeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, paymentInvoiceType);

        }

        [AbpAuthorize(AppPermissions.Pages_PaymentInvoiceTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _paymentInvoiceTypeRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_PaymentInvoiceTypes)]
        public async Task<List<PaymentInvoiceTypeInvoiceTypeLookupTableDto>> GetAllInvoiceTypeForTableDropdown()
        {
            return await _lookup_invoiceTypeRepository.GetAll()
                .Select(invoiceType => new PaymentInvoiceTypeInvoiceTypeLookupTableDto
                {
                    Id = invoiceType.Id,
                    DisplayName = invoiceType == null || invoiceType.Name == null ? "" : invoiceType.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_PaymentInvoiceTypes)]
        public async Task<List<PaymentInvoiceTypeManualPaymentDetailLookupTableDto>> GetAllManualPaymentDetailForTableDropdown()
        {
            return await _lookup_manualPaymentDetailRepository.GetAll()
                .Select(manualPaymentDetail => new PaymentInvoiceTypeManualPaymentDetailLookupTableDto
                {
                    Id = manualPaymentDetail.Id,
                    DisplayName = manualPaymentDetail == null || manualPaymentDetail.Name == null ? "" : manualPaymentDetail.Name.ToString()
                }).ToListAsync();
        }

    }
}