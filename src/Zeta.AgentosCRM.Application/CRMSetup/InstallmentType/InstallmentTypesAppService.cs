using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.InstallmentType.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup.InstallmentType
{
    [AbpAuthorize(AppPermissions.Pages_InstallmentTypes)]
    public class InstallmentTypesAppService : AgentosCRMAppServiceBase, IInstallmentTypesAppService
    {
        private readonly IRepository<InstallmentType> _installmentTypeRepository;

        public InstallmentTypesAppService(IRepository<InstallmentType> installmentTypeRepository)
        {
            _installmentTypeRepository = installmentTypeRepository;

        }

        public async Task<PagedResultDto<GetInstallmentTypeForViewDto>> GetAll(GetAllInstallmentTypesInput input)
        {

            var filteredInstallmentTypes = _installmentTypeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var pagedAndFilteredInstallmentTypes = filteredInstallmentTypes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var installmentTypes = from o in pagedAndFilteredInstallmentTypes
                                   select new
                                   {

                                       o.Abbrivation,
                                       o.Name,
                                       Id = o.Id
                                   };

            var totalCount = await filteredInstallmentTypes.CountAsync();

            var dbList = await installmentTypes.ToListAsync();
            var results = new List<GetInstallmentTypeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetInstallmentTypeForViewDto()
                {
                    InstallmentType = new InstallmentTypeDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetInstallmentTypeForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetInstallmentTypeForViewDto> GetInstallmentTypeForView(int id)
        {
            var installmentType = await _installmentTypeRepository.GetAsync(id);

            var output = new GetInstallmentTypeForViewDto { InstallmentType = ObjectMapper.Map<InstallmentTypeDto>(installmentType) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_InstallmentTypes_Edit)]
        public async Task<GetInstallmentTypeForEditOutput> GetInstallmentTypeForEdit(EntityDto input)
        {
            var installmentType = await _installmentTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetInstallmentTypeForEditOutput { InstallmentType = ObjectMapper.Map<CreateOrEditInstallmentTypeDto>(installmentType) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditInstallmentTypeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_InstallmentTypes_Create)]
        protected virtual async Task Create(CreateOrEditInstallmentTypeDto input)
        {
            var installmentType = ObjectMapper.Map<InstallmentType>(input);

            if (AbpSession.TenantId != null)
            {
                installmentType.TenantId = (int)AbpSession.TenantId;
            }

            await _installmentTypeRepository.InsertAsync(installmentType);

        }

        [AbpAuthorize(AppPermissions.Pages_InstallmentTypes_Edit)]
        protected virtual async Task Update(CreateOrEditInstallmentTypeDto input)
        {
            var installmentType = await _installmentTypeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, installmentType);

        }

        [AbpAuthorize(AppPermissions.Pages_InstallmentTypes_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _installmentTypeRepository.DeleteAsync(input.Id);
        }

    }
}