using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.CRMSetup.Documents.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.CRMSetup.Documents
{
    public interface IDocumentTypesAppService : IApplicationService
    {
        Task<PagedResultDto<GetDocumentTypeForViewDto>> GetAll(GetAllDocumentTypesInput input);

        Task<GetDocumentTypeForViewDto> GetDocumentTypeForView(int id);

        Task<GetDocumentTypeForEditOutput> GetDocumentTypeForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditDocumentTypeDto input);

        Task Delete(EntityDto input);

    }
}