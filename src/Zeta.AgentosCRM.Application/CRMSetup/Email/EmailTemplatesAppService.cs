using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMSetup.Email.Dtos;
using Zeta.AgentosCRM.Dto;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMSetup.Email
{
    [AbpAuthorize(AppPermissions.Pages_EmailTemplates)]
    public class EmailTemplatesAppService : AgentosCRMAppServiceBase, IEmailTemplatesAppService
    {
        private readonly IRepository<EmailTemplate> _emailTemplateRepository;

        public EmailTemplatesAppService(IRepository<EmailTemplate> emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;

        }

        public async Task<PagedResultDto<GetEmailTemplateForViewDto>> GetAll(GetAllEmailTemplatesInput input)
        {

            var filteredEmailTemplates = _emailTemplateRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Title.Contains(input.Filter) || e.EmailSubject.Contains(input.Filter) || e.EmailBody.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.Contains(input.TitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailSubjectFilter), e => e.EmailSubject.Contains(input.EmailSubjectFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailBodyFilter), e => e.EmailBody.Contains(input.EmailBodyFilter));

            var pagedAndFilteredEmailTemplates = filteredEmailTemplates
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var emailTemplates = from o in pagedAndFilteredEmailTemplates
                                 select new
                                 {

                                     o.Title,
                                     o.EmailSubject,
                                     o.EmailBody,
                                     Id = o.Id
                                 };

            var totalCount = await filteredEmailTemplates.CountAsync();

            var dbList = await emailTemplates.ToListAsync();
            var results = new List<GetEmailTemplateForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetEmailTemplateForViewDto()
                {
                    EmailTemplate = new EmailTemplateDto
                    {

                        Title = o.Title,
                        EmailSubject = o.EmailSubject,
                        EmailBody = o.EmailBody,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetEmailTemplateForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetEmailTemplateForViewDto> GetEmailTemplateForView(int id)
        {
            var emailTemplate = await _emailTemplateRepository.GetAsync(id);

            var output = new GetEmailTemplateForViewDto { EmailTemplate = ObjectMapper.Map<EmailTemplateDto>(emailTemplate) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_EmailTemplates_Edit)]
        public async Task<GetEmailTemplateForEditOutput> GetEmailTemplateForEdit(EntityDto input)
        {
            var emailTemplate = await _emailTemplateRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEmailTemplateForEditOutput { EmailTemplate = ObjectMapper.Map<CreateOrEditEmailTemplateDto>(emailTemplate) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditEmailTemplateDto input)
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

        [AbpAuthorize(AppPermissions.Pages_EmailTemplates_Create)]
        protected virtual async Task Create(CreateOrEditEmailTemplateDto input)
        {
            var emailTemplate = ObjectMapper.Map<EmailTemplate>(input);

            if (AbpSession.TenantId != null)
            {
                emailTemplate.TenantId = (int)AbpSession.TenantId;
            }

            await _emailTemplateRepository.InsertAsync(emailTemplate);

        }

        [AbpAuthorize(AppPermissions.Pages_EmailTemplates_Edit)]
        protected virtual async Task Update(CreateOrEditEmailTemplateDto input)
        {
            var emailTemplate = await _emailTemplateRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, emailTemplate);

        }

        [AbpAuthorize(AppPermissions.Pages_EmailTemplates_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _emailTemplateRepository.DeleteAsync(input.Id);
        }

    }
}