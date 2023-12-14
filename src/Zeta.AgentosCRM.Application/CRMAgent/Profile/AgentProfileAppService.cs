using Abp; 
using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories; 
using Abp.UI;
using System;
using System.Threading.Tasks; 
using Zeta.AgentosCRM.Authorization.Users.Profile.Dto;
using Zeta.AgentosCRM.Authorization.Users.Profile;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.Configuration; 
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMAgent.Profile.Dtos;

namespace Zeta.AgentosCRM.CRMAgent.Profile
{
    [AbpAuthorize]
    public class AgentProfileAppService : AgentosCRMAppServiceBase, IAgentProfileAppService
    {
        private readonly IRepository<Agent, long> _agentRepository;
        private const int MaxProfilePictureBytes = 5242880; //5MB
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ProfileImageServiceFactory _profileImageServiceFactory;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly LocalProfileImageService _localProfileImageService;

        public AgentProfileAppService(IBinaryObjectManager binaryObjectManager,
                                       ProfileImageServiceFactory profileImageServiceFactory,
                                       ITempFileCacheManager tempFileCacheManager,
                                       IRepository<Agent, long> agentRepository,
                                       LocalProfileImageService localProfileImageService)
        {
            _binaryObjectManager = binaryObjectManager;
            _profileImageServiceFactory = profileImageServiceFactory;
            _tempFileCacheManager = tempFileCacheManager;
            _agentRepository = agentRepository;
            _localProfileImageService = localProfileImageService;
        }

        [AbpAllowAnonymous]
        public async Task<GetProfilePictureOutput> GetProfilePictureByAgent(long agentId)
        {
            var profileImage = await _localProfileImageService.GetProfilePictureContentForAgent(agentId);
            return new GetProfilePictureOutput(profileImage);
        }

        [AbpAllowAnonymous]
        public async Task<GetProfilePictureOutput> GetProfilePictureByPictireId(string fileTokkenId)
        {
            Guid guid = Guid.Parse(fileTokkenId);
            var profileImage = await _localProfileImageService.GetProfilePictureContent(guid);
            return new GetProfilePictureOutput(profileImage);
        }
         

        public async Task UpdateProfilePicture(UpdateAgentProfilePictureInput input)
        {
            if (input.AgentId.HasValue)
            {
                //await CheckUpdateAgentsProfilePicturePermission(); 
                await UpdateProfilePictureForAgent(input.AgentId.Value, input);
            }
            //else
            //{
            //    await InsertProfilePictureForAgent(input);
            //}
        }

        private async Task CheckUpdateAgentsProfilePicturePermission()
        {
            var permissionToChangeAnotherUsersProfilePicture = await PermissionChecker.IsGrantedAsync(
                AppPermissions.Pages_Agents_ChangeProfilePicture
            );

            if (!permissionToChangeAnotherUsersProfilePicture)
            {
                var localizedPermissionName = L("UpdateAgentsProfilePicture");
                throw new AbpAuthorizationException(
                    string.Format(
                        L("AllOfThesePermissionsMustBeGranted"),
                        localizedPermissionName
                    )
                );
            }
        }


        private async Task UpdateProfilePictureForAgent(long clientId, UpdateAgentProfilePictureInput input)
        {
            var clientProfile = await _agentRepository.FirstOrDefaultAsync(p => p.Id == clientId);
            if (clientProfile == null)
            {
                // Create a new client profile if it doesn't exist
                // clientProfile = new ClientProfile { ClientId = agentId };
            }
            var userId = (long)AbpSession.UserId;
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

        public async Task<Guid> InsertProfilePictureForAgent(UpdateAgentProfilePictureInput input)
        {
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

            var storedFile = new BinaryObject(AbpSession.TenantId, byteArray,
                $"Profile picture of Agent {DateTime.UtcNow}");
            await _binaryObjectManager.SaveAsync(storedFile);

            return storedFile.Id;
        }

    }
}
