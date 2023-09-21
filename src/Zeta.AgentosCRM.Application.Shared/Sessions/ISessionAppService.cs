using System.Threading.Tasks;
using Abp.Application.Services;
using Zeta.AgentosCRM.Sessions.Dto;

namespace Zeta.AgentosCRM.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
