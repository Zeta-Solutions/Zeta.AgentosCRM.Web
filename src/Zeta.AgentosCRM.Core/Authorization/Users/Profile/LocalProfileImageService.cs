using System;
using System.Threading.Tasks;
using Abp;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Zeta.AgentosCRM.CRMClient;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.Authorization.Users.Profile
{
    public class LocalProfileImageService : IProfileImageService, ITransientDependency
    {
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly UserManager _userManager;
        private readonly IRepository<Client, long> _clientRepository;

        public LocalProfileImageService(
            IBinaryObjectManager binaryObjectManager,
            UserManager userManager,
            IRepository<Client, long> clientRepository)
        {
            _binaryObjectManager = binaryObjectManager;
            _userManager = userManager;
            _clientRepository = clientRepository;
        }

        public async Task<string> GetProfilePictureContentForUser(UserIdentifier userIdentifier)
        {
            var user = await _userManager.GetUserOrNullAsync(userIdentifier);
            if (user?.ProfilePictureId == null)
            {
                return "";
            }
             
            var file = await _binaryObjectManager.GetOrNullAsync(user.ProfilePictureId.Value);
            return file == null ? "" : Convert.ToBase64String(file.Bytes);
        }
        public async Task<string> GetProfilePictureContentForClient(long clientId)
        {
            var client = await _clientRepository.GetAsync(clientId);
            if (client?.ProfilePictureId == null)
            {
                return "";
            }
             
            var file = await _binaryObjectManager.GetOrNullAsync(client.ProfilePictureId.Value);
            return file == null ? "" : Convert.ToBase64String(file.Bytes);
        }
        public async Task<string> GetProfilePictureContent(Guid pictureId)
        { 
            var file = await _binaryObjectManager.GetOrNullAsync(pictureId);
            return file == null ? "" : Convert.ToBase64String(file.Bytes);
        }
    }
}