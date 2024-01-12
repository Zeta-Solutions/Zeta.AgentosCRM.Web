using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic; 
using Zeta.AgentosCRM.Tenants.Email.Dtos;

namespace Zeta.AgentosCRM.Tenants.Email
{
    public interface ISentEmailsAppService : IApplicationService
    {
        Task<PagedResultDto<GetSentEmailForViewDto>> GetAll(GetAllSentEmailsInput input);

        Task<GetSentEmailForViewDto> GetSentEmailForView(long id);

        Task<GetSentEmailForEditOutput> GetSentEmailForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditSentEmailDto input);

        Task Delete(EntityDto<long> input);

        Task<List<SentEmailEmailTemplateLookupTableDto>> GetAllEmailTemplateForTableDropdown();

        Task<List<SentEmailEmailConfigurationLookupTableDto>> GetAllEmailConfigurationForTableDropdown();

        Task<List<SentEmailClientLookupTableDto>> GetAllClientForTableDropdown();

        Task<List<SentEmailApplicationLookupTableDto>> GetAllApplicationForTableDropdown();

    }
}