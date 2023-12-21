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
    [AbpAuthorize(AppPermissions.Pages_CRMSetup_InvoiceTypes)]
    public class InvoiceTypesAppService : AgentosCRMAppServiceBase, IInvoiceTypesAppService
    {
        private readonly IRepository<InvoiceType> _invoiceTypeRepository;

        public InvoiceTypesAppService(IRepository<InvoiceType> invoiceTypeRepository)
        {
            _invoiceTypeRepository = invoiceTypeRepository;

        }

        public async Task<PagedResultDto<GetInvoiceTypeForViewDto>> GetAll(GetAllInvoiceTypesInput input)
        {

            var filteredInvoiceTypes = _invoiceTypeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var pagedAndFilteredInvoiceTypes = filteredInvoiceTypes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var invoiceTypes = from o in pagedAndFilteredInvoiceTypes
                               select new
                               {

                                   o.Abbrivation,
                                   o.Name,
                                   Id = o.Id
                               };

            var totalCount = await filteredInvoiceTypes.CountAsync();

            var dbList = await invoiceTypes.ToListAsync();
            var results = new List<GetInvoiceTypeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetInvoiceTypeForViewDto()
                {
                    InvoiceType = new InvoiceTypeDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetInvoiceTypeForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetInvoiceTypeForViewDto> GetInvoiceTypeForView(int id)
        {
            var invoiceType = await _invoiceTypeRepository.GetAsync(id);

            var output = new GetInvoiceTypeForViewDto { InvoiceType = ObjectMapper.Map<InvoiceTypeDto>(invoiceType) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_InvoiceTypes_Edit)]
        public async Task<GetInvoiceTypeForEditOutput> GetInvoiceTypeForEdit(EntityDto input)
        {
            var invoiceType = await _invoiceTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetInvoiceTypeForEditOutput { InvoiceType = ObjectMapper.Map<CreateOrEditInvoiceTypeDto>(invoiceType) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditInvoiceTypeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_InvoiceTypes_Create)]
        protected virtual async Task Create(CreateOrEditInvoiceTypeDto input)
        {
            var invoiceType = ObjectMapper.Map<InvoiceType>(input);

            if (AbpSession.TenantId != null)
            {
                invoiceType.TenantId = (int)AbpSession.TenantId;
            }

            await _invoiceTypeRepository.InsertAsync(invoiceType);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_InvoiceTypes_Edit)]
        protected virtual async Task Update(CreateOrEditInvoiceTypeDto input)
        {
            var invoiceType = await _invoiceTypeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, invoiceType);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMSetup_InvoiceTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _invoiceTypeRepository.DeleteAsync(input.Id);
        }

    }
}