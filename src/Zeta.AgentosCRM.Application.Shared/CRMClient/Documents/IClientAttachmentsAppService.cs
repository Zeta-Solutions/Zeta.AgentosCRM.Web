using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMClient.Documents.Dtos;
using Zeta.AgentosCRM.Dto;
using System.Collections.Generic;

namespace Zeta.AgentosCRM.CRMClient.Documents
{
    public interface IClientAttachmentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetClientAttachmentForViewDto>> GetAll(GetAllClientAttachmentsInput input);

        Task<GetClientAttachmentForViewDto> GetClientAttachmentForView(long id);

        Task<GetClientAttachmentForEditOutput> GetClientAttachmentForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditClientAttachmentDto input);

        Task Delete(EntityDto<long> input);

        Task<List<ClientAttachmentClientLookupTableDto>> GetAllClientForTableDropdown();

        Task RemoveAttachmentIdFile(EntityDto<long> input);

    }
}