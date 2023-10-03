using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup
{
    [AbpAuthorize(AppPermissions.Pages_DegreeLevels)]
    public class DegreeLevelsAppService : AgentosCRMAppServiceBase, IDegreeLevelsAppService
    {
        private readonly IRepository<DegreeLevel> _degreeLevelRepository;

        public DegreeLevelsAppService(IRepository<DegreeLevel> degreeLevelRepository)
        {
            _degreeLevelRepository = degreeLevelRepository;

        }

        public async Task<PagedResultDto<GetDegreeLevelForViewDto>> GetAll(GetAllDegreeLevelsInput input)
        {

            var filteredDegreeLevels = _degreeLevelRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var pagedAndFilteredDegreeLevels = filteredDegreeLevels
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var degreeLevels = from o in pagedAndFilteredDegreeLevels
                               select new
                               {

                                   o.Abbrivation,
                                   o.Name,
                                   Id = o.Id
                               };

            var totalCount = await filteredDegreeLevels.CountAsync();

            var dbList = await degreeLevels.ToListAsync();
            var results = new List<GetDegreeLevelForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetDegreeLevelForViewDto()
                {
                    DegreeLevel = new DegreeLevelDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetDegreeLevelForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetDegreeLevelForViewDto> GetDegreeLevelForView(int id)
        {
            var degreeLevel = await _degreeLevelRepository.GetAsync(id);

            var output = new GetDegreeLevelForViewDto { DegreeLevel = ObjectMapper.Map<DegreeLevelDto>(degreeLevel) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_DegreeLevels_Edit)]
        public async Task<GetDegreeLevelForEditOutput> GetDegreeLevelForEdit(EntityDto input)
        {
            var degreeLevel = await _degreeLevelRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDegreeLevelForEditOutput { DegreeLevel = ObjectMapper.Map<CreateOrEditDegreeLevelDto>(degreeLevel) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDegreeLevelDto input)
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

        [AbpAuthorize(AppPermissions.Pages_DegreeLevels_Create)]
        protected virtual async Task Create(CreateOrEditDegreeLevelDto input)
        {
            var degreeLevel = ObjectMapper.Map<DegreeLevel>(input);

            if (AbpSession.TenantId != null)
            {
                degreeLevel.TenantId = (int?)AbpSession.TenantId;
            }

            await _degreeLevelRepository.InsertAsync(degreeLevel);

        }

        [AbpAuthorize(AppPermissions.Pages_DegreeLevels_Edit)]
        protected virtual async Task Update(CreateOrEditDegreeLevelDto input)
        {
            var degreeLevel = await _degreeLevelRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, degreeLevel);

        }

        [AbpAuthorize(AppPermissions.Pages_DegreeLevels_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _degreeLevelRepository.DeleteAsync(input.Id);
        }

    }
}