using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.CRMInvoice.Dtos.InvIncomeSharing;
using Zeta.AgentosCRM.CRMInvoice.Dtos.InvPaymentReceived;
using Microsoft.EntityFrameworkCore;

namespace Zeta.AgentosCRM.CRMInvoice
{
    public class InvPaymentReceivedAppService : AgentosCRMAppServiceBase, IInvPaymentReceivedAppService
    {
        private readonly IRepository<InvPaymentReceived, long> _invPaymentReceivedRepository;

        public InvPaymentReceivedAppService(IRepository<InvPaymentReceived, long> invPaymentReceivedRepository)
        {
            _invPaymentReceivedRepository = invPaymentReceivedRepository;
        }
        public async Task<PagedResultDto<GetInvPaymentReceivedForViewDto>> GetAll(GetAllInvPaymentReceivedInput input)
        {
            var filteredinvPaymentReceived = _invPaymentReceivedRepository.GetAll();

            var pagedAndfilteredinvPaymentReceived = filteredinvPaymentReceived
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var invPaymentReceived = from o in pagedAndfilteredinvPaymentReceived
                                   
                                   select new
                                   {

                                       o.InvoiceHeadId,
                                       o.PaymentsReceived,
                                       Id = o.Id,
                                       o.PaymentsReceivedDate,
                                       o.MarkInvoicePaid,
                                       o.PaymentMethodId,
                                       o.AddNotes,
                                       o.AttachmentId,
                                     
                                   };

            var totalCount = await filteredinvPaymentReceived.CountAsync();

            var dbList = await invPaymentReceived.ToListAsync();
            var results = new List<GetInvPaymentReceivedForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetInvPaymentReceivedForViewDto()
                {
                    InvPaymentReceived = new InvPaymentReceivedDto
                    {

                        InvoiceHeadId = o.InvoiceHeadId,
                        PaymentsReceived = o.PaymentsReceived,
                        Id = o.Id,
                        PaymentsReceivedDate = o.PaymentsReceivedDate,
                        MarkInvoicePaid = o.MarkInvoicePaid,
                        PaymentMethodId = o.PaymentMethodId,
                        AddNotes = o.AddNotes,
                        AttachmentId = o.AttachmentId,

                    },
                };

                results.Add(res);
            }

            return new PagedResultDto<GetInvPaymentReceivedForViewDto>(
                totalCount,
                results
            );

        }
        public async Task<GetInvPaymentReceivedForViewDto> GetInvPaymentReceivedForView(long id)
        {
            var InvPaymentReceived = await _invPaymentReceivedRepository.GetAsync(id);

            var output = new GetInvPaymentReceivedForViewDto { InvPaymentReceived = ObjectMapper.Map<InvPaymentReceivedDto>(InvPaymentReceived) };
           
            return output;
        }
        public async Task<GetInvPaymentReceivedForEditOutput> GetInvPaymentReceivedForEdit(EntityDto<long> input)
        {
            var InvPaymentReceived = await _invPaymentReceivedRepository.FirstOrDefaultAsync(input.Id);
            var output = new GetInvPaymentReceivedForEditOutput
            {
                InvPaymentReceived = ObjectMapper.Map<CreateOrEditInvPaymentReceivedDto>(InvPaymentReceived)
            };
            
            return output;
        }
        public async Task CreateOrEdit(CreateOrEditInvPaymentReceivedDto input)
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
        protected virtual async Task Create(CreateOrEditInvPaymentReceivedDto input)
        {
            var InvPaymentReceived = ObjectMapper.Map<InvPaymentReceived>(input);

            if (AbpSession.TenantId != null)
            {
                InvPaymentReceived.TenantId = (int)AbpSession.TenantId;
            }
            await _invPaymentReceivedRepository.InsertAsync(InvPaymentReceived);

        }
        protected virtual async Task Update(CreateOrEditInvPaymentReceivedDto input)
        {
            var invPaymentReceived = await _invPaymentReceivedRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, invPaymentReceived);
        }
        public async Task Delete(EntityDto<long> input)
        {
            await _invPaymentReceivedRepository.DeleteAsync(input.Id);
        }
    }
}
