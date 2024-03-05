using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.CRMInvoice.Dtos;
using Zeta.AgentosCRM.CRMSetup.Account;
using Zeta.AgentosCRM.CRMSetup.CRMCurrency;
using SkiaSharp;
using System.Linq.Expressions;

namespace Zeta.AgentosCRM.CRMInvoice
{
    //[AbpAuthorize(AppPermissions.Pages_InvoiceHead)]
    public class InvoiceHeadAppService : AgentosCRMAppServiceBase, IInvoiceHeadAppService
    {
        private readonly IRepository<InvoiceHead, long> _invoiceHeadRepository;
        private readonly IRepository<InvoiceDetail, long> _invoiceDetailsRepository;
        private readonly IRepository<CRMCurrency, int> _lookup_CurrencyRepository;
        private readonly IRepository<ManualPaymentDetail, int> _lookup_ManualPaymentDetailRepository;
        private readonly IRepository<InvPaymentReceived, long> _invPaymentReceivedRepository;
        private readonly IRepository<InvIncomeSharing, long> _invIncomeSharingRepository;

        public InvoiceHeadAppService(IRepository<InvoiceHead, long> invoiceHeadRepository, IRepository<InvoiceDetail, long> invoiceDetailsRepository, IRepository<CRMCurrency, int> lookup_CurrencyRepository, IRepository<ManualPaymentDetail, int> lookup_ManualPaymentDetailRepository, IRepository<InvIncomeSharing, long> invIncomeSharingRepository, IRepository<InvPaymentReceived, long> invPaymentReceivedRepository)
        {
            _invoiceHeadRepository = invoiceHeadRepository;
            _invoiceDetailsRepository = invoiceDetailsRepository;
            _lookup_CurrencyRepository = lookup_CurrencyRepository;
            _lookup_ManualPaymentDetailRepository = lookup_ManualPaymentDetailRepository;
            _invIncomeSharingRepository = invIncomeSharingRepository;
            _invPaymentReceivedRepository = invPaymentReceivedRepository;
        }
        public async Task<PagedResultDto<GetInvoiceHeadForViewDto>> GetAll(GetAllInvoiceHeadInput input)
        {

            var filteredInvoiceHead = _invoiceHeadRepository.GetAll()
                         .Include(e => e.ManualPaymentDetailFk)
                        .Include(e => e.CurrencyFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ClientName.Contains(input.Filter) || e.ClientName.Contains(input.Filter));

            var pagedAndFilteredInvoiceHead = filteredInvoiceHead
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var InvoiceHead = from o in pagedAndFilteredInvoiceHead
                              join o1 in _lookup_CurrencyRepository.GetAll() on o.CurrencyId equals o1.Id into j1
                              from s1 in j1.DefaultIfEmpty()

                              join o2 in _lookup_ManualPaymentDetailRepository.GetAll() on o.ManualPaymentDetail equals o2.Id into j2
                              from s2 in j2.DefaultIfEmpty()
                              select new
                                {

                                  o.ClientId,
                                  o.PartnerId,
                                  Id = o.Id,
                                  o.ApplicationId,
                                  o.PartnerName,
                                  o.PartnerAddress,
                                  o.PartnerContact,
                                  o.PartnerService,
                                  o.ClientName,
                                  o.ClientDOB,
                                  o.PartnerClientId,
                                  o.Product,
                                  o.Branch,
                                  o.Workflow,
                                  o.InvoiceDate,
                                  o.InvoiceDueDate,
                                  o.DiscountGivenToClient,
                                  o.TotalFee,
                                  o.CommissionClaimed,
                                  o.Tax,
                                  o.NetFeePaidToPartner,
                                  o.NetFeeReceived,
                                  o.NetIncome,
                                  o.TotalPayables,
                                  o.TotalIncome,
                                  o.TotalAmount,
                                  o.TotalAmountInclTax,
                                  o.TotalPaid,
                                  o.TotalDue,
                                  o.InvoiceNo,
                                  o.Status,                                 
                                  o.ApplicationName,
                                  o.ClientAssignee,
                                  o.ApplicationOwner,
                                  o.TotalDetailCount,
                                  o.InvoiceType,
                                  o.InvoiceCreatedDate,
                                  o.InvoiceCreatedDateDet,
                                  o.TotalRevenue,
                                  o.ClientEmail,
                                  InvoiceCurrencyName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                  InvoiceManualPaymentDetailName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                              };

            var totalCount = await filteredInvoiceHead.CountAsync();

            var dbList = await InvoiceHead.ToListAsync();
            var results = new List<GetInvoiceHeadForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetInvoiceHeadForViewDto()
                {
                    InvoiceHead = new InvoiceHeadDto
                    {

                        ClientId = o.ClientId,
                        PartnerId = o.PartnerId,
                        Id = o.Id,
                        ApplicationId = o.ApplicationId,
                        PartnerName = o.PartnerName,
                        PartnerAddress = o.PartnerAddress,
                        PartnerContact = o.PartnerContact,
                        PartnerService = o.PartnerService,
                        ClientName = o.ClientName,
                        ClientDOB = o.ClientDOB,
                        PartnerClientId = o.PartnerClientId,
                        Product = o.Product,
                        Branch = o.Branch,
                        Workflow = o.Workflow,
                        InvoiceDate = o.InvoiceDate,
                        InvoiceDueDate = o.InvoiceDueDate,
                        DiscountGivenToClient = o.DiscountGivenToClient,
                        TotalFee = o.TotalFee,
                        CommissionClaimed = o.CommissionClaimed,
                        Tax = o.Tax,
                        NetFeePaidToPartner = o.NetFeePaidToPartner,
                        NetFeeReceived = o.NetFeeReceived,
                        NetIncome = o.NetIncome,
                        TotalPayables = o.TotalPayables,
                        TotalIncome = o.TotalIncome,
                        TotalAmount = o.TotalAmount,
                        TotalAmountInclTax = o.TotalAmountInclTax,
                        TotalPaid = o.TotalPaid,
                        TotalDue = o.TotalDue,
                        InvoiceNo = o.InvoiceNo,
                        Status = o.Status,
                        ApplicationName = o.ApplicationName,
                        ClientAssignee = o.ClientAssignee,
                        ApplicationOwner = o.ApplicationOwner,
                        TotalDetailCount = o.TotalDetailCount,
                        InvoiceType=o.InvoiceType,
                        InvoiceCreatedDateDet = o.InvoiceCreatedDateDet,
                        TotalRevenue = o.TotalRevenue,
                        ClientEmail = o.ClientEmail
                    },
                    InvoiceCurrencyName=o.InvoiceCurrencyName,
                    InvoiceManualPaymentDetailName=o.InvoiceManualPaymentDetailName,
                };

                results.Add(res);
            }

            return new PagedResultDto<GetInvoiceHeadForViewDto>(
                totalCount,
                results
            );

        }
        public async Task<GetInvoiceHeadForViewDto> GetInvoiceHeadForView(long id)
        {
            var invoiceHead = await _invoiceHeadRepository.GetAsync(id);

            var output = new GetInvoiceHeadForViewDto { InvoiceHead = ObjectMapper.Map<InvoiceHeadDto>(invoiceHead) };
            if (output.InvoiceHead.CurrencyId != null)
            {
                var _lookupPartner = await _lookup_CurrencyRepository.FirstOrDefaultAsync((int)output.InvoiceHead.CurrencyId);
                output.InvoiceCurrencyName = _lookupPartner?.Name?.ToString();
            }

            if (output.InvoiceHead.ManualPaymentDetail != null)
            {
                var _lookupPartner = await _lookup_ManualPaymentDetailRepository.FirstOrDefaultAsync((int)output.InvoiceHead.ManualPaymentDetail);
                output.InvoiceManualPaymentDetailName = _lookupPartner?.Name?.ToString();
            }
            return output;
        }
        //[AbpAuthorize(AppPermissions.Pages_InvoiceHead_Edit)]
        public async Task<GetInvoiceHeadForEditOutput> GetInvoiceHeadForEdit(EntityDto<long> input)
        {
            var InvoiceHead = await _invoiceHeadRepository.FirstOrDefaultAsync(input.Id);
            var InvoiceDeatils = await _invoiceDetailsRepository.GetAllListAsync(p => p.InvoiceHeadId == input.Id);
            var output = new GetInvoiceHeadForEditOutput
            {
                InvoiceHead = ObjectMapper.Map<CreateOrEditInvoiceHeadDto>(InvoiceHead),
                InvoiceDetail = ObjectMapper.Map<List<CreateOrEditInvoiceDetailDto>>(InvoiceDeatils)
            };

            if (output.InvoiceHead.CurrencyId != null)
            {
                var _lookupPartner = await _lookup_CurrencyRepository.FirstOrDefaultAsync((int)output.InvoiceHead.CurrencyId);
                output.InvoiceCurrencyName = _lookupPartner?.Name?.ToString();
            }

            if (output.InvoiceHead.ManualPaymentDetail != null)
            {
                var _lookupPartner = await _lookup_ManualPaymentDetailRepository.FirstOrDefaultAsync((int)output.InvoiceHead.ManualPaymentDetail);
                output.InvoiceManualPaymentDetailName = _lookupPartner?.Name?.ToString();
            }
            return output;
        }
        public async Task CreateOrEdit(CreateOrEditInvoiceHeadDto input)
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
        //[AbpAuthorize(AppPermissions.Pages_InvoiceHead_Create)]
        protected virtual async Task Create(CreateOrEditInvoiceHeadDto input)
        {
            var invoicehead = ObjectMapper.Map<InvoiceHead>(input);
            invoicehead.InvoiceNo = await GetInvoiceCode();

            if (AbpSession.TenantId != null)
            {
                invoicehead.TenantId = (int)AbpSession.TenantId;
            }
            var invoiceheadId = _invoiceHeadRepository.InsertAndGetIdAsync(invoicehead).Result;
            foreach (var step in input.InvoiceDetail)
            {
                step.InvoiceHeadId = invoiceheadId;
                var stepEntity = ObjectMapper.Map<InvoiceDetail>(step);
                await _invoiceDetailsRepository.InsertAsync(stepEntity);
            }
            foreach (var PaymentReceived in input.InvPaymentReceived)
            {
                PaymentReceived.InvoiceHeadId = invoiceheadId;
                var PaymentReceivedEntity = ObjectMapper.Map<InvPaymentReceived>(PaymentReceived);
                await _invPaymentReceivedRepository.InsertAsync(PaymentReceivedEntity);
            }
            foreach (var IncomeSharing in input.InvIncomeSharing)
            {
                IncomeSharing.InvoiceHeadId = invoiceheadId;
                var IncomeSharingEntity = ObjectMapper.Map<InvIncomeSharing>(IncomeSharing);
                await _invIncomeSharingRepository.InsertAsync(IncomeSharingEntity);
            }
            //var InvIncomeSharing = ObjectMapper.Map<InvIncomeSharing>(input.InvIncomeSharing);
            //InvIncomeSharing.InvoiceHeadId = invoiceheadId;
            //await _invIncomeSharingRepository.InsertAsync(InvIncomeSharing);

            CurrentUnitOfWork.SaveChanges();
            //await _leadHeadRepository.InsertAsync(lead);

        }
        private async Task<string> GetInvoiceCode()
        {
            var invoiceHead = await _invoiceHeadRepository.GetAll()
                                                .OrderByDescending(x => x.Id)
                                                .FirstOrDefaultAsync();
            var invoiceid = invoiceHead.Id;
            var formattedInvoiceId = invoiceHead.Id.ToString("D3");
            var InvoiceNo = "INV-" + formattedInvoiceId;

            return InvoiceNo; 
        }
        protected virtual async Task Update(CreateOrEditInvoiceHeadDto input)
        {
            var invoicehead = await _invoiceHeadRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, invoicehead);
            foreach (var invoice in input.InvoiceDetail)
            {

                if (invoice.Id == 0)
                {
                    invoice.InvoiceHeadId = (int)input.Id;
                    var invoiceDetail = ObjectMapper.Map<InvoiceDetail>(invoice);
                    await _invoiceDetailsRepository.InsertAsync(invoiceDetail);
                }
                else
                {
                    invoice.InvoiceHeadId = (int)input.Id;
                    var invoicedet = await _invoiceDetailsRepository.FirstOrDefaultAsync((int)invoice.Id);
                    ObjectMapper.Map(invoice, invoicedet);
                }
            }
            foreach (var PaymentReceived in input.InvPaymentReceived)
            {

                if (PaymentReceived.Id == 0)
                {
                    PaymentReceived.InvoiceHeadId = (int)input.Id;
                    var payment = ObjectMapper.Map<InvPaymentReceived>(PaymentReceived);
                    await _invPaymentReceivedRepository.InsertAsync(payment);
                }
                else
                {
                    PaymentReceived.InvoiceHeadId = (int)input.Id;
                    var paymentdet = await _invPaymentReceivedRepository.FirstOrDefaultAsync((int)PaymentReceived.Id);
                    ObjectMapper.Map(PaymentReceived, paymentdet);
                }
            }
            foreach (var invIncomeSharing in input.InvIncomeSharing)
            {

                if (invIncomeSharing.Id == 0)
                {
                    invIncomeSharing.InvoiceHeadId = (int)input.Id;
                    var incomeSharings = ObjectMapper.Map<InvIncomeSharing>(invIncomeSharing);
                    await _invIncomeSharingRepository.InsertAsync(incomeSharings);
                }
                else
                {
                    invIncomeSharing.InvoiceHeadId = (int)input.Id;
                    var Incomedet = await _invPaymentReceivedRepository.FirstOrDefaultAsync((int)invIncomeSharing.Id);
                    ObjectMapper.Map(invIncomeSharing, Incomedet);
                }
            }
            //var invIncomeSharing = input.InvIncomeSharing;
            //var incomeSharing = await _invIncomeSharingRepository.FirstOrDefaultAsync(x => x.InvoiceHeadId == input.Id);
            //ObjectMapper.Map(input.InvIncomeSharing, incomeSharing);

        }
        //[AbpAuthorize(AppPermissions.Pages_InvoiceHead_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _invoiceHeadRepository.DeleteAsync(input.Id);
        }
        //[AbpAuthorize(AppPermissions.Pages_InvoiceHead)]
        public async Task<List<InvoicePaymentLookupTableDto>> GetAllPaymentForTableDropdown()
        {
            return await _lookup_ManualPaymentDetailRepository.GetAll()
                .Select(payment => new InvoicePaymentLookupTableDto
                {
                    Id = payment.Id,
                    DisplayName = payment == null || payment.Name == null ? "" : payment.Name.ToString()
                }).ToListAsync();
        }
        //[AbpAuthorize(AppPermissions.Pages_InvoiceHead)]
        public async Task<List<InvoiceCurrencyLookupTableDto>> GetAllCurrencyForTableDropdown()
        {
            return await _lookup_CurrencyRepository.GetAll()
                .Select(currency => new InvoiceCurrencyLookupTableDto
                {
                    Id = currency.Id,
                    DisplayName = currency == null || currency.Name == null ? "" : currency.Name.ToString()
                }).ToListAsync();
        }
    }
}
