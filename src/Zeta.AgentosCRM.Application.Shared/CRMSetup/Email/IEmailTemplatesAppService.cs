using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Email.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Email
{
    public interface IEmailTemplatesAppService : IApplicationService
    {
        Task<PagedResultDto<GetEmailTemplateForViewDto>> GetAll(GetAllEmailTemplatesInput input);

        Task<GetEmailTemplateForViewDto> GetEmailTemplateForView(int id);

        Task<GetEmailTemplateForEditOutput> GetEmailTemplateForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditEmailTemplateDto input);

        Task Delete(EntityDto input);

    }
}