using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMSetup.CRMCurrency;

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
using Zeta.AgentosCRM.CRMAppointments;
using Zeta.AgentosCRM.CRMAppointments.Invitees;
using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.CRMAppointments.Invitees.Dtos;
using Zeta.AgentosCRM.CRMClient.Quotation.Dtos;
using Zeta.AgentosCRM.CRMSetup;

namespace Zeta.AgentosCRM.CRMClient.Quotation
{
    [AbpAuthorize(AppPermissions.Pages_ClientQuotationHeads)]
    public class ClientQuotationHeadsAppService : AgentosCRMAppServiceBase, IClientQuotationHeadsAppService
    {
        private readonly IRepository<ClientQuotationHead, long> _clientQuotationHeadRepository;
        private readonly IRepository<Client, long> _lookup_clientRepository;
        private readonly IRepository<CRMCurrency, int> _lookup_crmCurrencyRepository;
        private readonly IRepository<ClientQuotationDetail, long> _clientQuotationDetailRepository;
        public ClientQuotationHeadsAppService(IRepository<ClientQuotationHead, long> clientQuotationHeadRepository, IRepository<Client, long> lookup_clientRepository, IRepository<CRMCurrency, int> lookup_crmCurrencyRepository, IRepository<ClientQuotationDetail, long> clientQuotationDetailRepository)
        {
            _clientQuotationHeadRepository = clientQuotationHeadRepository;
            _lookup_clientRepository = lookup_clientRepository;
            _lookup_crmCurrencyRepository = lookup_crmCurrencyRepository;
            _clientQuotationDetailRepository = clientQuotationDetailRepository;
        }

        public async Task<PagedResultDto<GetClientQuotationHeadForViewDto>> GetAll(GetAllClientQuotationHeadsInput input)
        {

            var filteredClientQuotationHeads = _clientQuotationHeadRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .Include(e => e.CurrencyFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ClientEmail.Contains(input.Filter) || e.ClientName.Contains(input.Filter))
                        .WhereIf(input.MinDueDateFilter != null, e => e.DueDate >= input.MinDueDateFilter)
                        .WhereIf(input.MaxDueDateFilter != null, e => e.DueDate <= input.MaxDueDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientEmailFilter), e => e.ClientEmail.Contains(input.ClientEmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientNameFilter), e => e.ClientName.Contains(input.ClientNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientFirstNameFilter), e => e.ClientFk != null && e.ClientFk.FirstName == input.ClientFirstNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CRMCurrencyNameFilter), e => e.CurrencyFk != null && e.CurrencyFk.Name == input.CRMCurrencyNameFilter);

            var pagedAndFilteredClientQuotationHeads = filteredClientQuotationHeads
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var clientQuotationHeads = from o in pagedAndFilteredClientQuotationHeads
                                       join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                                       from s1 in j1.DefaultIfEmpty()

                                       join o2 in _lookup_crmCurrencyRepository.GetAll() on o.CurrencyId equals o2.Id into j2
                                       from s2 in j2.DefaultIfEmpty()

                                       select new
                                       {

                                           o.DueDate,
                                           o.ClientEmail,
                                           o.ClientName,
                                           Id = o.Id,
                                           ClientFirstName = s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString(),
                                           CRMCurrencyName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                                       };

            var totalCount = await filteredClientQuotationHeads.CountAsync();

            var dbList = await clientQuotationHeads.ToListAsync();
            var results = new List<GetClientQuotationHeadForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetClientQuotationHeadForViewDto()
                {
                    ClientQuotationHead = new ClientQuotationHeadDto
                    {

                        DueDate = o.DueDate,
                        ClientEmail = o.ClientEmail,
                        ClientName = o.ClientName,
                        Id = o.Id,
                    },
                    ClientFirstName = o.ClientFirstName,
                    CRMCurrencyName = o.CRMCurrencyName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetClientQuotationHeadForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetClientQuotationHeadForViewDto> GetClientQuotationHeadForView(long id)
        {
            var clientQuotationHead = await _clientQuotationHeadRepository.GetAsync(id);

            var output = new GetClientQuotationHeadForViewDto { ClientQuotationHead = ObjectMapper.Map<ClientQuotationHeadDto>(clientQuotationHead) };

            if (output.ClientQuotationHead.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.ClientQuotationHead.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            if (output.ClientQuotationHead.CurrencyId != null)
            {
                var _lookupCRMCurrency = await _lookup_crmCurrencyRepository.FirstOrDefaultAsync((int)output.ClientQuotationHead.CurrencyId);
                output.CRMCurrencyName = _lookupCRMCurrency?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ClientQuotationHeads_Edit)]
        public async Task<GetClientQuotationHeadForEditOutput> GetClientQuotationHeadForEdit(EntityDto<long> input)
        {
            var clientQuotationHead = await _clientQuotationHeadRepository.FirstOrDefaultAsync(input.Id);
			var clientQuotationDeatils = await _clientQuotationDetailRepository.GetAllListAsync(p => p.QuotationHeadId == input.Id);

			var output = new GetClientQuotationHeadForEditOutput { 
                ClientQuotationHead = ObjectMapper.Map<CreateOrEditClientQuotationHeadDto>(clientQuotationHead),

				 ClientQuotationDetail = ObjectMapper.Map<List<CreateOrEditClientQuotationDetailDto>>(clientQuotationDeatils)
			};

            if (output.ClientQuotationHead.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.ClientQuotationHead.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            if (output.ClientQuotationHead.CurrencyId != null)
            {
                var _lookupCRMCurrency = await _lookup_crmCurrencyRepository.FirstOrDefaultAsync((int)output.ClientQuotationHead.CurrencyId);
                output.CRMCurrencyName = _lookupCRMCurrency?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditClientQuotationHeadDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ClientQuotationHeads_Create)]
        protected virtual async Task Create([FromBody] CreateOrEditClientQuotationHeadDto input)
        {
            var clientQuotationHead = ObjectMapper.Map<ClientQuotationHead>(input);

            if (AbpSession.TenantId != null)
            {
                clientQuotationHead.TenantId = (int)AbpSession.TenantId;
            }
            var quotationheadId = _clientQuotationHeadRepository.InsertAndGetIdAsync(clientQuotationHead).Result;
            foreach (var step in input.QuotationDetails)
            {
                step.QuotationHeadId = quotationheadId;
                var stepEntity = ObjectMapper.Map<ClientQuotationDetail>(step);
                await _clientQuotationDetailRepository.InsertAsync(stepEntity);
            }
            CurrentUnitOfWork.SaveChanges();
           // await _clientQuotationHeadRepository.InsertAsync(clientQuotationHead);

        }

        [AbpAuthorize(AppPermissions.Pages_ClientQuotationHeads_Edit)]
        protected virtual async Task Update(CreateOrEditClientQuotationHeadDto input)
        {
            var clientQuotationHead = await _clientQuotationHeadRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, clientQuotationHead);
			 
			foreach (var quotation in input.QuotationDetails)
			{

				if (quotation.Id == 0)
				{
                    quotation.QuotationHeadId = (int)input.Id;
					var quotationDetail = ObjectMapper.Map<ClientQuotationDetail>(quotation);
					await _clientQuotationDetailRepository.InsertAsync(quotationDetail);
				}
				else
				{
					quotation.QuotationHeadId = (int)input.Id;
					var workflowStep = await _clientQuotationDetailRepository.FirstOrDefaultAsync((int)quotation.Id);
					ObjectMapper.Map(quotation, workflowStep);
				}
			}
			//CurrentUnitOfWork.SaveChanges();
		}

        [AbpAuthorize(AppPermissions.Pages_ClientQuotationHeads_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _clientQuotationHeadRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_ClientQuotationHeads)]
        public async Task<List<ClientQuotationHeadClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new ClientQuotationHeadClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = client == null || client.FirstName == null ? "" : client.FirstName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ClientQuotationHeads)]
        public async Task<List<ClientQuotationHeadCRMCurrencyLookupTableDto>> GetAllCRMCurrencyForTableDropdown()
        {
            return await _lookup_crmCurrencyRepository.GetAll()
                .Select(crmCurrency => new ClientQuotationHeadCRMCurrencyLookupTableDto
                {
                    Id = crmCurrency.Id,
                    DisplayName = crmCurrency == null || crmCurrency.Name == null ? "" : crmCurrency.Name.ToString()
                }).ToListAsync();
        }

    }
}