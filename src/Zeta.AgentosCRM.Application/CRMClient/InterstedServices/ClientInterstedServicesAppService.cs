using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMPartner;
using Zeta.AgentosCRM.CRMProducts;
using Zeta.AgentosCRM.CRMPartner.PartnerBranch;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMClient.InterstedServices.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMClient.InterstedServices
{
    [AbpAuthorize(AppPermissions.Pages_ClientInterstedServices)]
    public class ClientInterstedServicesAppService : AgentosCRMAppServiceBase, IClientInterstedServicesAppService
    {
        private readonly IRepository<ClientInterstedService, long> _clientInterstedServiceRepository;
        private readonly IRepository<Client, long> _lookup_clientRepository;
        private readonly IRepository<Partner, long> _lookup_partnerRepository;
        private readonly IRepository<Product, long> _lookup_productRepository;
        private readonly IRepository<Branch, long> _lookup_branchRepository;

        public ClientInterstedServicesAppService(IRepository<ClientInterstedService, long> clientInterstedServiceRepository, IRepository<Client, long> lookup_clientRepository, IRepository<Partner, long> lookup_partnerRepository, IRepository<Product, long> lookup_productRepository, IRepository<Branch, long> lookup_branchRepository)
        {
            _clientInterstedServiceRepository = clientInterstedServiceRepository;
            _lookup_clientRepository = lookup_clientRepository;
            _lookup_partnerRepository = lookup_partnerRepository;
            _lookup_productRepository = lookup_productRepository;
            _lookup_branchRepository = lookup_branchRepository;

        }

        public async Task<PagedResultDto<GetClientInterstedServiceForViewDto>> GetAll(GetAllClientInterstedServicesInput input)
        {

            var filteredClientInterstedServices = _clientInterstedServiceRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .Include(e => e.PartnerFk)
                        .Include(e => e.ProductFk)
                        .Include(e => e.BranchFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(input.MinStartDateFilter != null, e => e.StartDate >= input.MinStartDateFilter)
                        .WhereIf(input.MaxStartDateFilter != null, e => e.StartDate <= input.MaxStartDateFilter)
                        .WhereIf(input.MinEndDateFilter != null, e => e.EndDate >= input.MinEndDateFilter)
                        .WhereIf(input.MaxEndDateFilter != null, e => e.EndDate <= input.MaxEndDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientFirstNameFilter), e => e.ClientFk != null && e.ClientFk.FirstName == input.ClientFirstNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PartnerPartnerNameFilter), e => e.PartnerFk != null && e.PartnerFk.PartnerName == input.PartnerPartnerNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProductNameFilter), e => e.ProductFk != null && e.ProductFk.Name == input.ProductNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BranchNameFilter), e => e.BranchFk != null && e.BranchFk.Name == input.BranchNameFilter);

            var pagedAndFilteredClientInterstedServices = filteredClientInterstedServices
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var clientInterstedServices = from o in pagedAndFilteredClientInterstedServices
                                          join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                                          from s1 in j1.DefaultIfEmpty()

                                          join o2 in _lookup_partnerRepository.GetAll() on o.PartnerId equals o2.Id into j2
                                          from s2 in j2.DefaultIfEmpty()

                                          join o3 in _lookup_productRepository.GetAll() on o.ProductId equals o3.Id into j3
                                          from s3 in j3.DefaultIfEmpty()

                                          join o4 in _lookup_branchRepository.GetAll() on o.BranchId equals o4.Id into j4
                                          from s4 in j4.DefaultIfEmpty()

                                          select new
                                          {

                                              o.Name,
                                              o.StartDate,
                                              o.EndDate,
                                              Id = o.Id,
                                              ClientFirstName = s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString(),
                                              PartnerPartnerName = s2 == null || s2.PartnerName == null ? "" : s2.PartnerName.ToString(),
                                              ProductName = s3 == null || s3.Name == null ? "" : s3.Name.ToString(),
                                              BranchName = s4 == null || s4.Name == null ? "" : s4.Name.ToString()
                                          };

            var totalCount = await filteredClientInterstedServices.CountAsync();

            var dbList = await clientInterstedServices.ToListAsync();
            var results = new List<GetClientInterstedServiceForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetClientInterstedServiceForViewDto()
                {
                    ClientInterstedService = new ClientInterstedServiceDto
                    {

                        Name = o.Name,
                        StartDate = o.StartDate,
                        EndDate = o.EndDate,
                        Id = o.Id,
                    },
                    ClientFirstName = o.ClientFirstName,
                    PartnerPartnerName = o.PartnerPartnerName,
                    ProductName = o.ProductName,
                    BranchName = o.BranchName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetClientInterstedServiceForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetClientInterstedServiceForViewDto> GetClientInterstedServiceForView(long id)
        {
            var clientInterstedService = await _clientInterstedServiceRepository.GetAsync(id);

            var output = new GetClientInterstedServiceForViewDto { ClientInterstedService = ObjectMapper.Map<ClientInterstedServiceDto>(clientInterstedService) };

            if (output.ClientInterstedService.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.ClientInterstedService.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            if (output.ClientInterstedService.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.ClientInterstedService.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.ClientInterstedService.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.ClientInterstedService.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            if (output.ClientInterstedService.BranchId != null)
            {
                var _lookupBranch = await _lookup_branchRepository.FirstOrDefaultAsync((long)output.ClientInterstedService.BranchId);
                output.BranchName = _lookupBranch?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ClientInterstedServices_Edit)]
        public async Task<GetClientInterstedServiceForEditOutput> GetClientInterstedServiceForEdit(EntityDto<long> input)
        {
            var clientInterstedService = await _clientInterstedServiceRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetClientInterstedServiceForEditOutput { ClientInterstedService = ObjectMapper.Map<CreateOrEditClientInterstedServiceDto>(clientInterstedService) };

            if (output.ClientInterstedService.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.ClientInterstedService.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            if (output.ClientInterstedService.PartnerId != null)
            {
                var _lookupPartner = await _lookup_partnerRepository.FirstOrDefaultAsync((long)output.ClientInterstedService.PartnerId);
                output.PartnerPartnerName = _lookupPartner?.PartnerName?.ToString();
            }

            if (output.ClientInterstedService.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((long)output.ClientInterstedService.ProductId);
                output.ProductName = _lookupProduct?.Name?.ToString();
            }

            if (output.ClientInterstedService.BranchId != null)
            {
                var _lookupBranch = await _lookup_branchRepository.FirstOrDefaultAsync((long)output.ClientInterstedService.BranchId);
                output.BranchName = _lookupBranch?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditClientInterstedServiceDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ClientInterstedServices_Create)]
        protected virtual async Task Create(CreateOrEditClientInterstedServiceDto input)
        {
            var clientInterstedService = ObjectMapper.Map<ClientInterstedService>(input);

            if (AbpSession.TenantId != null)
            {
                clientInterstedService.TenantId = (int)AbpSession.TenantId;
            }

            await _clientInterstedServiceRepository.InsertAsync(clientInterstedService);

        }

        [AbpAuthorize(AppPermissions.Pages_ClientInterstedServices_Edit)]
        protected virtual async Task Update(CreateOrEditClientInterstedServiceDto input)
        {
            var clientInterstedService = await _clientInterstedServiceRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, clientInterstedService);

        }

        [AbpAuthorize(AppPermissions.Pages_ClientInterstedServices_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _clientInterstedServiceRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_ClientInterstedServices)]
        public async Task<List<ClientInterstedServiceClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new ClientInterstedServiceClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = client == null || client.FirstName == null ? "" : client.FirstName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ClientInterstedServices)]
        public async Task<List<ClientInterstedServicePartnerLookupTableDto>> GetAllPartnerForTableDropdown()
        {
            return await _lookup_partnerRepository.GetAll()
                .Select(partner => new ClientInterstedServicePartnerLookupTableDto
                {
                    Id = partner.Id,
                    DisplayName = partner == null || partner.PartnerName == null ? "" : partner.PartnerName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ClientInterstedServices)]
        public async Task<List<ClientInterstedServiceProductLookupTableDto>> GetAllProductForTableDropdown()
        {
            return await _lookup_productRepository.GetAll()
                .Select(product => new ClientInterstedServiceProductLookupTableDto
                {
                    Id = product.Id,
                    DisplayName = product == null || product.Name == null ? "" : product.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ClientInterstedServices)]
        public async Task<List<ClientInterstedServiceBranchLookupTableDto>> GetAllBranchForTableDropdown()
        {
            return await _lookup_branchRepository.GetAll()
                .Select(branch => new ClientInterstedServiceBranchLookupTableDto
                {
                    Id = branch.Id,
                    DisplayName = branch == null || branch.Name == null ? "" : branch.Name.ToString()
                }).ToListAsync();
        }

    }
}