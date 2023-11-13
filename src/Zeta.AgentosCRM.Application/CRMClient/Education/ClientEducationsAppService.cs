using Zeta.AgentosCRM.CRMSetup;

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
    [AbpAuthorize(AppPermissions.Pages_ClientEducations)]
    public class ClientEducationsAppService : AgentosCRMAppServiceBase, IClientEducationsAppService
    {
        private readonly IRepository<ClientEducation, long> _clientEducationRepository;
        private readonly IRepository<DegreeLevel, int> _lookup_degreeLevelRepository;
        private readonly IRepository<Subject, int> _lookup_subjectRepository;
        private readonly IRepository<SubjectArea, int> _lookup_subjectAreaRepository;

        public ClientEducationsAppService(IRepository<ClientEducation, long> clientEducationRepository, IRepository<DegreeLevel, int> lookup_degreeLevelRepository, IRepository<Subject, int> lookup_subjectRepository, IRepository<SubjectArea, int> lookup_subjectAreaRepository)
        {
            _clientEducationRepository = clientEducationRepository;
            _lookup_degreeLevelRepository = lookup_degreeLevelRepository;
            _lookup_subjectRepository = lookup_subjectRepository;
            _lookup_subjectAreaRepository = lookup_subjectAreaRepository;

        }

        public async Task<PagedResultDto<GetClientEducationForViewDto>> GetAll(GetAllClientEducationsInput input)
        {

            var filteredClientEducations = _clientEducationRepository.GetAll()
                        .Include(e => e.DegreeLevelFk)
                        .Include(e => e.SubjectFk)
                        .Include(e => e.SubjectAreaFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.DegreeTitle.Contains(input.Filter) || e.Institution.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DegreeTitleFilter), e => e.DegreeTitle.Contains(input.DegreeTitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.InstitutionFilter), e => e.Institution.Contains(input.InstitutionFilter))
                        .WhereIf(input.MinCourseStartDateFilter != null, e => e.CourseStartDate >= input.MinCourseStartDateFilter)
                        .WhereIf(input.MaxCourseStartDateFilter != null, e => e.CourseStartDate <= input.MaxCourseStartDateFilter)
                        .WhereIf(input.MinCourseEndDateFilter != null, e => e.CourseEndDate >= input.MinCourseEndDateFilter)
                        .WhereIf(input.MaxCourseEndDateFilter != null, e => e.CourseEndDate <= input.MaxCourseEndDateFilter)
                        .WhereIf(input.MinAcadmicScoreFilter != null, e => e.AcadmicScore >= input.MinAcadmicScoreFilter)
                        .WhereIf(input.MaxAcadmicScoreFilter != null, e => e.AcadmicScore <= input.MaxAcadmicScoreFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DegreeLevelNameFilter), e => e.DegreeLevelFk != null && e.DegreeLevelFk.Name == input.DegreeLevelNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectNameFilter), e => e.SubjectFk != null && e.SubjectFk.Name == input.SubjectNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectAreaNameFilter), e => e.SubjectAreaFk != null && e.SubjectAreaFk.Name == input.SubjectAreaNameFilter);

            var pagedAndFilteredClientEducations = filteredClientEducations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var clientEducations = from o in pagedAndFilteredClientEducations
                                   join o1 in _lookup_degreeLevelRepository.GetAll() on o.DegreeLevelId equals o1.Id into j1
                                   from s1 in j1.DefaultIfEmpty()

                                   join o2 in _lookup_subjectRepository.GetAll() on o.SubjectId equals o2.Id into j2
                                   from s2 in j2.DefaultIfEmpty()

                                   join o3 in _lookup_subjectAreaRepository.GetAll() on o.SubjectAreaId equals o3.Id into j3
                                   from s3 in j3.DefaultIfEmpty()

                                   select new
                                   {

                                       o.DegreeTitle,
                                       o.Institution,
                                       o.CourseStartDate,
                                       o.CourseEndDate,
                                       o.AcadmicScore,
                                       Id = o.Id,
                                       DegreeLevelName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                       SubjectName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                                       SubjectAreaName = s3 == null || s3.Name == null ? "" : s3.Name.ToString()
                                   };

            var totalCount = await filteredClientEducations.CountAsync();

            var dbList = await clientEducations.ToListAsync();
            var results = new List<GetClientEducationForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetClientEducationForViewDto()
                {
                    ClientEducation = new ClientEducationDto
                    {

                        DegreeTitle = o.DegreeTitle,
                        Institution = o.Institution,
                        CourseStartDate = o.CourseStartDate,
                        CourseEndDate = o.CourseEndDate,
                        AcadmicScore = o.AcadmicScore,
                        Id = o.Id,
                    },
                    DegreeLevelName = o.DegreeLevelName,
                    SubjectName = o.SubjectName,
                    SubjectAreaName = o.SubjectAreaName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetClientEducationForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetClientEducationForViewDto> GetClientEducationForView(long id)
        {
            var clientEducation = await _clientEducationRepository.GetAsync(id);

            var output = new GetClientEducationForViewDto { ClientEducation = ObjectMapper.Map<ClientEducationDto>(clientEducation) };

            if (output.ClientEducation.DegreeLevelId != null)
            {
                var _lookupDegreeLevel = await _lookup_degreeLevelRepository.FirstOrDefaultAsync((int)output.ClientEducation.DegreeLevelId);
                output.DegreeLevelName = _lookupDegreeLevel?.Name?.ToString();
            }

            if (output.ClientEducation.SubjectId != null)
            {
                var _lookupSubject = await _lookup_subjectRepository.FirstOrDefaultAsync((int)output.ClientEducation.SubjectId);
                output.SubjectName = _lookupSubject?.Name?.ToString();
            }

            if (output.ClientEducation.SubjectAreaId != null)
            {
                var _lookupSubjectArea = await _lookup_subjectAreaRepository.FirstOrDefaultAsync((int)output.ClientEducation.SubjectAreaId);
                output.SubjectAreaName = _lookupSubjectArea?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ClientEducations_Edit)]
        public async Task<GetClientEducationForEditOutput> GetClientEducationForEdit(EntityDto<long> input)
        {
            var clientEducation = await _clientEducationRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetClientEducationForEditOutput { ClientEducation = ObjectMapper.Map<CreateOrEditClientEducationDto>(clientEducation) };

            if (output.ClientEducation.DegreeLevelId != null)
            {
                var _lookupDegreeLevel = await _lookup_degreeLevelRepository.FirstOrDefaultAsync((int)output.ClientEducation.DegreeLevelId);
                output.DegreeLevelName = _lookupDegreeLevel?.Name?.ToString();
            }

            if (output.ClientEducation.SubjectId != null)
            {
                var _lookupSubject = await _lookup_subjectRepository.FirstOrDefaultAsync((int)output.ClientEducation.SubjectId);
                output.SubjectName = _lookupSubject?.Name?.ToString();
            }

            if (output.ClientEducation.SubjectAreaId != null)
            {
                var _lookupSubjectArea = await _lookup_subjectAreaRepository.FirstOrDefaultAsync((int)output.ClientEducation.SubjectAreaId);
                output.SubjectAreaName = _lookupSubjectArea?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditClientEducationDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ClientEducations_Create)]
        protected virtual async Task Create(CreateOrEditClientEducationDto input)
        {
            var clientEducation = ObjectMapper.Map<ClientEducation>(input);

            if (AbpSession.TenantId != null)
            {
                clientEducation.TenantId = (int)AbpSession.TenantId;
            }

            await _clientEducationRepository.InsertAsync(clientEducation);

        }

        [AbpAuthorize(AppPermissions.Pages_ClientEducations_Edit)]
        protected virtual async Task Update(CreateOrEditClientEducationDto input)
        {
            var clientEducation = await _clientEducationRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, clientEducation);

        }

        [AbpAuthorize(AppPermissions.Pages_ClientEducations_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _clientEducationRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_ClientEducations)]
        public async Task<List<ClientEducationDegreeLevelLookupTableDto>> GetAllDegreeLevelForTableDropdown()
        {
            return await _lookup_degreeLevelRepository.GetAll()
                .Select(degreeLevel => new ClientEducationDegreeLevelLookupTableDto
                {
                    Id = degreeLevel.Id,
                    DisplayName = degreeLevel == null || degreeLevel.Name == null ? "" : degreeLevel.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ClientEducations)]
        public async Task<List<ClientEducationSubjectLookupTableDto>> GetAllSubjectForTableDropdown()
        {
            return await _lookup_subjectRepository.GetAll()
                .Select(subject => new ClientEducationSubjectLookupTableDto
                {
                    Id = subject.Id,
                    DisplayName = subject == null || subject.Name == null ? "" : subject.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_ClientEducations)]
        public async Task<List<ClientEducationSubjectAreaLookupTableDto>> GetAllSubjectAreaForTableDropdown()
        {
            return await _lookup_subjectAreaRepository.GetAll()
                .Select(subjectArea => new ClientEducationSubjectAreaLookupTableDto
                {
                    Id = subjectArea.Id,
                    DisplayName = subjectArea == null || subjectArea.Name == null ? "" : subjectArea.Name.ToString()
                }).ToListAsync();
        }

    }
}