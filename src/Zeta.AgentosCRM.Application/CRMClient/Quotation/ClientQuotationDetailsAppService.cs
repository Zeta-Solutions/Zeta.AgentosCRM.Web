using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;
using Zeta.AgentosCRM.CRMProducts;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMClient.Quotation.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMClient.Quotation
{
    [AbpAuthorize(AppPermissions.Pages_ClientQuotationDetails)]
    public class ClientQuotationDetailsAppService : AgentosCRMAppServiceBase, IClientQuotationDetailsAppService
    {
        private readonly IRepository<ClientQuotationDetail, long> _clientQuotationDetailRepository;
        private readonly IRepository<Workflow, int> _lookup_workflowRepository;
        private readonly IRepository<Partner, long> _lookup_partnerRepository;
        private readonly IRepository<Branch, long> _lookup_branchRepository;
        private readonly IRepository<Product, long> _lookup_productRepository;
        private readonly IRepository<ClientQuotationHead, long> _lookup_clientQuotationHeadRepository;

        public ClientQuotationDetailsAppService(IRepository<ClientQuotationDetail, long> clientQuotationDetailRepository, IRepository<Workflow, int> lookup_workflowRepository, IRepository<Partner, long> lookup_partnerRepository, IRepository<Branch, long> lookup_branchRepository, IRepository<Product, long> lookup_productRepository, IRepository<ClientQuotationHead, long> lookup_clientQuotationHeadRepository)
        {
            _clientQuotationDetailRepository = clientQuotationDetailRepository;
            _lookup_workflowRepository = lookup_workflowRepository;
            _lookup_partnerRepository = lookup_partnerRepository;
            _lookup_branchRepository = lookup_branchRepository;
            _lookup_productRepository = lookup_productRepository;
            _lookup_clientQuotationHeadRepository = lookup_clientQuotationHeadRepository;

        }

        public async Task<PagedResultDto<GetClientQuotationDetailForViewDto>> GetAll(GetAllClientQuotationDetailsInput input)
        {

            var filteredClientQuotationDetails = _clientQuotationDetailRepository.GetAll()
                        .Include(e => e.WorkflowFk)
                        .Include(e => e.PartnerFk)
                        .Include(e => e.BranchFk)
                        .Include(e => e.ProductFk)
                        .Include(e => e.QuotationHeadFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description.Contains(input.DescriptionFilter))
                        .WhereIf(input.MinServiceFeeFilter != null, e => e.ServiceFee >= input.MinServiceFeeFilter)
                        .WhereIf(input.MaxServiceFeeFilter != null, e => e.ServiceFee <= input.MaxServiceFeeFilter)
                        .WhereIf(input.MinDiscountFilter != null, e => e.Discount >= input.MinDiscountFilter)
                        .WhereIf(input.MaxDiscountFilter != null, e => e.Discount <= input.MaxDiscountFilter)
                        .WhereIf(input.MinNetFeeFilter != null, e => e.NetFee >= input.MinNetFeeFilter)
                        .WhereIf(input.MaxNetFeeFilter != null, e => e.NetFee <= input.MaxNetFeeFilter)
                        .WhereIf(input.MinExchangeRateFilter != null, e => e.ExchangeRate >= input.MinExchangeRateFilter)
                        .WhereIf(input.MaxExchangeRateFilter != null, e => e.ExchangeRate <= input.MaxExchangeRateFilter)
                        .WhereIf(input.MinTotalAmountFilter != null, e => e.TotalAmount >= input.MinTotalAmountFilter)
                        .WhereIf(input.MaxTotalAmountFilter != null, e => e.TotalAmount <= input.MaxTotalAmountFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WorkflowNameFilter), e => e.WorkflowFk != null && e.WorkflowFk.Name == input.WorkflowNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BranchNameFilter), e => e.BranchFk != null && e.BranchFk.Name == input.BranchNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProductNameFilter), e => e.ProductFk != null && e.ProductFk.Name == input.ProductNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientQuotationHeadClientNameFilter), e => e.QuotationHeadFk != null && e.QuotationHeadFk.ClientName == input.ClientQuotationHeadClientNameFilter);

            var pagedAndFilteredClientQuotationDetails = filteredClientQuotationDetails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var clientQuotationDetails = from o in pagedAndFilteredClientQuotationDetails
                                         join o1 in _lookup_workflowRepository.GetAll() on o.WorkflowId equals o1.Id into j1
                                         from s1 in j1.DefaultIfEmpty()

                                         join o2 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o2.Id into j2
                                         from s2 in j2.DefaultIfEmpty()

                                         join o3 in _lookup_branchRepository.GetAll() on o.BranchId equals o3.Id into j3
                                         from s3 in j3.DefaultIfEmpty()

                                         join o4 in _lookup_productRepository.GetAll() on o.ProductId equals o4.Id into j4
                                         from s4 in j4.DefaultIfEmpty()

                                         join o5 in _lookup_clientQuotationHeadRepository.GetAll() on o.QuotationHeadId equals o5.Id into j5
                                         from s5 in j5.DefaultIfEmpty()

                                         select new
                                         {

                                             o.Description,
                                             o.ServiceFee,
                                             o.Discount,
                                             o.NetFee,
                                             o.ExchangeRate,
                                             o.TotalAmount,
                                             o.Id,
                                             WorkflowName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                             PartnerPartnerName = s2 == null || s2.PartnerName == null ? "" : s2.PartnerName.ToString(),
                                             BranchName = s3 == null || s3.Name == null ? "" : s3.Name.ToString(),
                                             ProductName = s4 == null || s4.Name == null ? "" : s4.Name.ToString(),
                                             ClientQuotationHeadClientName = s5 == null || s5.ClientName == null ? "" : s5.ClientName.ToString()
                                         };

            var totalCount = await filteredClientQuotationDetails.CountAsync();

            var dbList = await clientQuotationDetails.ToListAsync();
            var results = new List<GetClientQuotationDetailForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetClientQuotationDetailForViewDto()
                {
                    ClientQuotationDetail = new ClientQuotationDetailDto
                    {

                        Description = o.Description,
                        ServiceFee = o.ServiceFee,
                        Discount = o.Discount,
                        NetFee = o.NetFee,
                        ExchangeRate = o.ExchangeRate,
                        TotalAmount = o.TotalAmount,
                        Id = o.Id,
                    },
                    WorkflowName = o.WorkflowName,
                    PartnerPartnerName = o.PartnerPartnerName,
                    BranchName = o.BranchName,
                    ProductName = o.ProductName,
                    ClientQuotationHeadClientName = o.ClientQuotationHeadClientName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetClientQuotationDetailForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetClientQuotationDetailForViewDto> GetClientQuotationDetailForView(long id)
        {
            var clientQuotationDetail = await _clientQuotationDetailRepository.GetAsync(id);

            var output = new GetClientQuotationDetailForViewDto { ClientQuotationDetail = ObjectMapper.Map<ClientQuotationDetailDto>(clientQuotationDetail) };

            if (output.ClientQuotationDetail.WorkflowId != null)
            {
                var _lookupWorkflow = await _lookup_workflowRepository.FirstOrDefaultAsync(output.ClientQuotationDetail.WorkflowId);
                output.WorkflowName = _lookupWorkflow?.Name?.ToString();
            }

            if (output.ClientQuotationDetail.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync(output.ClientQuotationDetail.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.ClientQuotationDetail.BranchId != null)
            {
                var _lookupBranch = await _lookup_branchRepository.FirstOrDefaultAsync(output.ClientQuotationDetail.BranchId);
                output.BranchName = _lookupBranch?.Name?.ToString();
            }

            if (output.ClientQuotationDetail.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync(output.ClientQuotationDetail.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            if (output.ClientQuotationDetail.QuotationHeadId != null)
            {
                var _lookupClientQuotationHead = await _lookup_clientQuotationHeadRepository.FirstOrDefaultAsync(output.ClientQuotationDetail.QuotationHeadId);
                output.ClientQuotationHeadClientName = _lookupClientQuotationHead?.ClientName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ClientQuotationDetails_Edit)]
        public async Task<GetClientQuotationDetailForEditOutput> GetClientQuotationDetailForEdit(EntityDto<long> input)
        {
            var clientQuotationDetail = await _clientQuotationDetailRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetClientQuotationDetailForEditOutput { ClientQuotationDetail = ObjectMapper.Map<CreateOrEditClientQuotationDetailDto>(clientQuotationDetail) };

            if (output.ClientQuotationDetail.WorkflowId != null)
            {
                var _lookupWorkflow = await _lookup_workflowRepository.FirstOrDefaultAsync(output.ClientQuotationDetail.WorkflowId);
                output.WorkflowName = _lookupWorkflow?.Name?.ToString();
            }

            if (output.ClientQuotationDetail.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync(output.ClientQuotationDetail.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.ClientQuotationDetail.BranchId != null)
            {
                var _lookupBranch = await _lookup_branchRepository.FirstOrDefaultAsync(output.ClientQuotationDetail.BranchId);
                output.BranchName = _lookupBranch?.Name?.ToString();
            }

            if (output.ClientQuotationDetail.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync(output.ClientQuotationDetail.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            if (output.ClientQuotationDetail.QuotationHeadId != null)
            {
                var _lookupClientQuotationHead = await _lookup_clientQuotationHeadRepository.FirstOrDefaultAsync(output.ClientQuotationDetail.QuotationHeadId);
                output.ClientQuotationHeadClientName = _lookupClientQuotationHead?.ClientName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditClientQuotationDetailDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ClientQuotationDetails_Create)]
        protected virtual async Task Create(CreateOrEditClientQuotationDetailDto input)
        {
            var clientQuotationDetail = ObjectMapper.Map<ClientQuotationDetail>(input);

            if (AbpSession.TenantId != null)
            {
                clientQuotationDetail.TenantId = (int)AbpSession.TenantId;
            }

            await _clientQuotationDetailRepository.InsertAsync(clientQuotationDetail);

        }

        [AbpAuthorize(AppPermissions.Pages_ClientQuotationDetails_Edit)]
        protected virtual async Task Update(CreateOrEditClientQuotationDetailDto input)
        {
            var clientQuotationDetail = await _clientQuotationDetailRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, clientQuotationDetail);

        }

        [AbpAuthorize(AppPermissions.Pages_ClientQuotationDetails_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _clientQuotationDetailRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_ClientQuotationDetails)]
        public async Task<List<ClientQuotationDetailWorkflowLookupTableDto>> GetAllWorkflowForTableDropdown()
        {
            return await _lookup_workflowRepository.GetAll()
                .Select(workflow => new ClientQuotationDetailWorkflowLookupTableDto
                {
                    Id = workflow.Id,
                    DisplayName = workflow == null || workflow.Name == null ? "" : workflow.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ClientQuotationDetails)]
        public async Task<List<ClientQuotationDetailPartnerLookupTableDto>> GetAllPartnerForTableDropdown()
        {
            return await _lookup_partnerRepository.GetAll()
                .Select(partner => new ClientQuotationDetailPartnerLookupTableDto
                {
                    Id = partner.Id,
                    DisplayName = partner == null || partner.PartnerName == null ? "" : partner.PartnerName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ClientQuotationDetails)]
        public async Task<List<ClientQuotationDetailBranchLookupTableDto>> GetAllBranchForTableDropdown()
        {
            return await _lookup_branchRepository.GetAll()
                .Select(branch => new ClientQuotationDetailBranchLookupTableDto
                {
                    Id = branch.Id,
                    DisplayName = branch == null || branch.Name == null ? "" : branch.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ClientQuotationDetails)]
        public async Task<List<ClientQuotationDetailProductLookupTableDto>> GetAllProductForTableDropdown()
        {
            return await _lookup_productRepository.GetAll()
                .Select(product => new ClientQuotationDetailProductLookupTableDto
                {
                    Id = product.Id,
                    DisplayName = product == null || product.Name == null ? "" : product.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ClientQuotationDetails)]
        public async Task<List<ClientQuotationDetailClientQuotationHeadLookupTableDto>> GetAllClientQuotationHeadForTableDropdown()
        {
            return await _lookup_clientQuotationHeadRepository.GetAll()
                .Select(clientQuotationHead => new ClientQuotationDetailClientQuotationHeadLookupTableDto
                {
                    Id = clientQuotationHead.Id,
                    DisplayName = clientQuotationHead == null || clientQuotationHead.ClientName == null ? "" : clientQuotationHead.ClientName.ToString()
                }).ToListAsync();
        }

    }
}