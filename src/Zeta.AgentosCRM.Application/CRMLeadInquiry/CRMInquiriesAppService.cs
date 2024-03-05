using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.CRMSetup;
using Abp.Organizations;
using Zeta.AgentosCRM.CRMSetup.LeadSource;
using Zeta.AgentosCRM.CRMSetup.Tag;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMLeadInquiry.Exporting;
using Zeta.AgentosCRM.CRMLeadInquiry.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.Authorization.Users;
using Zeta.AgentosCRM.CRMClient.InterstedServices;

namespace Zeta.AgentosCRM.CRMLeadInquiry
{
    //[AbpAuthorize(AppPermissions.Pages_CRMInquiries)]
    public class CRMInquiriesAppService : AgentosCRMAppServiceBase, ICRMInquiriesAppService
    {
        private readonly IRepository<CRMInquiry, long> _crmInquiryRepository;
        private readonly ICRMInquiriesExcelExporter _crmInquiriesExcelExporter;
        private readonly IRepository<Country, int> _lookup_countryRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<DegreeLevel, int> _lookup_degreeLevelRepository;
        private readonly IRepository<Subject, int> _lookup_subjectRepository;
        private readonly IRepository<SubjectArea, int> _lookup_subjectAreaRepository;
        private readonly IRepository<OrganizationUnit, long> _lookup_organizationUnitRepository;
        private readonly IRepository<LeadSource, int> _lookup_leadSourceRepository;
        private readonly IRepository<Tag, int> _lookup_tagRepository;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<ClientInterstedService, long> _clientInterstedServiceRepository;
        private readonly IRepository<Workflow> _workflowRepository;
        public CRMInquiriesAppService(IRepository<CRMInquiry, long> crmInquiryRepository, ICRMInquiriesExcelExporter crmInquiriesExcelExporter, IRepository<Country, int> lookup_countryRepository, IRepository<DegreeLevel, int> lookup_degreeLevelRepository, IRepository<Subject, int> lookup_subjectRepository, IRepository<SubjectArea, int> lookup_subjectAreaRepository, IRepository<OrganizationUnit, long> lookup_organizationUnitRepository, IRepository<LeadSource, int> lookup_leadSourceRepository, IRepository<Tag, int> lookup_tagRepository, ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager, IRepository<User, long> lookup_userRepository, IRepository<ClientInterstedService, long> clientInterstedServiceRepository, IRepository<Workflow> workflowRepository)
        {
            _crmInquiryRepository = crmInquiryRepository;
            _crmInquiriesExcelExporter = crmInquiriesExcelExporter;
            _lookup_countryRepository = lookup_countryRepository;
            _lookup_degreeLevelRepository = lookup_degreeLevelRepository;
            _lookup_subjectRepository = lookup_subjectRepository;
            _lookup_subjectAreaRepository = lookup_subjectAreaRepository;
            _lookup_organizationUnitRepository = lookup_organizationUnitRepository;
            _lookup_leadSourceRepository = lookup_leadSourceRepository;
            _lookup_tagRepository = lookup_tagRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            _lookup_userRepository = lookup_userRepository;
            _clientInterstedServiceRepository = clientInterstedServiceRepository;
            _workflowRepository = workflowRepository;
        }

        public async Task<PagedResultDto<GetCRMInquiryForViewDto>> GetAll(GetAllCRMInquiriesInput input)
        {

            var filteredCRMInquiries = _crmInquiryRepository.GetAll()
                        .Include(e => e.CountryFk)
                        .Include(e => e.PassportCountryFk)
                        .Include(e => e.DegreeLevelFk)
                        .Include(e => e.SubjectFk)
                        .Include(e => e.SubjectAreaFk)
                        .Include(e => e.OrganizationUnitFk)
                        .Include(e => e.LeadSourceFk)
                        .Include(e => e.TagFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FirstName.Contains(input.Filter) || e.LastName.Contains(input.Filter) || e.PhoneCode.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.SecondaryEmail.Contains(input.Filter) || e.ContactPreference.Contains(input.Filter) || e.Street.Contains(input.Filter) || e.City.Contains(input.Filter) || e.State.Contains(input.Filter) || e.PostalCode.Contains(input.Filter) || e.VisaType.Contains(input.Filter) || e.PreferedInTake.Contains(input.Filter) || e.DegreeTitle.Contains(input.Filter) || e.Institution.Contains(input.Filter) || e.Comments.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FirstNameFilter), e => e.FirstName.Contains(input.FirstNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LastNameFilter), e => e.LastName.Contains(input.LastNameFilter))
                        .WhereIf(input.MinDateofBirthFilter != null, e => e.DateofBirth >= input.MinDateofBirthFilter)
                        .WhereIf(input.MaxDateofBirthFilter != null, e => e.DateofBirth <= input.MaxDateofBirthFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneCodeFilter), e => e.PhoneCode.Contains(input.PhoneCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo.Contains(input.PhoneNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.Contains(input.EmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SecondaryEmailFilter), e => e.SecondaryEmail.Contains(input.SecondaryEmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ContactPreferenceFilter), e => e.ContactPreference.Contains(input.ContactPreferenceFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StreetFilter), e => e.Street.Contains(input.StreetFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CityFilter), e => e.City.Contains(input.CityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StateFilter), e => e.State.Contains(input.StateFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PostalCodeFilter), e => e.PostalCode.Contains(input.PostalCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.VisaTypeFilter), e => e.VisaType.Contains(input.VisaTypeFilter))
                        .WhereIf(input.MinVisaExpiryDateFilter != null, e => e.VisaExpiryDate >= input.MinVisaExpiryDateFilter)
                        .WhereIf(input.MaxVisaExpiryDateFilter != null, e => e.VisaExpiryDate <= input.MaxVisaExpiryDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PreferedInTakeFilter), e => e.PreferedInTake.Contains(input.PreferedInTakeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DegreeTitleFilter), e => e.DegreeTitle.Contains(input.DegreeTitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.InstitutionFilter), e => e.Institution.Contains(input.InstitutionFilter))
                        .WhereIf(input.MinCourseStartDateFilter != null, e => e.CourseStartDate >= input.MinCourseStartDateFilter)
                        .WhereIf(input.MaxCourseStartDateFilter != null, e => e.CourseStartDate <= input.MaxCourseStartDateFilter)
                        .WhereIf(input.MinCourseEndDateFilter != null, e => e.CourseEndDate >= input.MinCourseEndDateFilter)
                        .WhereIf(input.MaxCourseEndDateFilter != null, e => e.CourseEndDate <= input.MaxCourseEndDateFilter)
                        .WhereIf(input.MinAcademicScoreFilter != null, e => e.AcademicScore >= input.MinAcademicScoreFilter)
                        .WhereIf(input.MaxAcademicScoreFilter != null, e => e.AcademicScore <= input.MaxAcademicScoreFilter)
                        .WhereIf(input.IsGpaFilter.HasValue && input.IsGpaFilter > -1, e => (input.IsGpaFilter == 1 && e.IsGpa) || (input.IsGpaFilter == 0 && !e.IsGpa))
                        .WhereIf(input.MinToeflFilter != null, e => e.Toefl >= input.MinToeflFilter)
                        .WhereIf(input.MaxToeflFilter != null, e => e.Toefl <= input.MaxToeflFilter)
                        .WhereIf(input.MinIeltsFilter != null, e => e.Ielts >= input.MinIeltsFilter)
                        .WhereIf(input.MaxIeltsFilter != null, e => e.Ielts <= input.MaxIeltsFilter)
                        .WhereIf(input.MinPteFilter != null, e => e.Pte >= input.MinPteFilter)
                        .WhereIf(input.MaxPteFilter != null, e => e.Pte <= input.MaxPteFilter)
                        .WhereIf(input.MinSat1Filter != null, e => e.Sat1 >= input.MinSat1Filter)
                        .WhereIf(input.MaxSat1Filter != null, e => e.Sat1 <= input.MaxSat1Filter)
                        .WhereIf(input.MinSat2Filter != null, e => e.Sat2 >= input.MinSat2Filter)
                        .WhereIf(input.MaxSat2Filter != null, e => e.Sat2 <= input.MaxSat2Filter)
                        .WhereIf(input.MinGreFilter != null, e => e.Gre >= input.MinGreFilter)
                        .WhereIf(input.MaxGreFilter != null, e => e.Gre <= input.MaxGreFilter)
                        .WhereIf(input.MinGMatFilter != null, e => e.GMat >= input.MinGMatFilter)
                        .WhereIf(input.MaxGMatFilter != null, e => e.GMat <= input.MaxGMatFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CommentsFilter), e => e.Comments.Contains(input.CommentsFilter))
                        .WhereIf(input.MinStatusFilter != null, e => e.Status >= input.MinStatusFilter)
                        .WhereIf(input.MaxStatusFilter != null, e => e.Status <= input.MaxStatusFilter)
                        .WhereIf(input.IsArchivedFilter.HasValue && input.IsArchivedFilter > -1, e => (input.IsArchivedFilter == 1 && e.IsArchived) || (input.IsArchivedFilter == 0 && !e.IsArchived))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CountryNameFilter), e => e.CountryFk != null && e.CountryFk.Name == input.CountryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CountryName2Filter), e => e.PassportCountryFk != null && e.PassportCountryFk.Name == input.CountryName2Filter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DegreeLevelNameFilter), e => e.DegreeLevelFk != null && e.DegreeLevelFk.Name == input.DegreeLevelNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectNameFilter), e => e.SubjectFk != null && e.SubjectFk.Name == input.SubjectNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectAreaNameFilter), e => e.SubjectAreaFk != null && e.SubjectAreaFk.Name == input.SubjectAreaNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.OrganizationUnitFk != null && e.OrganizationUnitFk.DisplayName == input.OrganizationUnitDisplayNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LeadSourceNameFilter), e => e.LeadSourceFk != null && e.LeadSourceFk.Name == input.LeadSourceNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TagNameFilter), e => e.TagFk != null && e.TagFk.Name == input.TagNameFilter);

            var pagedAndFilteredCRMInquiries = filteredCRMInquiries
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var crmInquiries = from o in pagedAndFilteredCRMInquiries
                               join o1 in _lookup_countryRepository.GetAll() on o.CountryId equals o1.Id into j1
                               from s1 in j1.DefaultIfEmpty()

                               join o2 in _lookup_countryRepository.GetAll() on o.PassportCountryId equals o2.Id into j2
                               from s2 in j2.DefaultIfEmpty()

                               join o3 in _lookup_degreeLevelRepository.GetAll() on o.DegreeLevelId equals o3.Id into j3
                               from s3 in j3.DefaultIfEmpty()

                               join o4 in _lookup_subjectRepository.GetAll() on o.SubjectId equals o4.Id into j4
                               from s4 in j4.DefaultIfEmpty()

                               join o5 in _lookup_subjectAreaRepository.GetAll() on o.SubjectAreaId equals o5.Id into j5
                               from s5 in j5.DefaultIfEmpty()

                               join o6 in _lookup_organizationUnitRepository.GetAll() on o.OrganizationUnitId equals o6.Id into j6
                               from s6 in j6.DefaultIfEmpty()

                               join o7 in _lookup_leadSourceRepository.GetAll() on o.LeadSourceId equals o7.Id into j7
                               from s7 in j7.DefaultIfEmpty()

                               join o8 in _lookup_tagRepository.GetAll() on o.TagId equals o8.Id into j8
                               from s8 in j8.DefaultIfEmpty()

                               join o9 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o9.Id into j9
                               from s9 in j9.DefaultIfEmpty()

                               


                               select new
                               {

                                   o.FirstName,
                                   o.LastName,
                                   o.DateofBirth,
                                   o.PhoneCode,
                                   o.PhoneNo,
                                   o.Email,
                                   o.SecondaryEmail,
                                   o.ContactPreference,
                                   o.Street,
                                   o.City,
                                   o.State,
                                   o.PostalCode,
                                   o.VisaType,
                                   o.VisaExpiryDate,
                                   o.PreferedInTake,
                                   o.DegreeTitle,
                                   o.Institution,
                                   o.CourseStartDate,
                                   o.CourseEndDate,
                                   o.AcademicScore,
                                   o.IsGpa,
                                   o.Toefl,
                                   o.Ielts,
                                   o.Pte,
                                   o.Sat1,
                                   o.Sat2,
                                   o.Gre,
                                   o.GMat,
                                   o.DocumentId,
                                   o.PictureId,
                                   o.Comments,
                                   o.Status,
                                   o.IsArchived,
                                   o.CreationTime,
                                   o.LastModificationTime,
                                   o.InterstedService,
                                   Id = o.Id,
                                   CountryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                   CountryName2 = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                                   DegreeLevelName = s3 == null || s3.Name == null ? "" : s3.Name.ToString(),
                                   SubjectName = s4 == null || s4.Name == null ? "" : s4.Name.ToString(),
                                   SubjectAreaName = s5 == null || s5.Name == null ? "" : s5.Name.ToString(),
                                   OrganizationUnitDisplayName = s6 == null || s6.DisplayName == null ? "" : s6.DisplayName.ToString(),
                                   LeadSourceName = s7 == null || s7.Name == null ? "" : s7.Name.ToString(),
                                   TagName = s8 == null || s8.Name == null ? "" : s8.Name.ToString(),
                                   UserName = s9 == null || s9.Name == null ? "" : s9.Name.ToString(),
        };

            var totalCount = await filteredCRMInquiries.CountAsync();

            var dbList = await crmInquiries.ToListAsync();
            var results = new List<GetCRMInquiryForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCRMInquiryForViewDto()
                {
                    CRMInquiry = new CRMInquiryDto
                    {

                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        DateofBirth = o.DateofBirth,
                        PhoneCode = o.PhoneCode,
                        PhoneNo = o.PhoneNo,
                        Email = o.Email,
                        SecondaryEmail = o.SecondaryEmail,
                        ContactPreference = o.ContactPreference,
                        Street = o.Street,
                        City = o.City,
                        State = o.State,
                        PostalCode = o.PostalCode,
                        VisaType = o.VisaType,
                        VisaExpiryDate = o.VisaExpiryDate,
                        PreferedInTake = o.PreferedInTake,
                        DegreeTitle = o.DegreeTitle,
                        Institution = o.Institution,
                        CourseStartDate = o.CourseStartDate,
                        CourseEndDate = o.CourseEndDate,
                        AcademicScore = o.AcademicScore,
                        IsGpa = o.IsGpa,
                        Toefl = o.Toefl,
                        Ielts = o.Ielts,
                        Pte = o.Pte,
                        Sat1 = o.Sat1,
                        Sat2 = o.Sat2,
                        Gre = o.Gre,
                        GMat = o.GMat,
                        DocumentId = o.DocumentId,
                        PictureId = o.PictureId,
                        Comments = o.Comments,
                        Status = o.Status,
                        IsArchived = o.IsArchived,
                        Id = o.Id,
                        CreationTime=o.CreationTime,
                        LastModificationTime=o.LastModificationTime,
                    },
                    CountryName = o.CountryName,
                    CountryName2 = o.CountryName2,
                    DegreeLevelName = o.DegreeLevelName,
                    SubjectName = o.SubjectName,
                    SubjectAreaName = o.SubjectAreaName,
                    OrganizationUnitDisplayName = o.OrganizationUnitDisplayName,
                    LeadSourceName = o.LeadSourceName,
                    TagName = o.TagName,
                    UserName = o.UserName,
                };
                res.CRMInquiry.InterstedService = await GetDisplayNamesForIds(o.InterstedService);
                res.CRMInquiry.DocumentIdFileName = await GetBinaryFileName(o.DocumentId);
                res.CRMInquiry.PictureIdFileName = await GetBinaryFileName(o.PictureId);

                results.Add(res);
            }

            return new PagedResultDto<GetCRMInquiryForViewDto>(
                totalCount,
                results
            );

        }
        private async Task<string> GetDisplayNamesForIds(string ids)
        {
            if (string.IsNullOrEmpty(ids))
                return string.Empty;

            var idArray = ids.Split(',').Select(long.Parse).ToList(); // Convert to List<long>
            var clientInterstedServices = _clientInterstedServiceRepository.GetAll()
                .Where(clientIntersted => idArray.Contains(clientIntersted.Id))
                .ToList();

            var workflowIdList = new List<long>();
            foreach (var clientIntersted in clientInterstedServices)
            {
                workflowIdList.Add(clientIntersted.WorkflowId);

            }
            var workFlowNames = new List<string>();
            foreach (var Wid in workflowIdList)
            {
                var workFlow = _workflowRepository.GetAll()
                    .FirstOrDefault(workflow => workflow.Id == Wid); // Retrieve the workflow with the specific ID

                if (workFlow != null)
                {
                    workFlowNames.Add(workFlow.Name);
                }
            }
            //var workFlowNames = _workflowRepository.GetAll()
            //    .Where(workflow => workflowIdList.Contains(workflow.Id))
            //    .Select(workflow => workflow.Name)
            //.ToList();
            string concatenatedNames = "";
            foreach (var name in workFlowNames)
            {
                if (!string.IsNullOrEmpty(concatenatedNames))
                {
                    concatenatedNames += ", ";
                }
                concatenatedNames += name;
            }
            return concatenatedNames;
            //return string.Join(", ", workFlowNames);
        }




        public async Task<GetCRMInquiryForViewDto> GetCRMInquiryForView(long id)
        {
            var crmInquiry = await _crmInquiryRepository.GetAsync(id);

            var output = new GetCRMInquiryForViewDto { CRMInquiry = ObjectMapper.Map<CRMInquiryDto>(crmInquiry) };

            if (output.CRMInquiry.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.CRMInquiry.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }

            if (output.CRMInquiry.PassportCountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.CRMInquiry.PassportCountryId);
                output.CountryName2 = _lookupCountry?.Name?.ToString();
            }

            if (output.CRMInquiry.DegreeLevelId != null)
            {
                var _lookupDegreeLevel = await _lookup_degreeLevelRepository.FirstOrDefaultAsync((int)output.CRMInquiry.DegreeLevelId);
                output.DegreeLevelName = _lookupDegreeLevel?.Name?.ToString();
            }

            if (output.CRMInquiry.SubjectId != null)
            {
                var _lookupSubject = await _lookup_subjectRepository.FirstOrDefaultAsync((int)output.CRMInquiry.SubjectId);
                output.SubjectName = _lookupSubject?.Name?.ToString();
            }

            if (output.CRMInquiry.SubjectAreaId != null)
            {
                var _lookupSubjectArea = await _lookup_subjectAreaRepository.FirstOrDefaultAsync((int)output.CRMInquiry.SubjectAreaId);
                output.SubjectAreaName = _lookupSubjectArea?.Name?.ToString();
            }

            if (output.CRMInquiry.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.CRMInquiry.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            if (output.CRMInquiry.LeadSourceId != null)
            {
                var _lookupLeadSource = await _lookup_leadSourceRepository.FirstOrDefaultAsync((int)output.CRMInquiry.LeadSourceId);
                output.LeadSourceName = _lookupLeadSource?.Name?.ToString();
            }

            if (output.CRMInquiry.TagId != null)
            {
                var _lookupTag = await _lookup_tagRepository.FirstOrDefaultAsync((int)output.CRMInquiry.TagId);
                output.TagName = _lookupTag?.Name?.ToString();
            }

            output.CRMInquiry.DocumentIdFileName = await GetBinaryFileName(crmInquiry.DocumentId);

            output.CRMInquiry.PictureIdFileName = await GetBinaryFileName(crmInquiry.PictureId);

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CRMInquiries_Edit)]
        public async Task<GetCRMInquiryForEditOutput> GetCRMInquiryForEdit(EntityDto<long> input)
        {
            var crmInquiry = await _crmInquiryRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCRMInquiryForEditOutput { CRMInquiry = ObjectMapper.Map<CreateOrEditCRMInquiryDto>(crmInquiry) };

            if (output.CRMInquiry.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.CRMInquiry.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }

            if (output.CRMInquiry.PassportCountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.CRMInquiry.PassportCountryId);
                output.CountryName2 = _lookupCountry?.Name?.ToString();
            }

            if (output.CRMInquiry.DegreeLevelId != null)
            {
                var _lookupDegreeLevel = await _lookup_degreeLevelRepository.FirstOrDefaultAsync((int)output.CRMInquiry.DegreeLevelId);
                output.DegreeLevelName = _lookupDegreeLevel?.Name?.ToString();
            }

            if (output.CRMInquiry.SubjectId != null)
            {
                var _lookupSubject = await _lookup_subjectRepository.FirstOrDefaultAsync((int)output.CRMInquiry.SubjectId);
                output.SubjectName = _lookupSubject?.Name?.ToString();
            }

            if (output.CRMInquiry.SubjectAreaId != null)
            {
                var _lookupSubjectArea = await _lookup_subjectAreaRepository.FirstOrDefaultAsync((int)output.CRMInquiry.SubjectAreaId);
                output.SubjectAreaName = _lookupSubjectArea?.Name?.ToString();
            }

            if (output.CRMInquiry.OrganizationUnitId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.CRMInquiry.OrganizationUnitId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit?.DisplayName?.ToString();
            }

            if (output.CRMInquiry.LeadSourceId != null)
            {
                var _lookupLeadSource = await _lookup_leadSourceRepository.FirstOrDefaultAsync((int)output.CRMInquiry.LeadSourceId);
                output.LeadSourceName = _lookupLeadSource?.Name?.ToString();
            }

            if (output.CRMInquiry.TagId != null)
            {
                var _lookupTag = await _lookup_tagRepository.FirstOrDefaultAsync((int)output.CRMInquiry.TagId);
                output.TagName = _lookupTag?.Name?.ToString();
            }

            output.DocumentIdFileName = await GetBinaryFileName(crmInquiry.DocumentId);

            output.PictureIdFileName = await GetBinaryFileName(crmInquiry.PictureId);

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCRMInquiryDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CRMInquiries_Create)]
        protected virtual async Task Create(CreateOrEditCRMInquiryDto input)
        {
            var crmInquiry = ObjectMapper.Map<CRMInquiry>(input);

            if (AbpSession.TenantId != null)
            {
                crmInquiry.TenantId = (int)AbpSession.TenantId;
            }

            await _crmInquiryRepository.InsertAsync(crmInquiry);
            crmInquiry.DocumentId = await GetBinaryObjectFromCache(input.DocumentIdToken);
            crmInquiry.PictureId = await GetBinaryObjectFromCache(input.PictureIdToken);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMInquiries_Edit)]
        protected virtual async Task Update(CreateOrEditCRMInquiryDto input)
        {
            var crmInquiry = await _crmInquiryRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, crmInquiry);
            crmInquiry.DocumentId = await GetBinaryObjectFromCache(input.DocumentIdToken);
            crmInquiry.PictureId = await GetBinaryObjectFromCache(input.PictureIdToken);

        }

        [AbpAuthorize(AppPermissions.Pages_CRMInquiries_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _crmInquiryRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetCRMInquiriesToExcel(GetAllCRMInquiriesForExcelInput input)
        {

            var filteredCRMInquiries = _crmInquiryRepository.GetAll()
                        .Include(e => e.CountryFk)
                        .Include(e => e.PassportCountryFk)
                        .Include(e => e.DegreeLevelFk)
                        .Include(e => e.SubjectFk)
                        .Include(e => e.SubjectAreaFk)
                        .Include(e => e.OrganizationUnitFk)
                        .Include(e => e.LeadSourceFk)
                        .Include(e => e.TagFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FirstName.Contains(input.Filter) || e.LastName.Contains(input.Filter) || e.PhoneCode.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.SecondaryEmail.Contains(input.Filter) || e.ContactPreference.Contains(input.Filter) || e.Street.Contains(input.Filter) || e.City.Contains(input.Filter) || e.State.Contains(input.Filter) || e.PostalCode.Contains(input.Filter) || e.VisaType.Contains(input.Filter) || e.PreferedInTake.Contains(input.Filter) || e.DegreeTitle.Contains(input.Filter) || e.Institution.Contains(input.Filter) || e.Comments.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FirstNameFilter), e => e.FirstName.Contains(input.FirstNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LastNameFilter), e => e.LastName.Contains(input.LastNameFilter))
                        .WhereIf(input.MinDateofBirthFilter != null, e => e.DateofBirth >= input.MinDateofBirthFilter)
                        .WhereIf(input.MaxDateofBirthFilter != null, e => e.DateofBirth <= input.MaxDateofBirthFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneCodeFilter), e => e.PhoneCode.Contains(input.PhoneCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo.Contains(input.PhoneNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.Contains(input.EmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SecondaryEmailFilter), e => e.SecondaryEmail.Contains(input.SecondaryEmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ContactPreferenceFilter), e => e.ContactPreference.Contains(input.ContactPreferenceFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StreetFilter), e => e.Street.Contains(input.StreetFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CityFilter), e => e.City.Contains(input.CityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StateFilter), e => e.State.Contains(input.StateFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PostalCodeFilter), e => e.PostalCode.Contains(input.PostalCodeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.VisaTypeFilter), e => e.VisaType.Contains(input.VisaTypeFilter))
                        .WhereIf(input.MinVisaExpiryDateFilter != null, e => e.VisaExpiryDate >= input.MinVisaExpiryDateFilter)
                        .WhereIf(input.MaxVisaExpiryDateFilter != null, e => e.VisaExpiryDate <= input.MaxVisaExpiryDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PreferedInTakeFilter), e => e.PreferedInTake.Contains(input.PreferedInTakeFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DegreeTitleFilter), e => e.DegreeTitle.Contains(input.DegreeTitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.InstitutionFilter), e => e.Institution.Contains(input.InstitutionFilter))
                        .WhereIf(input.MinCourseStartDateFilter != null, e => e.CourseStartDate >= input.MinCourseStartDateFilter)
                        .WhereIf(input.MaxCourseStartDateFilter != null, e => e.CourseStartDate <= input.MaxCourseStartDateFilter)
                        .WhereIf(input.MinCourseEndDateFilter != null, e => e.CourseEndDate >= input.MinCourseEndDateFilter)
                        .WhereIf(input.MaxCourseEndDateFilter != null, e => e.CourseEndDate <= input.MaxCourseEndDateFilter)
                        .WhereIf(input.MinAcademicScoreFilter != null, e => e.AcademicScore >= input.MinAcademicScoreFilter)
                        .WhereIf(input.MaxAcademicScoreFilter != null, e => e.AcademicScore <= input.MaxAcademicScoreFilter)
                        .WhereIf(input.IsGpaFilter.HasValue && input.IsGpaFilter > -1, e => (input.IsGpaFilter == 1 && e.IsGpa) || (input.IsGpaFilter == 0 && !e.IsGpa))
                        .WhereIf(input.MinToeflFilter != null, e => e.Toefl >= input.MinToeflFilter)
                        .WhereIf(input.MaxToeflFilter != null, e => e.Toefl <= input.MaxToeflFilter)
                        .WhereIf(input.MinIeltsFilter != null, e => e.Ielts >= input.MinIeltsFilter)
                        .WhereIf(input.MaxIeltsFilter != null, e => e.Ielts <= input.MaxIeltsFilter)
                        .WhereIf(input.MinPteFilter != null, e => e.Pte >= input.MinPteFilter)
                        .WhereIf(input.MaxPteFilter != null, e => e.Pte <= input.MaxPteFilter)
                        .WhereIf(input.MinSat1Filter != null, e => e.Sat1 >= input.MinSat1Filter)
                        .WhereIf(input.MaxSat1Filter != null, e => e.Sat1 <= input.MaxSat1Filter)
                        .WhereIf(input.MinSat2Filter != null, e => e.Sat2 >= input.MinSat2Filter)
                        .WhereIf(input.MaxSat2Filter != null, e => e.Sat2 <= input.MaxSat2Filter)
                        .WhereIf(input.MinGreFilter != null, e => e.Gre >= input.MinGreFilter)
                        .WhereIf(input.MaxGreFilter != null, e => e.Gre <= input.MaxGreFilter)
                        .WhereIf(input.MinGMatFilter != null, e => e.GMat >= input.MinGMatFilter)
                        .WhereIf(input.MaxGMatFilter != null, e => e.GMat <= input.MaxGMatFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CommentsFilter), e => e.Comments.Contains(input.CommentsFilter))
                        .WhereIf(input.MinStatusFilter != null, e => e.Status >= input.MinStatusFilter)
                        .WhereIf(input.MaxStatusFilter != null, e => e.Status <= input.MaxStatusFilter)
                        .WhereIf(input.IsArchivedFilter.HasValue && input.IsArchivedFilter > -1, e => (input.IsArchivedFilter == 1 && e.IsArchived) || (input.IsArchivedFilter == 0 && !e.IsArchived))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CountryNameFilter), e => e.CountryFk != null && e.CountryFk.Name == input.CountryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CountryName2Filter), e => e.PassportCountryFk != null && e.PassportCountryFk.Name == input.CountryName2Filter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DegreeLevelNameFilter), e => e.DegreeLevelFk != null && e.DegreeLevelFk.Name == input.DegreeLevelNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectNameFilter), e => e.SubjectFk != null && e.SubjectFk.Name == input.SubjectNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectAreaNameFilter), e => e.SubjectAreaFk != null && e.SubjectAreaFk.Name == input.SubjectAreaNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.OrganizationUnitFk != null && e.OrganizationUnitFk.DisplayName == input.OrganizationUnitDisplayNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LeadSourceNameFilter), e => e.LeadSourceFk != null && e.LeadSourceFk.Name == input.LeadSourceNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TagNameFilter), e => e.TagFk != null && e.TagFk.Name == input.TagNameFilter);

            var query = (from o in filteredCRMInquiries
                         join o1 in _lookup_countryRepository.GetAll() on o.CountryId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_countryRepository.GetAll() on o.PassportCountryId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _lookup_degreeLevelRepository.GetAll() on o.DegreeLevelId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         join o4 in _lookup_subjectRepository.GetAll() on o.SubjectId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()

                         join o5 in _lookup_subjectAreaRepository.GetAll() on o.SubjectAreaId equals o5.Id into j5
                         from s5 in j5.DefaultIfEmpty()

                         join o6 in _lookup_organizationUnitRepository.GetAll() on o.OrganizationUnitId equals o6.Id into j6
                         from s6 in j6.DefaultIfEmpty()

                         join o7 in _lookup_leadSourceRepository.GetAll() on o.LeadSourceId equals o7.Id into j7
                         from s7 in j7.DefaultIfEmpty()

                         join o8 in _lookup_tagRepository.GetAll() on o.TagId equals o8.Id into j8
                         from s8 in j8.DefaultIfEmpty()

                         select new GetCRMInquiryForViewDto()
                         {
                             CRMInquiry = new CRMInquiryDto
                             {
                                 FirstName = o.FirstName,
                                 LastName = o.LastName,
                                 DateofBirth = o.DateofBirth,
                                 PhoneCode = o.PhoneCode,
                                 PhoneNo = o.PhoneNo,
                                 Email = o.Email,
                                 SecondaryEmail = o.SecondaryEmail,
                                 ContactPreference = o.ContactPreference,
                                 Street = o.Street,
                                 City = o.City,
                                 State = o.State,
                                 PostalCode = o.PostalCode,
                                 VisaType = o.VisaType,
                                 VisaExpiryDate = o.VisaExpiryDate,
                                 PreferedInTake = o.PreferedInTake,
                                 DegreeTitle = o.DegreeTitle,
                                 Institution = o.Institution,
                                 CourseStartDate = o.CourseStartDate,
                                 CourseEndDate = o.CourseEndDate,
                                 AcademicScore = o.AcademicScore,
                                 IsGpa = o.IsGpa,
                                 Toefl = o.Toefl,
                                 Ielts = o.Ielts,
                                 Pte = o.Pte,
                                 Sat1 = o.Sat1,
                                 Sat2 = o.Sat2,
                                 Gre = o.Gre,
                                 GMat = o.GMat,
                                 DocumentId = o.DocumentId,
                                 PictureId = o.PictureId,
                                 Comments = o.Comments,
                                 Status = o.Status,
                                 IsArchived = o.IsArchived,
                                 Id = o.Id
                             },
                             CountryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                             CountryName2 = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                             DegreeLevelName = s3 == null || s3.Name == null ? "" : s3.Name.ToString(),
                             SubjectName = s4 == null || s4.Name == null ? "" : s4.Name.ToString(),
                             SubjectAreaName = s5 == null || s5.Name == null ? "" : s5.Name.ToString(),
                             OrganizationUnitDisplayName = s6 == null || s6.DisplayName == null ? "" : s6.DisplayName.ToString(),
                             LeadSourceName = s7 == null || s7.Name == null ? "" : s7.Name.ToString(),
                             TagName = s8 == null || s8.Name == null ? "" : s8.Name.ToString()
                         });

            var crmInquiryListDtos = await query.ToListAsync();

            return _crmInquiriesExcelExporter.ExportToFile(crmInquiryListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_CRMInquiries)]
        public async Task<List<CRMInquiryCountryLookupTableDto>> GetAllCountryForTableDropdown()
        {
            return await _lookup_countryRepository.GetAll()
                .Select(country => new CRMInquiryCountryLookupTableDto
                {
                    Id = country.Id,
                    DisplayName = country == null || country.Name == null ? "" : country.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CRMInquiries)]
        public async Task<List<CRMInquiryDegreeLevelLookupTableDto>> GetAllDegreeLevelForTableDropdown()
        {
            return await _lookup_degreeLevelRepository.GetAll()
                .Select(degreeLevel => new CRMInquiryDegreeLevelLookupTableDto
                {
                    Id = degreeLevel.Id,
                    DisplayName = degreeLevel == null || degreeLevel.Name == null ? "" : degreeLevel.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CRMInquiries)]
        public async Task<List<CRMInquirySubjectLookupTableDto>> GetAllSubjectForTableDropdown()
        {
            return await _lookup_subjectRepository.GetAll()
                .Select(subject => new CRMInquirySubjectLookupTableDto
                {
                    Id = subject.Id,
                    DisplayName = subject == null || subject.Name == null ? "" : subject.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CRMInquiries)]
        public async Task<List<CRMInquirySubjectAreaLookupTableDto>> GetAllSubjectAreaForTableDropdown()
        {
            return await _lookup_subjectAreaRepository.GetAll()
                .Select(subjectArea => new CRMInquirySubjectAreaLookupTableDto
                {
                    Id = subjectArea.Id,
                    DisplayName = subjectArea == null || subjectArea.Name == null ? "" : subjectArea.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CRMInquiries)]
        public async Task<List<CRMInquiryOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForTableDropdown()
        {
            return await _lookup_organizationUnitRepository.GetAll()
                .Select(organizationUnit => new CRMInquiryOrganizationUnitLookupTableDto
                {
                    Id = organizationUnit.Id,
                    DisplayName = organizationUnit == null || organizationUnit.DisplayName == null ? "" : organizationUnit.DisplayName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CRMInquiries)]
        public async Task<List<CRMInquiryLeadSourceLookupTableDto>> GetAllLeadSourceForTableDropdown()
        {
            return await _lookup_leadSourceRepository.GetAll()
                .Select(leadSource => new CRMInquiryLeadSourceLookupTableDto
                {
                    Id = leadSource.Id,
                    DisplayName = leadSource == null || leadSource.Name == null ? "" : leadSource.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_CRMInquiries)]
        public async Task<List<CRMInquiryTagLookupTableDto>> GetAllTagForTableDropdown()
        {
            return await _lookup_tagRepository.GetAll()
                .Select(tag => new CRMInquiryTagLookupTableDto
                {
                    Id = tag.Id,
                    DisplayName = tag == null || tag.Name == null ? "" : tag.Name.ToString()
                }).ToListAsync();
        }

        private async Task<Guid?> GetBinaryObjectFromCache(string fileToken)
        {
            if (fileToken.IsNullOrWhiteSpace())
            {
                return null;
            }

            var fileCache = _tempFileCacheManager.GetFileInfo(fileToken);

            if (fileCache == null)
            {
                throw new UserFriendlyException("There is no such file with the token: " + fileToken);
            }

            var storedFile = new BinaryObject(AbpSession.TenantId, fileCache.File, fileCache.FileName);
            await _binaryObjectManager.SaveAsync(storedFile);

            return storedFile.Id;
        }

        private async Task<string> GetBinaryFileName(Guid? fileId)
        {
            if (!fileId.HasValue)
            {
                return null;
            }

            var file = await _binaryObjectManager.GetOrNullAsync(fileId.Value);
            return file?.Description;
        }

        [AbpAuthorize(AppPermissions.Pages_CRMInquiries_Edit)]
        public async Task RemoveDocumentIdFile(EntityDto<long> input)
        {
            var crmInquiry = await _crmInquiryRepository.FirstOrDefaultAsync(input.Id);
            if (crmInquiry == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!crmInquiry.DocumentId.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(crmInquiry.DocumentId.Value);
            crmInquiry.DocumentId = null;
        }

        [AbpAuthorize(AppPermissions.Pages_CRMInquiries_Edit)]
        public async Task RemovePictureIdFile(EntityDto<long> input)
        {
            var crmInquiry = await _crmInquiryRepository.FirstOrDefaultAsync(input.Id);
            if (crmInquiry == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!crmInquiry.PictureId.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(crmInquiry.PictureId.Value);
            crmInquiry.PictureId = null;
        }

    }
}