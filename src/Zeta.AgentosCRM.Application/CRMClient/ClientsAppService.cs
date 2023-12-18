using Zeta.AgentosCRM.CRMSetup.Countries;
using Zeta.AgentosCRM.Authorization.Users;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMSetup;
using Zeta.AgentosCRM.CRMSetup.LeadSource; 
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMClient.Exporting;
using Zeta.AgentosCRM.CRMClient.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Zeta.AgentosCRM.CRMAgent;

namespace Zeta.AgentosCRM.CRMClient
{
    [AbpAuthorize(AppPermissions.Pages_Clients)]
    public class ClientsAppService : AgentosCRMAppServiceBase, IClientsAppService
    {
        private readonly IRepository<Client, long> _clientRepository;
        private readonly IClientsExcelExporter _clientsExcelExporter;
        private readonly IRepository<Country, int> _lookup_countryRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<Agent, long> _lookup_agentRepository;
        private readonly IRepository<BinaryObject, Guid> _lookup_binaryObjectRepository;
        private readonly IRepository<DegreeLevel, int> _lookup_degreeLevelRepository;
        private readonly IRepository<SubjectArea, int> _lookup_subjectAreaRepository;
        private readonly IRepository<LeadSource, int> _lookup_leadSourceRepository;

        public ClientsAppService(IRepository<Client, long> clientRepository, IClientsExcelExporter clientsExcelExporter, IRepository<Country, int> lookup_countryRepository, IRepository<User, long> lookup_userRepository, IRepository<BinaryObject, Guid> lookup_binaryObjectRepository, IRepository<DegreeLevel, int> lookup_degreeLevelRepository, IRepository<SubjectArea, int> lookup_subjectAreaRepository, IRepository<LeadSource, int> lookup_leadSourceRepository, IRepository<Agent, long> lookup_agentRepository)
        {
            _clientRepository = clientRepository;
            _clientsExcelExporter = clientsExcelExporter;
            _lookup_countryRepository = lookup_countryRepository;
            _lookup_userRepository = lookup_userRepository;
            _lookup_binaryObjectRepository = lookup_binaryObjectRepository;
            _lookup_degreeLevelRepository = lookup_degreeLevelRepository;
            _lookup_subjectAreaRepository = lookup_subjectAreaRepository;
            _lookup_leadSourceRepository = lookup_leadSourceRepository;
            _lookup_agentRepository = lookup_agentRepository;
        }

        public async Task<PagedResultDto<GetClientForViewDto>> GetAll(GetAllClientsInput input)
        {

            var filteredClients = _clientRepository.GetAll()
                        .Include(e => e.CountryFk)
                        .Include(e => e.AssigneeFk)
                        .Include(e => e.ProfilePictureFk)
                        .Include(e => e.HighestQualificationFk)
                        .Include(e => e.StudyAreaFk)
                        .Include(e => e.LeadSourceFk)
                        .Include(e => e.PassportCountryFk)
                        .Include(e => e.AgentFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FirstName.Contains(input.Filter) || e.LastName.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.PhoneCode.Contains(input.Filter) || e.University.Contains(input.Filter) || e.Street.Contains(input.Filter) || e.City.Contains(input.Filter) || e.State.Contains(input.Filter) || e.ZipCode.Contains(input.Filter) || e.PassportNo.Contains(input.Filter) || e.VisaType.Contains(input.Filter) || e.AddedFrom.Contains(input.Filter) || e.SecondaryEmail.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FirstNameFilter), e => e.FirstName.Contains(input.FirstNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LastNameFilter), e => e.LastName.Contains(input.LastNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.Contains(input.EmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo.Contains(input.PhoneNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneCodeFilter), e => e.PhoneNo.Contains(input.PhoneCodeFilter))
                        .WhereIf(input.MinDateofBirthFilter != null, e => e.DateofBirth >= input.MinDateofBirthFilter)
                        .WhereIf(input.MaxDateofBirthFilter != null, e => e.DateofBirth <= input.MaxDateofBirthFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityFilter), e => e.University.Contains(input.UniversityFilter))
                        .WhereIf(input.MinRatingFilter != null, e => e.Rating >= input.MinRatingFilter)
                        .WhereIf(input.MaxRatingFilter != null, e => e.Rating <= input.MaxRatingFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CountryNameFilter), e => e.CountryFk != null && e.CountryFk.Name == input.CountryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.AssigneeFk != null && e.AssigneeFk.Name == input.UserNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BinaryObjectDescriptionFilter), e => e.ProfilePictureFk != null && e.ProfilePictureFk.Description == input.BinaryObjectDescriptionFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DegreeLevelNameFilter), e => e.HighestQualificationFk != null && e.HighestQualificationFk.Name == input.DegreeLevelNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectAreaNameFilter), e => e.StudyAreaFk != null && e.StudyAreaFk.Name == input.SubjectAreaNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LeadSourceNameFilter), e => e.LeadSourceFk != null && e.LeadSourceFk.Name == input.LeadSourceNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PassportCountryFilter), e => e.PassportCountryFk != null && e.PassportCountryFk.Name == input.PassportCountryFilter)
                         .WhereIf(input.AgentIdFilter.HasValue, e => false || e.AgentId == input.AgentIdFilter.Value);

            var pagedAndFilteredClients = filteredClients
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var clients = from o in pagedAndFilteredClients
                          join o1 in _lookup_countryRepository.GetAll() on o.CountryId equals o1.Id into j1
                          from s1 in j1.DefaultIfEmpty()

                          join o2 in _lookup_userRepository.GetAll() on o.AssigneeId equals o2.Id into j2
                          from s2 in j2.DefaultIfEmpty()

                          join o3 in _lookup_binaryObjectRepository.GetAll() on o.ProfilePictureId equals o3.Id into j3
                          from s3 in j3.DefaultIfEmpty()

                          join o4 in _lookup_degreeLevelRepository.GetAll() on o.HighestQualificationId equals o4.Id into j4
                          from s4 in j4.DefaultIfEmpty()

                          join o5 in _lookup_subjectAreaRepository.GetAll() on o.StudyAreaId equals o5.Id into j5
                          from s5 in j5.DefaultIfEmpty()

                          join o6 in _lookup_leadSourceRepository.GetAll() on o.LeadSourceId equals o6.Id into j6
                          from s6 in j6.DefaultIfEmpty()

                          join o7 in _lookup_countryRepository.GetAll() on o.PassportCountryId equals o7.Id into j7
                          from s7 in j7.DefaultIfEmpty()

                          join o8 in _lookup_agentRepository.GetAll() on o.AgentId equals o8.Id into j8
                          from s8 in j8.DefaultIfEmpty()

                          select new
                          {

                              o.FirstName,
                              o.LastName,
                              o.Email,
                              o.PhoneNo,
                              o.PhoneCode,
                              o.DateofBirth,
                              o.ContactPreferences,
                              o.University,
                              o.Street,
                              o.City,
                              o.State,
                              o.ZipCode,
                              o.PreferedIntake,
                              o.PassportNo,
                              o.VisaType,
                              o.VisaExpiryDate,
                              o.Rating,
                              o.ClientPortal,
                              Id = o.Id,
                              o.ProfilePictureId,
                              CountryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                              UserName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                              BinaryObjectDescription = s3 == null || s3.Description == null ? "" : s3.Description.ToString(),
                              ImageBytes = s3 == null || s3.Bytes == null ? "" : Convert.ToBase64String(s3.Bytes),
                              DegreeLevelName = s4 == null || s4.Name == null ? "" : s4.Name.ToString(),
                              SubjectAreaName = s5 == null || s5.Name == null ? "" : s5.Name.ToString(),
                              LeadSourceName = s6 == null || s6.Name == null ? "" : s6.Name.ToString(),
                              PassportCountry = s7 == null || s7.Name == null ? "" : s7.Name.ToString(),
                              AgentName = s8 == null || s8.Name == null ? "" : s8.Name.ToString()
                          };

            var totalCount = await filteredClients.CountAsync();

            var dbList = await clients.ToListAsync();
            var results = new List<GetClientForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetClientForViewDto()
                {
                    Client = new ClientDto
                    {

                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        Email = o.Email,
                        PhoneNo = o.PhoneNo,
                        PhoneCode = o.PhoneCode,
                        DateofBirth = o.DateofBirth,
                        ContactPreferences = o.ContactPreferences,
                        University = o.University,
                        Street = o.Street,
                        City = o.City,
                        State = o.State,
                        ZipCode = o.ZipCode,
                        PreferedIntake = o.PreferedIntake,
                        PassportNo = o.PassportNo,
                        VisaType = o.VisaType,
                        VisaExpiryDate = o.VisaExpiryDate,
                        Rating = o.Rating,
                        ClientPortal = o.ClientPortal,
                        Id = o.Id,
                        ProfilePictureId=o.ProfilePictureId,
                    },
                    CountryName = o.CountryName,
                    UserName = o.UserName,
                    BinaryObjectDescription = o.BinaryObjectDescription,
                    DegreeLevelName = o.DegreeLevelName,
                    SubjectAreaName = o.SubjectAreaName,
                    LeadSourceName = o.LeadSourceName,
                    PassportCountry = o.PassportCountry,
                    AgentName = o.AgentName,
                    ImageBytes=o.ImageBytes,
                };

                results.Add(res);
            }

            return new PagedResultDto<GetClientForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetClientForViewDto> GetClientForView(long id)
        {
            var client = await _clientRepository.GetAsync(id);

            var output = new GetClientForViewDto { Client = ObjectMapper.Map<ClientDto>(client) };

            if (output.Client.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.Client.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }

            if (output.Client.AssigneeId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.Client.AssigneeId);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            if (output.Client.ProfilePictureId != null)
            {
                var _lookupBinaryObject = await _lookup_binaryObjectRepository.FirstOrDefaultAsync((Guid)output.Client.ProfilePictureId);
                output.BinaryObjectDescription = _lookupBinaryObject?.Description?.ToString();
            }

            if (output.Client.HighestQualificationId != null)
            {
                var _lookupDegreeLevel = await _lookup_degreeLevelRepository.FirstOrDefaultAsync((int)output.Client.HighestQualificationId);
                output.DegreeLevelName = _lookupDegreeLevel?.Name?.ToString();
            }

            if (output.Client.StudyAreaId != null)
            {
                var _lookupSubjectArea = await _lookup_subjectAreaRepository.FirstOrDefaultAsync((int)output.Client.StudyAreaId);
                output.SubjectAreaName = _lookupSubjectArea?.Name?.ToString();
            }

            if (output.Client.LeadSourceId != null)
            {
                var _lookupLeadSource = await _lookup_leadSourceRepository.FirstOrDefaultAsync((int)output.Client.LeadSourceId);
                output.LeadSourceName = _lookupLeadSource?.Name?.ToString();
            }

            if (output.Client.PassportCountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.Client.PassportCountryId);
                output.PassportCountry = _lookupCountry?.Name?.ToString();
            }

            if (output.Client.AgentId != null)
            {
                var _lookupAgent = await _lookup_agentRepository.FirstOrDefaultAsync((int)output.Client.AgentId);
                output.PassportCountry = _lookupAgent?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Clients_Edit)]
        public async Task<GetClientForEditOutput> GetClientForEdit(EntityDto<long> input)
        {
            var client = await _clientRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetClientForEditOutput { Client = ObjectMapper.Map<CreateOrEditClientDto>(client) };

            if (output.Client.CountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.Client.CountryId);
                output.CountryName = _lookupCountry?.Name?.ToString();
            }

            if (output.Client.AssigneeId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.Client.AssigneeId);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            if (output.Client.ProfilePictureId != null)
            {
                var _lookupBinaryObject = await _lookup_binaryObjectRepository.FirstOrDefaultAsync((Guid)output.Client.ProfilePictureId);
                output.BinaryObjectDescription = _lookupBinaryObject?.Description?.ToString();
            }

            if (output.Client.HighestQualificationId != null)
            {
                var _lookupDegreeLevel = await _lookup_degreeLevelRepository.FirstOrDefaultAsync((int)output.Client.HighestQualificationId);
                output.DegreeLevelName = _lookupDegreeLevel?.Name?.ToString();
            }

            if (output.Client.StudyAreaId != null)
            {
                var _lookupSubjectArea = await _lookup_subjectAreaRepository.FirstOrDefaultAsync((int)output.Client.StudyAreaId);
                output.SubjectAreaName = _lookupSubjectArea?.Name?.ToString();
            }

            if (output.Client.LeadSourceId != null)
            {
                var _lookupLeadSource = await _lookup_leadSourceRepository.FirstOrDefaultAsync((int)output.Client.LeadSourceId);
                output.LeadSourceName = _lookupLeadSource?.Name?.ToString();
            }

            if (output.Client.PassportCountryId != null)
            {
                var _lookupCountry = await _lookup_countryRepository.FirstOrDefaultAsync((int)output.Client.PassportCountryId);
                output.PassportCountry = _lookupCountry?.Name?.ToString();
            }

            if (output.Client.AgentId != null)
            {
                var _lookupAgent = await _lookup_agentRepository.FirstOrDefaultAsync((int)output.Client.AgentId);
                output.PassportCountry = _lookupAgent?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditClientDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Clients_Create)]
        protected virtual async Task Create(CreateOrEditClientDto input)
        {
            var client = ObjectMapper.Map<Client>(input);

            if (AbpSession.TenantId != null)
            {
                client.TenantId = (int)AbpSession.TenantId;
            }

            await _clientRepository.InsertAsync(client);

        }

        [AbpAuthorize(AppPermissions.Pages_Clients_Edit)]
        protected virtual async Task Update(CreateOrEditClientDto input)
        { 
            var client = await _clientRepository.FirstOrDefaultAsync((long)input.Id);
            input.ProfilePictureId = client.ProfilePictureId;
            ObjectMapper.Map(input, client);

        }

        [AbpAuthorize(AppPermissions.Pages_Clients_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _clientRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetClientsToExcel(GetAllClientsForExcelInput input)
        {

            var filteredClients = _clientRepository.GetAll()
                        .Include(e => e.CountryFk)
                        .Include(e => e.AssigneeFk)
                        .Include(e => e.ProfilePictureFk)
                        .Include(e => e.HighestQualificationFk)
                        .Include(e => e.StudyAreaFk)
                        .Include(e => e.LeadSourceFk)
                        .Include(e => e.PassportCountryFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FirstName.Contains(input.Filter) || e.LastName.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.PhoneCode.Contains(input.Filter) || e.University.Contains(input.Filter) || e.Street.Contains(input.Filter) || e.City.Contains(input.Filter) || e.State.Contains(input.Filter) || e.ZipCode.Contains(input.Filter) || e.PassportNo.Contains(input.Filter) || e.VisaType.Contains(input.Filter) || e.AddedFrom.Contains(input.Filter) || e.SecondaryEmail.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FirstNameFilter), e => e.FirstName.Contains(input.FirstNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LastNameFilter), e => e.LastName.Contains(input.LastNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.Contains(input.EmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo.Contains(input.PhoneNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneCodeFilter), e => e.PhoneNo.Contains(input.PhoneCodeFilter))
                        .WhereIf(input.MinDateofBirthFilter != null, e => e.DateofBirth >= input.MinDateofBirthFilter)
                        .WhereIf(input.MaxDateofBirthFilter != null, e => e.DateofBirth <= input.MaxDateofBirthFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UniversityFilter), e => e.University.Contains(input.UniversityFilter))
                        .WhereIf(input.MinRatingFilter != null, e => e.Rating >= input.MinRatingFilter)
                        .WhereIf(input.MaxRatingFilter != null, e => e.Rating <= input.MaxRatingFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CountryNameFilter), e => e.CountryFk != null && e.CountryFk.Name == input.CountryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.AssigneeFk != null && e.AssigneeFk.Name == input.UserNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BinaryObjectDescriptionFilter), e => e.ProfilePictureFk != null && e.ProfilePictureFk.Description == input.BinaryObjectDescriptionFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DegreeLevelNameFilter), e => e.HighestQualificationFk != null && e.HighestQualificationFk.Name == input.DegreeLevelNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectAreaNameFilter), e => e.StudyAreaFk != null && e.StudyAreaFk.Name == input.SubjectAreaNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LeadSourceNameFilter), e => e.LeadSourceFk != null && e.LeadSourceFk.Name == input.LeadSourceNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PassportCountryFilter), e => e.PassportCountryFk != null && e.PassportCountryFk.Name == input.PassportCountryFilter);

            var query = (from o in filteredClients
                         join o1 in _lookup_countryRepository.GetAll() on o.CountryId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_userRepository.GetAll() on o.AssigneeId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _lookup_binaryObjectRepository.GetAll() on o.ProfilePictureId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         join o4 in _lookup_degreeLevelRepository.GetAll() on o.HighestQualificationId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()

                         join o5 in _lookup_subjectAreaRepository.GetAll() on o.StudyAreaId equals o5.Id into j5
                         from s5 in j5.DefaultIfEmpty()

                         join o6 in _lookup_leadSourceRepository.GetAll() on o.LeadSourceId equals o6.Id into j6
                         from s6 in j6.DefaultIfEmpty()

                         join o7 in _lookup_countryRepository.GetAll() on o.PassportCountryId equals o7.Id into j7
                         from s7 in j7.DefaultIfEmpty()

                         select new GetClientForViewDto()
                         {
                             Client = new ClientDto
                             {
                                 FirstName = o.FirstName,
                                 LastName = o.LastName,
                                 Email = o.Email,
                                 PhoneNo = o.PhoneNo,
                                 PhoneCode = o.PhoneCode,
                                 DateofBirth = o.DateofBirth,
                                 ContactPreferences = o.ContactPreferences,
                                 University = o.University,
                                 Street = o.Street,
                                 City = o.City,
                                 State = o.State,
                                 ZipCode = o.ZipCode,
                                 PreferedIntake = o.PreferedIntake,
                                 PassportNo = o.PassportNo,
                                 VisaType = o.VisaType,
                                 VisaExpiryDate = o.VisaExpiryDate,
                                 Rating = o.Rating,
                                 ClientPortal = o.ClientPortal,
                                 Id = o.Id
                             },
                             CountryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                             UserName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                             BinaryObjectDescription = s3 == null || s3.Description == null ? "" : s3.Description.ToString(),
                             DegreeLevelName = s4 == null || s4.Name == null ? "" : s4.Name.ToString(),
                             SubjectAreaName = s5 == null || s5.Name == null ? "" : s5.Name.ToString(),
                             LeadSourceName = s6 == null || s6.Name == null ? "" : s6.Name.ToString(),
                             PassportCountry = s7 == null || s7.Name == null ? "" : s7.Name.ToString()
                         });

            var clientListDtos = await query.ToListAsync();

            return _clientsExcelExporter.ExportToFile(clientListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Clients)]
        public async Task<List<ClientCountryLookupTableDto>> GetAllCountryForTableDropdown()
        {
            return await _lookup_countryRepository.GetAll()
                .Select(country => new ClientCountryLookupTableDto
                {
                    Id = country.Id,
                    DisplayName = country == null || country.Name == null ? "" : country.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Clients)]
        public async Task<List<ClientUserLookupTableDto>> GetAllUserForTableDropdown()
        {
            return await _lookup_userRepository.GetAll()
                .Select(user => new ClientUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user == null || user.Name == null ? "" : user.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Clients)]
        public async Task<PagedResultDto<ClientBinaryObjectLookupTableDto>> GetAllBinaryObjectForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_binaryObjectRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Description != null && e.Description.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var binaryObjectList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ClientBinaryObjectLookupTableDto>();
            foreach (var binaryObject in binaryObjectList)
            {
                lookupTableDtoList.Add(new ClientBinaryObjectLookupTableDto
                {
                    Id = binaryObject.Id.ToString(),
                    DisplayName = binaryObject.Description?.ToString()
                });
            }

            return new PagedResultDto<ClientBinaryObjectLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
        [AbpAuthorize(AppPermissions.Pages_Clients)]
        public async Task<List<ClientDegreeLevelLookupTableDto>> GetAllDegreeLevelForTableDropdown()
        {
            return await _lookup_degreeLevelRepository.GetAll()
                .Select(degreeLevel => new ClientDegreeLevelLookupTableDto
                {
                    Id = degreeLevel.Id,
                    DisplayName = degreeLevel == null || degreeLevel.Name == null ? "" : degreeLevel.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Clients)]
        public async Task<List<ClientSubjectAreaLookupTableDto>> GetAllSubjectAreaForTableDropdown()
        {
            return await _lookup_subjectAreaRepository.GetAll()
                .Select(subjectArea => new ClientSubjectAreaLookupTableDto
                {
                    Id = subjectArea.Id,
                    DisplayName = subjectArea == null || subjectArea.Name == null ? "" : subjectArea.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Clients)]
        public async Task<List<ClientLeadSourceLookupTableDto>> GetAllLeadSourceForTableDropdown()
        {
            return await _lookup_leadSourceRepository.GetAll()
                .Select(leadSource => new ClientLeadSourceLookupTableDto
                {
                    Id = leadSource.Id,
                    DisplayName = leadSource == null || leadSource.Name == null ? "" : leadSource.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Clients)]
        public async Task<List<ClientAgentLookupTableDto>> GetAllAgentForTableDropdown()
        {
            return await _lookup_agentRepository.GetAll()
                .Select(agent => new ClientAgentLookupTableDto
                {
                    Id = agent.Id,
                    DisplayName = agent == null || agent.Name == null ? "" : agent.Name.ToString()
                }).ToListAsync();
        }

    }
}