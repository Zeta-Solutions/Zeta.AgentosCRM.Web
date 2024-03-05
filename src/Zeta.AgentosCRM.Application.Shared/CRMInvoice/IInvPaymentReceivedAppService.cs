using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMInvoice.Dtos.InvPaymentReceived;

namespace Zeta.AgentosCRM.CRMInvoice
{
    public interface IInvPaymentReceivedAppService : IApplicationService
    {
        Task<PagedResultDto<GetInvPaymentReceivedForViewDto>> GetAll(GetAllInvPaymentReceivedInput input);

        Task<GetInvPaymentReceivedForViewDto> GetInvPaymentReceivedForView(long id);

        Task<GetInvPaymentReceivedForEditOutput> GetInvPaymentReceivedForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditInvPaymentReceivedDto input);

        Task Delete(EntityDto<long> input);

    }
}
