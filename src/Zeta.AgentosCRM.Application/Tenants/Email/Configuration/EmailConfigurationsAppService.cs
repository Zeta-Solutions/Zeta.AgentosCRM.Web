using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.Tenants.Email.Configuration.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.Tenants.Email.Configuration
{
    [AbpAuthorize(AppPermissions.Pages_EmailConfigurations)]
    public class EmailConfigurationsAppService : AgentosCRMAppServiceBase, IEmailConfigurationsAppService
    {
        private readonly IRepository<EmailConfiguration, long> _emailConfigurationRepository;

        public EmailConfigurationsAppService(IRepository<EmailConfiguration, long> emailConfigurationRepository)
        {
            _emailConfigurationRepository = emailConfigurationRepository;

        }

        public async Task<PagedResultDto<GetEmailConfigurationForViewDto>> GetAll(GetAllEmailConfigurationsInput input)
        {

            var filteredEmailConfigurations = _emailConfigurationRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.SenderEmail.Contains(input.Filter) || e.SmtpServer.Contains(input.Filter) || e.SenderPassword.Contains(input.Filter) || e.UserName.Contains(input.Filter) || e.Protocol.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name.Contains(input.NameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SenderEmailFilter), e => e.SenderEmail.Contains(input.SenderEmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SmtpServerFilter), e => e.SmtpServer.Contains(input.SmtpServerFilter))
                        .WhereIf(input.MinSmtpPortFilter != null, e => e.SmtpPort >= input.MinSmtpPortFilter)
                        .WhereIf(input.MaxSmtpPortFilter != null, e => e.SmtpPort <= input.MaxSmtpPortFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SenderPasswordFilter), e => e.SenderPassword.Contains(input.SenderPasswordFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserName.Contains(input.UserNameFilter))
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(input.IsEnableSslFilter.HasValue && input.IsEnableSslFilter > -1, e => (input.IsEnableSslFilter == 1 && e.IsEnableSsl) || (input.IsEnableSslFilter == 0 && !e.IsEnableSsl))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ProtocolFilter), e => e.Protocol.Contains(input.ProtocolFilter));

            var pagedAndFilteredEmailConfigurations = filteredEmailConfigurations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var emailConfigurations = from o in pagedAndFilteredEmailConfigurations
                                      select new
                                      {

                                          o.Name,
                                          o.SenderEmail,
                                          o.SmtpServer,
                                          o.SmtpPort,
                                          o.SenderPassword,
                                          o.UserName,
                                          o.IsActive,
                                          o.IsEnableSsl,
                                          o.Protocol,
                                          Id = o.Id
                                      };

            var totalCount = await filteredEmailConfigurations.CountAsync();

            var dbList = await emailConfigurations.ToListAsync();
            var results = new List<GetEmailConfigurationForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetEmailConfigurationForViewDto()
                {
                    EmailConfiguration = new EmailConfigurationDto
                    {

                        Name = o.Name,
                        SenderEmail = o.SenderEmail,
                        SmtpServer = o.SmtpServer,
                        SmtpPort = o.SmtpPort,
                        SenderPassword = o.SenderPassword,
                        UserName = o.UserName,
                        IsActive = o.IsActive,
                        IsEnableSsl = o.IsEnableSsl,
                        Protocol = o.Protocol,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetEmailConfigurationForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetEmailConfigurationForViewDto> GetEmailConfigurationForView(long id)
        {
            var emailConfiguration = await _emailConfigurationRepository.GetAsync(id);

            var output = new GetEmailConfigurationForViewDto { EmailConfiguration = ObjectMapper.Map<EmailConfigurationDto>(emailConfiguration) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_EmailConfigurations_Edit)]
        public async Task<GetEmailConfigurationForEditOutput> GetEmailConfigurationForEdit(EntityDto<long> input)
        {
            var emailConfiguration = await _emailConfigurationRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEmailConfigurationForEditOutput { EmailConfiguration = ObjectMapper.Map<CreateOrEditEmailConfigurationDto>(emailConfiguration) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditEmailConfigurationDto input)
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

        [AbpAuthorize(AppPermissions.Pages_EmailConfigurations_Create)]
        protected virtual async Task Create(CreateOrEditEmailConfigurationDto input)
        {
            var emailConfiguration = ObjectMapper.Map<EmailConfiguration>(input);

            if (AbpSession.TenantId != null)
            {
                emailConfiguration.TenantId = (int)AbpSession.TenantId;
            }

            await _emailConfigurationRepository.InsertAsync(emailConfiguration);

        }

        [AbpAuthorize(AppPermissions.Pages_EmailConfigurations_Edit)]
        protected virtual async Task Update(CreateOrEditEmailConfigurationDto input)
        {
            var emailConfiguration = await _emailConfigurationRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, emailConfiguration);

        }

        [AbpAuthorize(AppPermissions.Pages_EmailConfigurations_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _emailConfigurationRepository.DeleteAsync(input.Id);
        }

    }
}