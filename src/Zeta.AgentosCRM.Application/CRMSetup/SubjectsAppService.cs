using Zeta.AgentosCRM.CRMSetup;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.Exporting;
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
    [AbpAuthorize(AppPermissions.Pages_SubjectAreas)]
    public class SubjectsAppService : AgentosCRMAppServiceBase, ISubjectsAppService
    {
        private readonly IRepository<Subject> _subjectRepository;
        private readonly ISubjectsExcelExporter _subjectsExcelExporter;
        private readonly IRepository<SubjectArea, int> _lookup_subjectAreaRepository;

        public SubjectsAppService(IRepository<Subject> subjectAreaRepository, ISubjectsExcelExporter subjectAreasExcelExporter, IRepository<SubjectArea, int> lookup_subjectRepository)
        {
            _subjectRepository = subjectAreaRepository;
            _subjectsExcelExporter = subjectAreasExcelExporter;
            _lookup_subjectAreaRepository = lookup_subjectRepository;

        }

        public async Task<PagedResultDto<GetSubjectForViewDto>> GetAll(GetAllSubjectsInput input)
        {

            var filteredSubjects = _subjectRepository.GetAll()
                        .Include(e => e.SubjectAreaFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectNameFilter), e => e.SubjectAreaFk != null && e.SubjectAreaFk.Name == input.SubjectNameFilter);

            var pagedAndFilteredSubjects = filteredSubjects
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var subjects = from o in pagedAndFilteredSubjects
                               join o1 in _lookup_subjectAreaRepository.GetAll() on o.SubjectAreaId equals o1.Id into j1
                               from s1 in j1.DefaultIfEmpty()

                               select new
                               {

                                   o.Abbrivation,
                                   o.Name,
                                   Id = o.Id,
                                   SubjectAreaName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                               };

            var totalCount = await filteredSubjects.CountAsync();

            var dbList = await subjects.ToListAsync();
            var results = new List<GetSubjectForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetSubjectForViewDto()
                {
                    Subject = new SubjectDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                    },
                    SubjectAreaName = o.SubjectAreaName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetSubjectForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetSubjectForViewDto> GetSubjectForView(int id)
        {
            var subjectArea = await _subjectRepository.GetAsync(id);

            var output = new GetSubjectForViewDto { Subject = ObjectMapper.Map<SubjectDto>(subjectArea) };

            if (output.SubjectAreaName != null)
            {
                var _lookupSubject = await _lookup_subjectAreaRepository.FirstOrDefaultAsync((int)output.Subject.SubjectAreaId);
                output.SubjectAreaName = _lookupSubject?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_SubjectAreas_Edit)]
        public async Task<GetSubjectForEditOutput> GetSubjectForEdit(EntityDto input)
        {
            var subjectArea = await _subjectRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSubjectForEditOutput { Subject = ObjectMapper.Map<CreateOrEditSubjectDto>(subjectArea) };

            if (output.SubjectAreaName != null)
            {
                var _lookupSubject = await _lookup_subjectAreaRepository.FirstOrDefaultAsync((int)output.Subject.SubjectAreaId);
                output.SubjectAreaName = _lookupSubject?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditSubjectDto input)
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

        [AbpAuthorize(AppPermissions.Pages_SubjectAreas_Create)]
        protected virtual async Task Create(CreateOrEditSubjectDto input)
        {
            var subjectArea = ObjectMapper.Map<Subject>(input);

            if (AbpSession.TenantId != null)
            {
                subjectArea.TenantId = (int)AbpSession.TenantId;
            }

            await _subjectRepository.InsertAsync(subjectArea);

        }

        [AbpAuthorize(AppPermissions.Pages_SubjectAreas_Edit)]
        protected virtual async Task Update(CreateOrEditSubjectDto input)
        {
            var subjectArea = await _subjectRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, subjectArea);

        }

        [AbpAuthorize(AppPermissions.Pages_SubjectAreas_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _subjectRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetSubjectsToExcel(GetAllSubjectsForExcelInput input)
        {

            var filteredSubjectAreas = _subjectRepository.GetAll()
                        .Include(e => e.SubjectAreaFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectAreaNameFilter), e => e.SubjectAreaFk != null && e.SubjectAreaFk.Name == input.SubjectAreaNameFilter);

            var query = (from o in filteredSubjectAreas
                         join o1 in _lookup_subjectAreaRepository.GetAll() on o.SubjectAreaId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetSubjectForViewDto()
                         {
                             Subject = new SubjectDto
                             {
                                 Abbrivation = o.Abbrivation,
                                 Name = o.Name,
                                 Id = o.Id
                             },
                             SubjectAreaName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                         });

            var subjectAreaListDtos = await query.ToListAsync();

            return _subjectsExcelExporter.ExportToFile(subjectAreaListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_SubjectAreas)]
        public async Task<List<SubjectSubjectAreaLookupTableDto>> GetAllSubjectAreaForTableDropdown()
        {
            return await _lookup_subjectAreaRepository.GetAll()
                .Select(subject => new SubjectSubjectAreaLookupTableDto
                {
                    Id = subject.Id,
                    DisplayName = subject == null || subject.Name == null ? "" : subject.Name.ToString()
                }).ToListAsync();
        }

    }
}