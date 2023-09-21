using System.Threading.Tasks;
using Zeta.AgentosCRM.Sessions.Dto;

namespace Zeta.AgentosCRM.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
