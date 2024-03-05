using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMInvoice.Dtos;
using Zeta.AgentosCRM.CRMInvoice.Dtos.InvIncomeSharing;
using Zeta.AgentosCRM.CRMSetup.Account;
using Abp.Organizations;
using Abp.Authorization;
using Zeta.AgentosCRM.Authorization;

namespace Zeta.AgentosCRM.CRMInvoice
{
    public class InvIncomeSharingAppService : AgentosCRMAppServiceBase, IInvIncomeSharingAppService
    {
        private readonly IRepository<InvIncomeSharing, long> _invIncomeSharingRepository;
        private readonly IRepository<InvoiceHead, long> _invoiceHeadRepository;
        private readonly IRepository<TaxSetting, int> _taxSettingRepository;
        private readonly IRepository<OrganizationUnit, long> _lookup_organizationUnitRepository;
        public InvIncomeSharingAppService(IRepository<InvIncomeSharing, long> invIncomeSharingRepository, IRepository<InvoiceHead, long> invoiceHeadRepository, IRepository<OrganizationUnit, long> lookup_organizationUnitRepository)
        {
            _invIncomeSharingRepository = invIncomeSharingRepository;
            _invoiceHeadRepository = invoiceHeadRepository;
            _lookup_organizationUnitRepository = lookup_organizationUnitRepository;
        }
        public async Task<PagedResultDto<GetInvIncomeSharingForViewDto>> GetAll(GetAllInvIncomeSharingInput input)
        {

            var filteredinvIncomeSharing = _invIncomeSharingRepository.GetAll()
                .Include(e => e.TaxSettingFk)
                        .Include(e => e.OrganizationUnitFk);

            var pagedAndfilteredinvIncomeSharing = filteredinvIncomeSharing
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var InvIncomeSharing = from o in pagedAndfilteredinvIncomeSharing
                                   join o1 in _taxSettingRepository.GetAll() on o.Tax equals o1.Id into j1
                                   from s1 in j1.DefaultIfEmpty()

                                   join o2 in _lookup_organizationUnitRepository.GetAll() on o.OrganizationUnitId equals o2.Id into j2
                                   from s2 in j2.DefaultIfEmpty()
                                   select new
                                   {

                                       o.InvoiceHeadId,
                                       o.PaymentsReceivedId,
                                       Id = o.Id,
                                       o.IncomeSharing,
                                       o.IsTax,
                                       o.Tax,
                                       o.TaxAmount,
                                       o.TotalIncludingTax,
                                       o.OrganizationUnitId,
                                       InvoiceTaxName = s1 == null || s1.TaxCode == null ? "" : s1.TaxCode.ToString(),
                                       OrganizationUnitDisplayName = s2 == null || s2.DisplayName == null ? "" : s2.DisplayName.ToString()
                                   };

            var totalCount = await filteredinvIncomeSharing.CountAsync();

            var dbList = await InvIncomeSharing.ToListAsync();
            var results = new List<GetInvIncomeSharingForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetInvIncomeSharingForViewDto()
                {
                    InvIncomeSharing = new InvIncomeSharingDto
                    {

                        InvoiceHeadId = o.InvoiceHeadId,
                        PaymentsReceivedId = o.PaymentsReceivedId,
                        Id = o.Id,
                        IncomeSharing = o.IncomeSharing,
                        IsTax = o.IsTax,
                        Tax = o.Tax,
                        TaxAmount = o.TaxAmount,
                        TotalIncludingTax = o.TotalIncludingTax,
                        OrganizationUnitId = o.OrganizationUnitId,

                    },
                    InvoiceTaxName = o.InvoiceTaxName,
                    OrganizationUnitDisplayName = o.OrganizationUnitDisplayName,
                };

                results.Add(res);
            }

            return new PagedResultDto<GetInvIncomeSharingForViewDto>(
                totalCount,
                results
            );

        }
        public async Task<GetInvIncomeSharingForViewDto> GetInvIncomeSharingForView(long id)
        {
            var incomeSharing = await _invIncomeSharingRepository.GetAsync(id);

            var output = new GetInvIncomeSharingForViewDto { InvIncomeSharing = ObjectMapper.Map<InvIncomeSharingDto>(incomeSharing) };
            if (output.InvIncomeSharing.Tax != null)
            {
                var _lookupPartner = await _taxSettingRepository.FirstOrDefaultAsync((int)output.InvIncomeSharing.Tax);
                output.InvoiceTaxName = _lookupPartner?.TaxCode?.ToString();
            }

            if (output.InvIncomeSharing.OrganizationUnitId != null)
            {
                var _lookupPartner = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((int)output.InvIncomeSharing.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupPartner?.DisplayName?.ToString();
            }
            return output;
        }
        public async Task<GetInvIncomeSharingForEditOutput> GetInvIncomeSharingForEdit(EntityDto<long> input)
        {
            var InvIncomeSharing = await _invIncomeSharingRepository.FirstOrDefaultAsync(input.Id);
            var output = new GetInvIncomeSharingForEditOutput
            {
                InvIncomeSharing = ObjectMapper.Map<CreateOrEditInvIncomeSharingDto>(InvIncomeSharing)
            };
            if (output.InvIncomeSharing.Tax != null)
            {
                var _lookupPartner = await _taxSettingRepository.FirstOrDefaultAsync((int)output.InvIncomeSharing.Tax);
                output.InvoiceTaxName = _lookupPartner?.TaxCode?.ToString();
            }

            if (output.InvIncomeSharing.OrganizationUnitId != null)
            {
                var _lookupPartner = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((int)output.InvIncomeSharing.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupPartner?.DisplayName?.ToString();
            }
            return output;
        }
        public async Task CreateOrEdit(CreateOrEditInvIncomeSharingDto input)
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
        protected virtual async Task Create(CreateOrEditInvIncomeSharingDto input)
        {
            var InvIncomeSharing = ObjectMapper.Map<InvIncomeSharing>(input);

            if (AbpSession.TenantId != null)
            {
                InvIncomeSharing.TenantId = (int)AbpSession.TenantId;
            }
            await _invIncomeSharingRepository.InsertAsync(InvIncomeSharing);

        }
        protected virtual async Task Update(CreateOrEditInvIncomeSharingDto input)
        {
            var invoiceDetail = await _invIncomeSharingRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, invoiceDetail);
        }
        public async Task Delete(EntityDto<long> input)
        {
            await _invIncomeSharingRepository.DeleteAsync(input.Id);
        }
    }
}
