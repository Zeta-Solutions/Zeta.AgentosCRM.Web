using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace Zeta.AgentosCRM.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
