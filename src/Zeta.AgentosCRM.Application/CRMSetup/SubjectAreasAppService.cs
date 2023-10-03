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
    public class SubjectAreasAppService : AgentosCRMAppServiceBase, ISubjectAreasAppService
    {
        private readonly IRepository<SubjectArea> _subjectAreaRepository;
        private readonly ISubjectAreasExcelExporter _subjectAreasExcelExporter;
        private readonly IRepository<Subject, int> _lookup_subjectRepository;

        public SubjectAreasAppService(IRepository<SubjectArea> subjectAreaRepository, ISubjectAreasExcelExporter subjectAreasExcelExporter, IRepository<Subject, int> lookup_subjectRepository)
        {
            _subjectAreaRepository = subjectAreaRepository;
            _subjectAreasExcelExporter = subjectAreasExcelExporter;
            _lookup_subjectRepository = lookup_subjectRepository;

        }

        public async Task<PagedResultDto<GetSubjectAreaForViewDto>> GetAll(GetAllSubjectAreasInput input)
        {

            var filteredSubjectAreas = _subjectAreaRepository.GetAll()
                        .Include(e => e.SubjectFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectNameFilter), e => e.SubjectFk != null && e.SubjectFk.Name == input.SubjectNameFilter);

            var pagedAndFilteredSubjectAreas = filteredSubjectAreas
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var subjectAreas = from o in pagedAndFilteredSubjectAreas
                               join o1 in _lookup_subjectRepository.GetAll() on o.SubjectId equals o1.Id into j1
                               from s1 in j1.DefaultIfEmpty()

                               select new
                               {

                                   o.Abbrivation,
                                   o.Name,
                                   Id = o.Id,
                                   SubjectName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                               };

            var totalCount = await filteredSubjectAreas.CountAsync();

            var dbList = await subjectAreas.ToListAsync();
            var results = new List<GetSubjectAreaForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetSubjectAreaForViewDto()
                {
                    SubjectArea = new SubjectAreaDto
                    {

                        Abbrivation = o.Abbrivation,
                        Name = o.Name,
                        Id = o.Id,
                    },
                    SubjectName = o.SubjectName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetSubjectAreaForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetSubjectAreaForViewDto> GetSubjectAreaForView(int id)
        {
            var subjectArea = await _subjectAreaRepository.GetAsync(id);

            var output = new GetSubjectAreaForViewDto { SubjectArea = ObjectMapper.Map<SubjectAreaDto>(subjectArea) };

            if (output.SubjectArea.SubjectId != null)
            {
                var _lookupSubject = await _lookup_subjectRepository.FirstOrDefaultAsync((int)output.SubjectArea.SubjectId);
                output.SubjectName = _lookupSubject?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_SubjectAreas_Edit)]
        public async Task<GetSubjectAreaForEditOutput> GetSubjectAreaForEdit(EntityDto input)
        {
            var subjectArea = await _subjectAreaRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSubjectAreaForEditOutput { SubjectArea = ObjectMapper.Map<CreateOrEditSubjectAreaDto>(subjectArea) };

            if (output.SubjectArea.SubjectId != null)
            {
                var _lookupSubject = await _lookup_subjectRepository.FirstOrDefaultAsync((int)output.SubjectArea.SubjectId);
                output.SubjectName = _lookupSubject?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditSubjectAreaDto input)
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
        protected virtual async Task Create(CreateOrEditSubjectAreaDto input)
        {
            var subjectArea = ObjectMapper.Map<SubjectArea>(input);

            if (AbpSession.TenantId != null)
            {
                subjectArea.TenantId = (int)AbpSession.TenantId;
            }

            await _subjectAreaRepository.InsertAsync(subjectArea);

        }

        [AbpAuthorize(AppPermissions.Pages_SubjectAreas_Edit)]
        protected virtual async Task Update(CreateOrEditSubjectAreaDto input)
        {
            var subjectArea = await _subjectAreaRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, subjectArea);

        }

        [AbpAuthorize(AppPermissions.Pages_SubjectAreas_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _subjectAreaRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetSubjectAreasToExcel(GetAllSubjectAreasForExcelInput input)
        {

            var filteredSubjectAreas = _subjectAreaRepository.GetAll()
                        .Include(e => e.SubjectFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectNameFilter), e => e.SubjectFk != null && e.SubjectFk.Name == input.SubjectNameFilter);

            var query = (from o in filteredSubjectAreas
                         join o1 in _lookup_subjectRepository.GetAll() on o.SubjectId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetSubjectAreaForViewDto()
                         {
                             SubjectArea = new SubjectAreaDto
                             {
                                 Abbrivation = o.Abbrivation,
                                 Name = o.Name,
                                 Id = o.Id
                             },
                             SubjectName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                         });

            var subjectAreaListDtos = await query.ToListAsync();

            return _subjectAreasExcelExporter.ExportToFile(subjectAreaListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_SubjectAreas)]
        public async Task<List<SubjectAreaSubjectLookupTableDto>> GetAllSubjectForTableDropdown()
        {
            return await _lookup_subjectRepository.GetAll()
                .Select(subject => new SubjectAreaSubjectLookupTableDto
                {
                    Id = subject.Id,
                    DisplayName = subject == null || subject.Name == null ? "" : subject.Name.ToString()
                }).ToListAsync();
        }

    }
}