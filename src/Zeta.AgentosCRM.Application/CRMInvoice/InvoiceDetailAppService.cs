using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMInvoice.Dtos;
using Zeta.AgentosCRM.CRMLead;
using Microsoft.EntityFrameworkCore;
using Zeta.AgentosCRM.CRMSetup.Account;
using Zeta.AgentosCRM.CRMClient.Quotation;
using Zeta.AgentosCRM.CRMClient.Quotation.Dtos;

namespace Zeta.AgentosCRM.CRMInvoice
{
   
    public class InvoiceDetailAppService : AgentosCRMAppServiceBase, IInvoiceDetailsAppService
    {
        private readonly IRepository<InvoiceDetail, long> _invoiceDetailsRepository;
        private readonly IRepository<TaxSetting, int> _taxSettingRepository;
        private readonly IRepository<InvoiceType, int> _invoiceTypesRepository;

        public InvoiceDetailAppService(IRepository<InvoiceDetail, long> invoiceDetailsRepository, IRepository<TaxSetting, int> taxSettingRepository, IRepository<InvoiceType, int> invoiceTypesRepository)
        {
            _invoiceDetailsRepository = invoiceDetailsRepository;
            _taxSettingRepository = taxSettingRepository;
            _invoiceTypesRepository = invoiceTypesRepository;
        }
        public async Task<PagedResultDto<GetInvoiceDetailForViewDto>> GetAll(GetAllInvoiceDetailsInput input)
        {

            var filteredInvoiceDetail = _invoiceDetailsRepository.GetAll()
                .Include(e => e.TaxSettingFk)
                        .Include(e => e.InvoiceTypesFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Description.Contains(input.Filter) || e.Description.Contains(input.Filter));

            var pagedAndFilteredInvoiceDetail = filteredInvoiceDetail
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var InvoiceDetail = from o in pagedAndFilteredInvoiceDetail
                                join o1 in _taxSettingRepository.GetAll() on o.Tax equals o1.Id into j1
                                from s1 in j1.DefaultIfEmpty()

                                join o2 in _invoiceTypesRepository.GetAll() on o.IncomeType equals o2.Id into j2
                                from s2 in j2.DefaultIfEmpty()
                                select new
                              {

                                  o.Description,
                                  o.TotalFee,
                                  Id = o.Id,
                                  o.CommissionPercent,                                
                                  o.CommissionAmount,                                
                                  o.TaxAmount,                                
                                  o.NetAmount,                                
                                  o.Amount,                                
                                  o.InvoiceHeadId,
                                  InvoiceTaxName = s1 == null || s1.TaxCode == null ? "" : s1.TaxCode.ToString(),
                                  InvoiceIncomeTypeName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                                };

            var totalCount = await filteredInvoiceDetail.CountAsync();

            var dbList = await InvoiceDetail.ToListAsync();
            var results = new List<GetInvoiceDetailForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetInvoiceDetailForViewDto()
                {
                    InvoiceDetail = new InvoiceDetailDto
                    {

                        Description = o.Description,
                        TotalFee = o.TotalFee,
                        Id = o.Id,
                        CommissionPercent = o.CommissionPercent,
                        CommissionAmount = o.CommissionAmount,
                        TaxAmount = o.TaxAmount,
                        NetAmount = o.NetAmount,
                        Amount = o.Amount,
                        InvoiceHeadId = o.InvoiceHeadId,

                    },
                    InvoiceTaxName = o.InvoiceTaxName,
                    InvoiceIncomeTypeName = o.InvoiceIncomeTypeName,
                };

                results.Add(res);
            }

            return new PagedResultDto<GetInvoiceDetailForViewDto>(
                totalCount,
                results
            );

        }
        public async Task<GetInvoiceDetailForViewDto> GetInvoiceDetailForView(long id)
        {
            var invoiceDetail = await _invoiceDetailsRepository.GetAsync(id);

            var output = new GetInvoiceDetailForViewDto { InvoiceDetail = ObjectMapper.Map<InvoiceDetailDto>(invoiceDetail) };
            if (output.InvoiceDetail.Tax != null)
            {
                var _lookupPartner = await _taxSettingRepository.FirstOrDefaultAsync((int)output.InvoiceDetail.Tax);
                output.InvoiceTaxName = _lookupPartner?.TaxCode?.ToString();
            }

            if (output.InvoiceDetail.IncomeType != null)
            {
                var _lookupPartner = await _invoiceTypesRepository.FirstOrDefaultAsync((int)output.InvoiceDetail.IncomeType);
                output.InvoiceIncomeTypeName = _lookupPartner?.Name?.ToString();
            }
            return output;
        }
        [AbpAuthorize(AppPermissions.Pages_InvoiceDetails_Edit)]
        public async Task<GetInvoiceDetailForEditOutput> GetInvoiceDetailForEdit(EntityDto<long> input)
        {
            var InvoiceDetail = await _invoiceDetailsRepository.FirstOrDefaultAsync(input.Id);
            var output = new GetInvoiceDetailForEditOutput
            {
                InvoiceDetail = ObjectMapper.Map<CreateOrEditInvoiceDetailDto>(InvoiceDetail)
            };

            if (output.InvoiceDetail.Tax != null)
            {
                var _lookupPartner = await _taxSettingRepository.FirstOrDefaultAsync((int)output.InvoiceDetail.Tax);
                output.InvoiceTaxName = _lookupPartner?.TaxCode?.ToString();
            }

            if (output.InvoiceDetail.IncomeType != null)
            {
                var _lookupPartner = await _invoiceTypesRepository.FirstOrDefaultAsync((int)output.InvoiceDetail.IncomeType);
                output.InvoiceIncomeTypeName = _lookupPartner?.Name?.ToString();
            }
            return output;
        }
        public async Task CreateOrEdit(CreateOrEditInvoiceDetailDto input)
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
        [AbpAuthorize(AppPermissions.Pages_InvoiceDetails_Create)]
        protected virtual async Task Create(CreateOrEditInvoiceDetailDto input)
        {
            var invoiceDetail = ObjectMapper.Map<InvoiceDetail>(input);

            if (AbpSession.TenantId != null)
            {
                invoiceDetail.TenantId = (int)AbpSession.TenantId;
            }
            await _invoiceDetailsRepository.InsertAsync(invoiceDetail);

        }
        [AbpAuthorize(AppPermissions.Pages_InvoiceDetails_Edit)]
        protected virtual async Task Update(CreateOrEditInvoiceDetailDto input)
        {
            var invoiceDetail = await _invoiceDetailsRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, invoiceDetail);
        }
        //[AbpAuthorize(AppPermissions.Pages_InvoiceDetails_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _invoiceDetailsRepository.DeleteAsync(input.Id);
        }
        //[AbpAuthorize(AppPermissions.Pages_InvoiceDetails)]
        public async Task<List<InvoiceTaxLookupTableDto>> GetAllTaxForTableDropdown()
        {
            return await _taxSettingRepository.GetAll()
                .Select(tax => new InvoiceTaxLookupTableDto
                {
                    Id = tax.Id,
                    // DisplayName = tax == null || tax.TaxCode == null ? "" : tax.TaxCode.ToString()
                   DisplayName = (tax == null || tax.TaxCode == null ? "" : tax.TaxCode.ToString()) + "(" + (tax == null || tax.TaxRate == null ? "" : tax.TaxRate.ToString()) + "%)"


        }).ToListAsync();
        }
        //[AbpAuthorize(AppPermissions.Pages_InvoiceDetails)]
        public async Task<List<InvoiceIncomeTypeLookupTableDto>> GetAllIncomeTypeForTableDropdown()
        {
            return await _invoiceTypesRepository.GetAll()
                .Select(invoicetype => new InvoiceIncomeTypeLookupTableDto
                {
                    Id = invoicetype.Id,
                    DisplayName = invoicetype == null || invoicetype.Name == null ? "" : invoicetype.Name.ToString()
                }).ToListAsync();
        }
    }
}
