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
    [AbpAuthorize(AppPermissions.Pages_Subjects)]
    public class SubjectsAppService : AgentosCRMAppServiceBase, ISubjectsAppService
    {
        private readonly IRepository<Subject> _subjectRepository;
        private readonly ISubjectsExcelExporter _subjectsExcelExporter;

        public SubjectsAppService(IRepository<Subject> subjectRepository, ISubjectsExcelExporter subjectsExcelExporter)
        {
            _subjectRepository = subjectRepository;
            _subjectsExcelExporter = subjectsExcelExporter;

        }

        public async Task<PagedResultDto<GetSubjectForViewDto>> GetAll(GetAllSubjectsInput input)
        {

            var filteredSubjects = _subjectRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var pagedAndFilteredSubjects = filteredSubjects
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var subjects = from o in pagedAndFilteredSubjects
                           select new
                           {

                               o.Abbrivation,
                               o.Name,
                               Id = o.Id
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
                    }
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
            var subject = await _subjectRepository.GetAsync(id);

            var output = new GetSubjectForViewDto { Subject = ObjectMapper.Map<SubjectDto>(subject) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Subjects_Edit)]
        public async Task<GetSubjectForEditOutput> GetSubjectForEdit(EntityDto input)
        {
            var subject = await _subjectRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSubjectForEditOutput { Subject = ObjectMapper.Map<CreateOrEditSubjectDto>(subject) };

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

        [AbpAuthorize(AppPermissions.Pages_Subjects_Create)]
        protected virtual async Task Create(CreateOrEditSubjectDto input)
        {
            var subject = ObjectMapper.Map<Subject>(input);

            if (AbpSession.TenantId != null)
            {
                subject.TenantId = (int)AbpSession.TenantId;
            }

            await _subjectRepository.InsertAsync(subject);

        }

        [AbpAuthorize(AppPermissions.Pages_Subjects_Edit)]
        protected virtual async Task Update(CreateOrEditSubjectDto input)
        {
            var subject = await _subjectRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, subject);

        }

        [AbpAuthorize(AppPermissions.Pages_Subjects_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _subjectRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetSubjectsToExcel(GetAllSubjectsForExcelInput input)
        {

            var filteredSubjects = _subjectRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Abbrivation.Contains(input.Filter) || e.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AbbrivationFilter), e => e.Abbrivation.Contains(input.AbbrivationFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter));

            var query = (from o in filteredSubjects
                         select new GetSubjectForViewDto()
                         {
                             Subject = new SubjectDto
                             {
                                 Abbrivation = o.Abbrivation,
                                 Name = o.Name,
                                 Id = o.Id
                             }
                         });

            var subjectListDtos = await query.ToListAsync();

            return _subjectsExcelExporter.ExportToFile(subjectListDtos);
        }

    }
}