using Abp;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.UI;
using System; 
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.Authorization.Users.Profile;
using Zeta.AgentosCRM.Authorization.Users.Profile.Dto;
using Zeta.AgentosCRM.Configuration;
using Zeta.AgentosCRM.CRMClient.Profile.Dto;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.CRMClient.Profile
{
    [AbpAuthorize]
    public class ClientProfileAppService : AgentosCRMAppServiceBase, IClientProfileAppService
    {
        private readonly IRepository<Client, long> _clientRepository;
        private const int MaxProfilePictureBytes = 5242880; //5MB
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ProfileImageServiceFactory _profileImageServiceFactory; 
        private readonly ITempFileCacheManager _tempFileCacheManager;

        public ClientProfileAppService(IBinaryObjectManager binaryObjectManager,
                                       ProfileImageServiceFactory profileImageServiceFactory,
                                       ITempFileCacheManager tempFileCacheManager,
                                       IRepository<Client, long> clientRepository)
        {
            _binaryObjectManager = binaryObjectManager;
            _profileImageServiceFactory = profileImageServiceFactory;
            _tempFileCacheManager = tempFileCacheManager;
            _clientRepository = clientRepository;
        }

        [DisableAuditing]
        public async Task<GetProfilePictureOutput> GetProfilePicture()
        {
            using (var profileImageService = await _profileImageServiceFactory.Get(AbpSession.ToUserIdentifier()))
            {
                var profilePictureContent = await profileImageService.Object.GetProfilePictureContentForUser(
                    AbpSession.ToUserIdentifier()
                );

                return new GetProfilePictureOutput(profilePictureContent);
            }
        }

        [AbpAllowAnonymous]
        public async Task<GetProfilePictureOutput> GetProfilePictureByClient(long clientId)
        {
            var userIdentifier = new UserIdentifier(AbpSession.TenantId, clientId);
            using (var profileImageService = await _profileImageServiceFactory.Get(userIdentifier))
            {
                var profileImage = await profileImageService.Object.GetProfilePictureContentForUser(userIdentifier);
                return new GetProfilePictureOutput(profileImage);
            }
        }

        [AbpAllowAnonymous]
        public async Task<GetProfilePictureOutput> GetProfilePictureByClientName(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                return new GetProfilePictureOutput(string.Empty);
            }

            var userIdentifier = new UserIdentifier(AbpSession.TenantId, user.Id);
            using (var profileImageService = await _profileImageServiceFactory.Get(userIdentifier))
            {
                var profileImage = await profileImageService.Object.GetProfilePictureContentForUser(userIdentifier);
                return new GetProfilePictureOutput(profileImage);
            }
        }

        public async Task UpdateProfilePicture(UpdateClientProfilePictureInput input)
        {
            var userId = AbpSession.GetUserId();
            if (input.ClientId.HasValue && input.ClientId.Value != userId)
            {
                await CheckUpdateClientsProfilePicturePermission();
                userId = input.ClientId.Value;
            }

            await UpdateProfilePictureForClient(userId, input);
        }

        private async Task CheckUpdateClientsProfilePicturePermission()
        {
            var permissionToChangeAnotherUsersProfilePicture = await PermissionChecker.IsGrantedAsync(
                AppPermissions.Pages_Clients_ChangeProfilePicture
            );

            if (!permissionToChangeAnotherUsersProfilePicture)
            {
                var localizedPermissionName = L("UpdateClientsProfilePicture");
                throw new AbpAuthorizationException(
                    string.Format(
                        L("AllOfThesePermissionsMustBeGranted"),
                        localizedPermissionName
                    )
                );
            }
        }


        private async Task UpdateProfilePictureForClient(long clientId, UpdateClientProfilePictureInput input)
        {
            var clientProfile = await _clientRepository.FirstOrDefaultAsync(p => p.Id == clientId);
            if (clientProfile == null)
            {
                // Create a new client profile if it doesn't exist
               // clientProfile = new ClientProfile { ClientId = clientId };
            }
            var userId= (long)AbpSession.UserId;
            var userIdentifier = new UserIdentifier(AbpSession.TenantId, userId);
            var allowToUseGravatar = await SettingManager.GetSettingValueForUserAsync<bool>(
                AppSettings.UserManagement.AllowUsingGravatarProfilePicture,
                user: userIdentifier
            );

            if (!allowToUseGravatar)
            {
                input.UseGravatarProfilePicture = false;
            }

            await SettingManager.ChangeSettingForUserAsync(
                userIdentifier,
                AppSettings.UserManagement.UseGravatarProfilePicture,
                input.UseGravatarProfilePicture.ToString().ToLowerInvariant()
            );

            if (input.UseGravatarProfilePicture)
            {
                return;
            }

            byte[] byteArray;

            var imageBytes = _tempFileCacheManager.GetFile(input.FileToken);

            if (imageBytes == null)
            {
                throw new UserFriendlyException("There is no such image file with the token: " + input.FileToken);
            }

            byteArray = imageBytes;

            if (byteArray.Length > MaxProfilePictureBytes)
            {
                throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit",
                    AppConsts.ResizedMaxProfilePictureBytesUserFriendlyValue));
            }

           // var user = await UserManager.GetUserByIdAsync(userIdentifier.UserId);

            if (clientProfile.ProfilePictureId.HasValue)
            {
                await _binaryObjectManager.DeleteAsync(clientProfile.ProfilePictureId.Value);
            }

            var storedFile = new BinaryObject(clientProfile.TenantId, byteArray,
                $"Profile picture of Client {clientProfile.Id}. {DateTime.UtcNow}");
            await _binaryObjectManager.SaveAsync(storedFile);

            clientProfile.ProfilePictureId = storedFile.Id;
        }

    }
}
