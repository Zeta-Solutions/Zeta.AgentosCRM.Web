using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.CRMCurrency.Dtos; 
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup.CRMCurrency
{
    [AbpAuthorize(AppPermissions.Pages_CRMCurrencies)]
    public class CRMCurrenciesAppService : AgentosCRMAppServiceBase, ICRMCurrenciesAppService
    {
        private readonly IRepository<CRMCurrency> _crmCurrencyRepository;

        public CRMCurrenciesAppService(IRepository<CRMCurrency> crmCurrencyRepository)
        {
            _crmCurrencyRepository = crmCurrencyRepository;

        }

        public async Task<PagedResultDto<GetCRMCurrencyForViewDto>> GetAll(GetAllCRMCurrenciesInput input)
        {

            var filteredCRMCurrencies = _crmCurrencyRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var pagedAndFilteredCRMCurrencies = filteredCRMCurrencies
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var crmCurrencies = from o in pagedAndFilteredCRMCurrencies
                                select new
                                {

                                    o.Abbrivation,
                                    o.Name,
                                    Id = o.Id
                                };

            var totalCount = await filteredCRMCurrencies.CountAsync();

            var dbList = await crmCurrencies.ToListAsync();
            var results = new List<GetCRMCurrencyForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCRMCurrencyForViewDto()
                {
                    CRMCurrency = new CRMCurrencyDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCRMCurrencyForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_CRMCurrencies_Edit)]
        public async Task<GetCRMCurrencyForEditOutput> GetCRMCurrencyForEdit(EntityDto input)
        {
            var crmCurrency = await _crmCurrencyRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCRMCurrencyForEditOutput { CRMCurrency = ObjectMapper.Map<CreateOrEditCRMCurrencyDto>(crmCurrency) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCRMCurrencyDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CRMCurrencies_Create)]
        protected virtual async Task Create(CreateOrEditCRMCurrencyDto input)
        {
            var crmCurrency = ObjectMapper.Map<CRMCurrency>(input);

            if (AbpSession.TenantId != null)
            {
                crmCurrency.TenantId = (int)AbpSession.TenantId;
            }

            await _crmCurrencyRepository.InsertAsync(crmCurrency);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMCurrencies_Edit)]
        protected virtual async Task Update(CreateOrEditCRMCurrencyDto input)
        {
            var crmCurrency = await _crmCurrencyRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, crmCurrency);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMCurrencies_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _crmCurrencyRepository.DeleteAsync(input.Id);
        }

        public async Task<GetCRMCurrencyForViewDto> GetCRMCurrencyForView(int id)
        {
            var crmCurrency = await _crmCurrencyRepository.GetAsync(id);

            var output = new GetCRMCurrencyForViewDto { CRMCurrency = ObjectMapper.Map<CRMCurrencyDto>(crmCurrency) };

            return output;
        }
    }
}