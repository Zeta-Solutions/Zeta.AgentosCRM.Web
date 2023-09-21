using System.Threading.Tasks;
using Abp.Dependency;

namespace Zeta.AgentosCRM.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}