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
    [AbpAuthorize(AppPermissions.Pages_EnglisTestScores)]
    public class EnglisTestScoresAppService : AgentosCRMAppServiceBase, IEnglisTestScoresAppService
    {
        private readonly IRepository<EnglisTestScore, long> _englisTestScoreRepository;
        private readonly IRepository<Client, long> _lookup_clientRepository;

        public EnglisTestScoresAppService(IRepository<EnglisTestScore, long> englisTestScoreRepository, IRepository<Client, long> lookup_clientRepository)
        {
            _englisTestScoreRepository = englisTestScoreRepository;
            _lookup_clientRepository = lookup_clientRepository;

        }

        public async Task<PagedResultDto<GetEnglisTestScoreForViewDto>> GetAll(GetAllEnglisTestScoresInput input)
        {

            var filteredEnglisTestScores = _englisTestScoreRepository.GetAll()
                        .Include(e => e.ClientFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(input.MinListentingFilter != null, e => e.Listenting >= input.MinListentingFilter)
                        .WhereIf(input.MaxListentingFilter != null, e => e.Listenting <= input.MaxListentingFilter)
                        .WhereIf(input.MinReadingFilter != null, e => e.Reading >= input.MinReadingFilter)
                        .WhereIf(input.MaxReadingFilter != null, e => e.Reading <= input.MaxReadingFilter)
                        .WhereIf(input.MinWritingFilter != null, e => e.Writing >= input.MinWritingFilter)
                        .WhereIf(input.MaxWritingFilter != null, e => e.Writing <= input.MaxWritingFilter)
                        .WhereIf(input.MinSpeakingFilter != null, e => e.Speaking >= input.MinSpeakingFilter)
                        .WhereIf(input.MaxSpeakingFilter != null, e => e.Speaking <= input.MaxSpeakingFilter)
                        .WhereIf(input.MinTotalScoreFilter != null, e => e.TotalScore >= input.MinTotalScoreFilter)
                        .WhereIf(input.MaxTotalScoreFilter != null, e => e.TotalScore <= input.MaxTotalScoreFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientFirstNameFilter), e => e.ClientFk != null && e.ClientFk.FirstName == input.ClientFirstNameFilter);

            var pagedAndFilteredEnglisTestScores = filteredEnglisTestScores
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var englisTestScores = from o in pagedAndFilteredEnglisTestScores
                                   join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                                   from s1 in j1.DefaultIfEmpty()

                                   select new
                                   {

                                       o.Name,
                                       o.Listenting,
                                       o.Reading,
                                       o.Writing,
                                       o.Speaking,
                                       o.TotalScore,
                                       Id = o.Id,
                                       ClientFirstName = s1 == null || s1.FirstName == null ? "" : s1.FirstName.ToString()
                                   };

            var totalCount = await filteredEnglisTestScores.CountAsync();

            var dbList = await englisTestScores.ToListAsync();
            var results = new List<GetEnglisTestScoreForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetEnglisTestScoreForViewDto()
                {
                    EnglisTestScore = new EnglisTestScoreDto
                    {

                        Name = o.Name,
                        Listenting = o.Listenting,
                        Reading = o.Reading,
                        Writing = o.Writing,
                        Speaking = o.Speaking,
                        TotalScore = o.TotalScore,
                        Id = o.Id,
                    },
                    ClientFirstName = o.ClientFirstName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetEnglisTestScoreForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetEnglisTestScoreForViewDto> GetEnglisTestScoreForView(long id)
        {
            var englisTestScore = await _englisTestScoreRepository.GetAsync(id);

            var output = new GetEnglisTestScoreForViewDto { EnglisTestScore = ObjectMapper.Map<EnglisTestScoreDto>(englisTestScore) };

            if (output.EnglisTestScore.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.EnglisTestScore.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_EnglisTestScores_Edit)]
        public async Task<GetEnglisTestScoreForEditOutput> GetEnglisTestScoreForEdit(EntityDto<long> input)
        {
            var englisTestScore = await _englisTestScoreRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEnglisTestScoreForEditOutput { EnglisTestScore = ObjectMapper.Map<CreateOrEditEnglisTestScoreDto>(englisTestScore) };

            if (output.EnglisTestScore.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.EnglisTestScore.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditEnglisTestScoreDto input)
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

        [AbpAuthorize(AppPermissions.Pages_EnglisTestScores_Create)]
        protected virtual async Task Create(CreateOrEditEnglisTestScoreDto input)
        {
            var englisTestScore = ObjectMapper.Map<EnglisTestScore>(input);

            if (AbpSession.TenantId != null)
            {
                englisTestScore.TenantId = (int)AbpSession.TenantId;
            }

            await _englisTestScoreRepository.InsertAsync(englisTestScore);

        }

        [AbpAuthorize(AppPermissions.Pages_EnglisTestScores_Edit)]
        protected virtual async Task Update(CreateOrEditEnglisTestScoreDto input)
        {
            var englisTestScore = await _englisTestScoreRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, englisTestScore);

        }

        [AbpAuthorize(AppPermissions.Pages_EnglisTestScores_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _englisTestScoreRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_EnglisTestScores)]
        public async Task<List<EnglisTestScoreClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new EnglisTestScoreClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = client == null || client.FirstName == null ? "" : client.FirstName.ToString()
                }).ToListAsync();
        }

    }
}