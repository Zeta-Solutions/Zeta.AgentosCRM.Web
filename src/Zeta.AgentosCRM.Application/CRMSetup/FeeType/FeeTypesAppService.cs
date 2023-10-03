using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.FeeType.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup.FeeType
{
    [AbpAuthorize(AppPermissions.Pages_FeeTypes)]
    public class FeeTypesAppService : AgentosCRMAppServiceBase, IFeeTypesAppService
    {
        private readonly IRepository<FeeType> _feeTypeRepository;

        public FeeTypesAppService(IRepository<FeeType> feeTypeRepository)
        {
            _feeTypeRepository = feeTypeRepository;

        }

        public async Task<PagedResultDto<GetFeeTypeForViewDto>> GetAll(GetAllFeeTypesInput input)
        {

            var filteredFeeTypes = _feeTypeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var pagedAndFilteredFeeTypes = filteredFeeTypes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var feeTypes = from o in pagedAndFilteredFeeTypes
                           select new
                           {

                               o.Abbrivation,
                               o.Name,
                               Id = o.Id
                           };

            var totalCount = await filteredFeeTypes.CountAsync();

            var dbList = await feeTypes.ToListAsync();
            var results = new List<GetFeeTypeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetFeeTypeForViewDto()
                {
                    FeeType = new FeeTypeDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetFeeTypeForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetFeeTypeForViewDto> GetFeeTypeForView(int id)
        {
            var feeType = await _feeTypeRepository.GetAsync(id);

            var output = new GetFeeTypeForViewDto { FeeType = ObjectMapper.Map<FeeTypeDto>(feeType) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_FeeTypes_Edit)]
        public async Task<GetFeeTypeForEditOutput> GetFeeTypeForEdit(EntityDto input)
        {
            var feeType = await _feeTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetFeeTypeForEditOutput { FeeType = ObjectMapper.Map<CreateOrEditFeeTypeDto>(feeType) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditFeeTypeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_FeeTypes_Create)]
        protected virtual async Task Create(CreateOrEditFeeTypeDto input)
        {
            var feeType = ObjectMapper.Map<FeeType>(input);

            if (AbpSession.TenantId != null)
            {
                feeType.TenantId = (int)AbpSession.TenantId;
            }

            await _feeTypeRepository.InsertAsync(feeType);

        }

        [AbpAuthorize(AppPermissions.Pages_FeeTypes_Edit)]
        protected virtual async Task Update(CreateOrEditFeeTypeDto input)
        {
            var feeType = await _feeTypeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, feeType);

        }

        [AbpAuthorize(AppPermissions.Pages_FeeTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _feeTypeRepository.DeleteAsync(input.Id);
        }

    }
}