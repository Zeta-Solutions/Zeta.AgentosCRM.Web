using Zeta.AgentosCRM.CRMClient;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMClient.Education.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMClient.Education
{
    [AbpAuthorize(AppPermissions.Pages_OtherTestScores)]
    public class OtherTestScoresAppService : AgentosCRMAppServiceBase, IOtherTestScoresAppService
    {
        private readonly IRepository<OtherTestScore> _otherTestScoreRepository;
        private readonly IRepository<Client, long> _lookup_clientRepository;

        public OtherTestScoresAppService(IRepository<OtherTestScore> otherTestScoreRepository, IRepository<Client, long> lookup_clientRepository)
        {
            _otherTestScoreRepository = otherTestScoreRepository;
            _lookup_clientRepository = lookup_clientRepository;

        }

        public async Task<PagedResultDto<GetOtherTestScoreForViewDto>> GetAll(GetAllOtherTestScoresInput input)
        {

            var filteredOtherTestScores = _otherTestScoreRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(input.MinTotalScoreFilter != null, e => e.TotalScore >= input.MinTotalScoreFilter)
                        .WhereIf(input.MaxTotalScoreFilter != null, e => e.TotalScore <= input.MaxTotalScoreFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientFirstNameFilter), e => e.ClientFk != null && e.ClientFk.FirstName == input.ClientFirstNameFilter);

            var pagedAndFilteredOtherTestScores = filteredOtherTestScores
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var otherTestScores = from o in pagedAndFilteredOtherTestScores
                                  join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                                  from s1 in j1.DefaultIfEmpty()

                                  select new
                                  {

                                      o.Name,
                                      o.TotalScore,
                                      Id = o.Id,
                                      ClientFirstName = s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString()
                                  };

            var totalCount = await filteredOtherTestScores.CountAsync();

            var dbList = await otherTestScores.ToListAsync();
            var results = new List<GetOtherTestScoreForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetOtherTestScoreForViewDto()
                {
                    OtherTestScore = new OtherTestScoreDto
                    {

                        Name = o.Name,
                        TotalScore = o.TotalScore,
                        Id = o.Id,
                    },
                    ClientFirstName = o.ClientFirstName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetOtherTestScoreForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetOtherTestScoreForViewDto> GetOtherTestScoreForView(int id)
        {
            var otherTestScore = await _otherTestScoreRepository.GetAsync(id);

            var output = new GetOtherTestScoreForViewDto { OtherTestScore = ObjectMapper.Map<OtherTestScoreDto>(otherTestScore) };

            if (output.OtherTestScore.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.OtherTestScore.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_OtherTestScores_Edit)]
        public async Task<GetOtherTestScoreForEditOutput> GetOtherTestScoreForEdit(EntityDto input)
        {
            var otherTestScore = await _otherTestScoreRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetOtherTestScoreForEditOutput { OtherTestScore = ObjectMapper.Map<CreateOrEditOtherTestScoreDto>(otherTestScore) };

            if (output.OtherTestScore.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.OtherTestScore.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditOtherTestScoreDto input)
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

        [AbpAuthorize(AppPermissions.Pages_OtherTestScores_Create)]
        protected virtual async Task Create(CreateOrEditOtherTestScoreDto input)
        {
            var otherTestScore = ObjectMapper.Map<OtherTestScore>(input);

            if (AbpSession.TenantId != null)
            {
                otherTestScore.TenantId = (int)AbpSession.TenantId;
            }

            await _otherTestScoreRepository.InsertAsync(otherTestScore);

        }

        [AbpAuthorize(AppPermissions.Pages_OtherTestScores_Edit)]
        protected virtual async Task Update(CreateOrEditOtherTestScoreDto input)
        {
            var otherTestScore = await _otherTestScoreRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, otherTestScore);

        }

        [AbpAuthorize(AppPermissions.Pages_OtherTestScores_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _otherTestScoreRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_OtherTestScores)]
        public async Task<List<OtherTestScoreClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new OtherTestScoreClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = client == null || client.FirstName == null ? "" : client.FirstName.ToString()
                }).ToListAsync();
        }

    }
}