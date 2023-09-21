using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Zeta.AgentosCRM.MultiTenancy.Accounting.Dto;

namespace Zeta.AgentosCRM.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
