using Abp;
using Abp.Domain.Services;
using System.Threading.Tasks;

namespace Zeta.AgentosCRM.Authorization.Users.Profile
{
    public interface IProfileImageService : IDomainService
    {
        Task<string> GetProfilePictureContentForUser(UserIdentifier userIdentifier);
    }
}