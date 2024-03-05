using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMInvoice.Dtos.InvIncomeSharing;

namespace Zeta.AgentosCRM.CRMInvoice
{
    public interface IInvIncomeSharingAppService : IApplicationService
    {
        Task<PagedResultDto<GetInvIncomeSharingForViewDto>> GetAll(GetAllInvIncomeSharingInput input);

        Task<GetInvIncomeSharingForViewDto> GetInvIncomeSharingForView(long id);

        Task<GetInvIncomeSharingForEditOutput> GetInvIncomeSharingForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditInvIncomeSharingDto input);

        Task Delete(EntityDto<long> input);
    }
}
