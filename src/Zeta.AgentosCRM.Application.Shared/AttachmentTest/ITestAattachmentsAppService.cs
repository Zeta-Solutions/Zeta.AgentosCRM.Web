using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.AttachmentTest.Dtos;
using Zeta.AgentosCRM.Dto;

namespace Zeta.AgentosCRM.AttachmentTest
{
    public interface ITestAattachmentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetTestAattachmentForViewDto>> GetAll(GetAllTestAattachmentsInput input);

        Task<GetTestAattachmentForViewDto> GetTestAattachmentForView(int id);

        Task<GetTestAattachmentForEditOutput> GetTestAattachmentForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditTestAattachmentDto input);

        Task Delete(EntityDto input);

        Task RemoveAttachmentFile(EntityDto input);

    }
}