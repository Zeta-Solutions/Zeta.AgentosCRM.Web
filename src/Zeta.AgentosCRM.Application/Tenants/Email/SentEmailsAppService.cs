using Zeta.AgentosCRM.CRMSetup.Email;
using Zeta.AgentosCRM.Tenants.Email.Configuration;
using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.CRMApplications;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.Tenants.Email.Dtos;

namespace Zeta.AgentosCRM.Tenants.Email
{
    [AbpAuthorize(AppPermissions.Pages_SentEmails)]
    public class SentEmailsAppService : AgentosCRMAppServiceBase, ISentEmailsAppService
    {
        private readonly IRepository<SentEmail, long> _sentEmailRepository;
        private readonly IRepository<EmailTemplate, int> _lookup_emailTemplateRepository;
        private readonly IRepository<EmailConfiguration, long> _lookup_emailConfigurationRepository;
        private readonly IRepository<Client, long> _lookup_clientRepository;
        private readonly IRepository<Application, long> _lookup_applicationRepository;

        public SentEmailsAppService(IRepository<SentEmail, long> sentEmailRepository, IRepository<EmailTemplate, int> lookup_emailTemplateRepository, IRepository<EmailConfiguration, long> lookup_emailConfigurationRepository, IRepository<Client, long> lookup_clientRepository, IRepository<Application, long> lookup_applicationRepository)
        {
            _sentEmailRepository = sentEmailRepository;
            _lookup_emailTemplateRepository = lookup_emailTemplateRepository;
            _lookup_emailConfigurationRepository = lookup_emailConfigurationRepository;
            _lookup_clientRepository = lookup_clientRepository;
            _lookup_applicationRepository = lookup_applicationRepository;

        }

        public async Task<PagedResultDto<GetSentEmailForViewDto>> GetAll(GetAllSentEmailsInput input)
        {

            var filteredSentEmails = _sentEmailRepository.GetAll()
                        .Include(e => e.EmailTemplateFk)
                        .Include(e => e.EmailConfigurationFk)
                        .Include(e => e.ClientFk)
                        .Include(e => e.ApplicationFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Title.Contains(input.Filter) || e.Subject.Contains(input.Filter) || e.FromEmail.Contains(input.Filter) || e.ToEmail.Contains(input.Filter) || e.CCEmail.Contains(input.Filter) || e.BCCEmail.Contains(input.Filter) || e.EmailBody.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.Contains(input.TitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SubjectFilter), e => e.Subject.Contains(input.SubjectFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FromEmailFilter), e => e.FromEmail.Contains(input.FromEmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ToEmailFilter), e => e.ToEmail.Contains(input.ToEmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CCEmailFilter), e => e.CCEmail.Contains(input.CCEmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BCCEmailFilter), e => e.BCCEmail.Contains(input.BCCEmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailBodyFilter), e => e.EmailBody.Contains(input.EmailBodyFilter))
                        .WhereIf(input.IsSentFilter.HasValue && input.IsSentFilter > -1, e => input.IsSentFilter == 1 && e.IsSent || input.IsSentFilter == 0 && !e.IsSent)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailTemplateTitleFilter), e => e.EmailTemplateFk != null && e.EmailTemplateFk.Title == input.EmailTemplateTitleFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailConfigurationNameFilter), e => e.EmailConfigurationFk != null && e.EmailConfigurationFk.Name == input.EmailConfigurationNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ClientFirstNameFilter), e => e.ClientFk != null && e.ClientFk.FirstName == input.ClientFirstNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ApplicationNameFilter), e => e.ApplicationFk != null && e.ApplicationFk.Name == input.ApplicationNameFilter);

            var pagedAndFilteredSentEmails = filteredSentEmails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var sentEmails = from o in pagedAndFilteredSentEmails
                             join o1 in _lookup_emailTemplateRepository.GetAll() on o.EmailTemplateId equals o1.Id into j1
                             from s1 in j1.DefaultIfEmpty()

                             join o2 in _lookup_emailConfigurationRepository.GetAll() on o.EmailConfigurationId equals o2.Id into j2
                             from s2 in j2.DefaultIfEmpty()

                             join o3 in _lookup_clientRepository.GetAll() on o.ClientId equals o3.Id into j3
                             from s3 in j3.DefaultIfEmpty()

                             join o4 in _lookup_applicationRepository.GetAll() on o.ApplicationId equals o4.Id into j4
                             from s4 in j4.DefaultIfEmpty()

                             select new
                             {

                                 o.Title,
                                 o.Subject,
                                 o.FromEmail,
                                 o.ToEmail,
                                 o.CCEmail,
                                 o.BCCEmail,
                                 o.EmailBody,
                                 o.IsSent,
                                 o.Id,
                                 EmailTemplateTitle = s1 == null || s1.Title == null ? "" : s1.Title.ToString(),
                                 EmailConfigurationName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                                 ClientFirstName = s3 == null || s3.FirstName == null ? "" : s3.FirstName.ToString(),
                                 ApplicationName = s4 == null || s4.Name == null ? "" : s4.Name.ToString()
                             };

            var totalCount = await filteredSentEmails.CountAsync();

            var dbList = await sentEmails.ToListAsync();
            var results = new List<GetSentEmailForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetSentEmailForViewDto()
                {
                    SentEmail = new SentEmailDto
                    {

                        Title = o.Title,
                        Subject = o.Subject,
                        FromEmail = o.FromEmail,
                        ToEmail = o.ToEmail,
                        CCEmail = o.CCEmail,
                        BCCEmail = o.BCCEmail,
                        EmailBody = o.EmailBody,
                        IsSent = o.IsSent,
                        Id = o.Id,
                    },
                    EmailTemplateTitle = o.EmailTemplateTitle,
                    EmailConfigurationName = o.EmailConfigurationName,
                    ClientFirstName = o.ClientFirstName,
                    ApplicationName = o.ApplicationName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetSentEmailForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetSentEmailForViewDto> GetSentEmailForView(long id)
        {
            var sentEmail = await _sentEmailRepository.GetAsync(id);

            var output = new GetSentEmailForViewDto { SentEmail = ObjectMapper.Map<SentEmailDto>(sentEmail) };

            if (output.SentEmail.EmailTemplateId != null)
            {
                var _lookupEmailTemplate = await _lookup_emailTemplateRepository.FirstOrDefaultAsync((int)output.SentEmail.EmailTemplateId);
                output.EmailTemplateTitle = _lookupEmailTemplate?.Title?.ToString();
            }

            if (output.SentEmail.EmailConfigurationId != null)
            {
                var _lookupEmailConfiguration = await _lookup_emailConfigurationRepository.FirstOrDefaultAsync((long)output.SentEmail.EmailConfigurationId);
                output.EmailConfigurationName = _lookupEmailConfiguration?.Name?.ToString();
            }

            if (output.SentEmail.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.SentEmail.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            if (output.SentEmail.ApplicationId != null)
            {
                var _lookupApplication = await _lookup_applicationRepository.FirstOrDefaultAsync((long)output.SentEmail.ApplicationId);
                output.ApplicationName = _lookupApplication?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_SentEmails_Edit)]
        public async Task<GetSentEmailForEditOutput> GetSentEmailForEdit(EntityDto<long> input)
        {
            var sentEmail = await _sentEmailRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSentEmailForEditOutput { SentEmail = ObjectMapper.Map<CreateOrEditSentEmailDto>(sentEmail) };

            if (output.SentEmail.EmailTemplateId != null)
            {
                var _lookupEmailTemplate = await _lookup_emailTemplateRepository.FirstOrDefaultAsync((int)output.SentEmail.EmailTemplateId);
                output.EmailTemplateTitle = _lookupEmailTemplate?.Title?.ToString();
            }

            if (output.SentEmail.EmailConfigurationId != null)
            {
                var _lookupEmailConfiguration = await _lookup_emailConfigurationRepository.FirstOrDefaultAsync((long)output.SentEmail.EmailConfigurationId);
                output.EmailConfigurationName = _lookupEmailConfiguration?.Name?.ToString();
            }

            if (output.SentEmail.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((long)output.SentEmail.ClientId);
                output.ClientFirstName = _lookupClient?.FirstName?.ToString();
            }

            if (output.SentEmail.ApplicationId != null)
            {
                var _lookupApplication = await _lookup_applicationRepository.FirstOrDefaultAsync((long)output.SentEmail.ApplicationId);
                output.ApplicationName = _lookupApplication?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditSentEmailDto input)
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

        [AbpAuthorize(AppPermissions.Pages_SentEmails_Create)]
        protected virtual async Task Create(CreateOrEditSentEmailDto input)
        {
            var sentEmail = ObjectMapper.Map<SentEmail>(input);

            if (AbpSession.TenantId != null)
            {
                sentEmail.TenantId = (int)AbpSession.TenantId;
            }

            await _sentEmailRepository.InsertAsync(sentEmail);

        }

        [AbpAuthorize(AppPermissions.Pages_SentEmails_Edit)]
        protected virtual async Task Update(CreateOrEditSentEmailDto input)
        {
            var sentEmail = await _sentEmailRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, sentEmail);

        }

        [AbpAuthorize(AppPermissions.Pages_SentEmails_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _sentEmailRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_SentEmails)]
        public async Task<List<SentEmailEmailTemplateLookupTableDto>> GetAllEmailTemplateForTableDropdown()
        {
            return await _lookup_emailTemplateRepository.GetAll()
                .Select(emailTemplate => new SentEmailEmailTemplateLookupTableDto
                {
                    Id = emailTemplate.Id,
                    DisplayName = emailTemplate == null || emailTemplate.Title == null ? "" : emailTemplate.Title.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_SentEmails)]
        public async Task<List<SentEmailEmailConfigurationLookupTableDto>> GetAllEmailConfigurationForTableDropdown()
        {
            return await _lookup_emailConfigurationRepository.GetAll()
                .Select(emailConfiguration => new SentEmailEmailConfigurationLookupTableDto
                {
                    Id = emailConfiguration.Id,
                    DisplayName = emailConfiguration == null || emailConfiguration.Name == null ? "" : emailConfiguration.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_SentEmails)]
        public async Task<List<SentEmailClientLookupTableDto>> GetAllClientForTableDropdown()
        {
            return await _lookup_clientRepository.GetAll()
                .Select(client => new SentEmailClientLookupTableDto
                {
                    Id = client.Id,
                    DisplayName = client == null || client.FirstName == null ? "" : client.FirstName.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_SentEmails)]
        public async Task<List<SentEmailApplicationLookupTableDto>> GetAllApplicationForTableDropdown()
        {
            return await _lookup_applicationRepository.GetAll()
                .Select(application => new SentEmailApplicationLookupTableDto
                {
                    Id = application.Id,
                    DisplayName = application == null || application.Name == null ? "" : application.Name.ToString()
                }).ToListAsync();
        }

    }
}