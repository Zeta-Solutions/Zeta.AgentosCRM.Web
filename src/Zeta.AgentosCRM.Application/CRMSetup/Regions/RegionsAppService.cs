using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.Regions.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup.Regions
{
    [AbpAuthorize(AppPermissions.Pages_Regions)]
    public class RegionsAppService : AgentosCRMAppServiceBase, IRegionsAppService
    {
        private readonly IRepository<Region> _regionRepository;

        public RegionsAppService(IRepository<Region> regionRepository)
        {
            _regionRepository = regionRepository;

        }

        public async Task<PagedResultDto<GetRegionForViewDto>> GetAll(GetAllRegionsInput input)
        {

            var filteredRegions = _regionRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var pagedAndFilteredRegions = filteredRegions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var regions = from o in pagedAndFilteredRegions
                          select new
                          {

                              o.Abbrivation,
                              o.Name,
                              Id = o.Id
                          };

            var totalCount = await filteredRegions.CountAsync();

            var dbList = await regions.ToListAsync();
            var results = new List<GetRegionForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetRegionForViewDto()
                {
                    Region = new RegionDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetRegionForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetRegionForViewDto> GetRegionForView(int id)
        {
            var region = await _regionRepository.GetAsync(id);

            var output = new GetRegionForViewDto { Region = ObjectMapper.Map<RegionDto>(region) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Regions_Edit)]
        public async Task<GetRegionForEditOutput> GetRegionForEdit(EntityDto input)
        {
            var region = await _regionRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRegionForEditOutput { Region = ObjectMapper.Map<CreateOrEditRegionDto>(region) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditRegionDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Regions_Create)]
        protected virtual async Task Create(CreateOrEditRegionDto input)
        {
            var region = ObjectMapper.Map<Region>(input);

            if (AbpSession.TenantId != null)
            {
                region.TenantId = (int)AbpSession.TenantId;
            }

            await _regionRepository.InsertAsync(region);

        }

        [AbpAuthorize(AppPermissions.Pages_Regions_Edit)]
        protected virtual async Task Update(CreateOrEditRegionDto input)
        {
            var region = await _regionRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, region);

        }

        [AbpAuthorize(AppPermissions.Pages_Regions_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _regionRepository.DeleteAsync(input.Id);
        }

    }
}