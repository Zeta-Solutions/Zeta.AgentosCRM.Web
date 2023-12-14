using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp;
using System; 
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization.Users.Profile.Dto;
using Zeta.AgentosCRM.Authorization.Users.Profile;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.Configuration; 
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMPartner.Profile.Dtos;

namespace Zeta.AgentosCRM.CRMPartner.Profile
{
    public class PartnerProfileAppService : AgentosCRMAppServiceBase, IPartnerProfileAppService
    {
        private readonly IRepository<Partner, long> _partnerRepository;
        private const int MaxProfilePictureBytes = 5242880; //5MB
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ProfileImageServiceFactory _profileImageServiceFactory;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly LocalProfileImageService _localProfileImageService;

        public PartnerProfileAppService(IBinaryObjectManager binaryObjectManager,
                                       ProfileImageServiceFactory profileImageServiceFactory,
                                       ITempFileCacheManager tempFileCacheManager,
                                       IRepository<Partner, long> partnerRepository,
                                       LocalProfileImageService localProfileImageService)
        {
            _binaryObjectManager = binaryObjectManager;
            _profileImageServiceFactory = profileImageServiceFactory;
            _tempFileCacheManager = tempFileCacheManager;
            _partnerRepository = partnerRepository;
            _localProfileImageService = localProfileImageService;
        }

        [AbpAllowAnonymous]
        public async Task<GetProfilePictureOutput> GetProfilePictureByPartner(long partnerId)
        {
            var profileImage = await _localProfileImageService.GetProfilePictureContentForPartner(partnerId);
            return new GetProfilePictureOutput(profileImage);
        }

        [AbpAllowAnonymous]
        public async Task<GetProfilePictureOutput> GetProfilePictureByPictireId(string fileTokkenId)
        {
            Guid guid = Guid.Parse(fileTokkenId);
            var profileImage = await _localProfileImageService.GetProfilePictureContent(guid);
            return new GetProfilePictureOutput(profileImage);
        }

        public async Task UpdateProfilePicture(UpdatePartnerProfilePictureInput input)
        {
            if (input.PartnerId.HasValue)
            {
                //await CheckUpdatePartnersProfilePicturePermission(); 
                await UpdateProfilePictureForPartner(input.PartnerId.Value, input);
            }
            //else
            //{
            //    await InsertProfilePictureForPartner(input);
            //}
        }

        private async Task CheckUpdatePartnersProfilePicturePermission()
        {
            var permissionToChangeAnotherUsersProfilePicture = await PermissionChecker.IsGrantedAsync(
                AppPermissions.Pages_Partners_ChangeProfilePicture
            );

            if (!permissionToChangeAnotherUsersProfilePicture)
            {
                var localizedPermissionName = L("UpdatePartnersProfilePicture");
                throw new AbpAuthorizationException(
                    string.Format(
                        L("AllOfThesePermissionsMustBeGranted"),
                        localizedPermissionName
                    )
                );
            }
        }


        private async Task UpdateProfilePictureForPartner(long partnerId, UpdatePartnerProfilePictureInput input)
        {
            var partnerProfile = await _partnerRepository.FirstOrDefaultAsync(p => p.Id == partnerId);
            if (partnerProfile == null)
            {
                // Create a new Partner profile if it doesn't exist
                // partnerProfile = new PartnerProfile { PartnerId = partnerId };
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

            if (partnerProfile.ProfilePictureId.HasValue)
            {
                await _binaryObjectManager.DeleteAsync(partnerProfile.ProfilePictureId.Value);
            }

            var storedFile = new BinaryObject(partnerProfile.TenantId, byteArray,
                $"Profile picture of Partner {partnerProfile.Id}. {DateTime.UtcNow}");
            await _binaryObjectManager.SaveAsync(storedFile);

            partnerProfile.ProfilePictureId = storedFile.Id;
        }

        public async Task<Guid> InsertProfilePictureForPartner(UpdatePartnerProfilePictureInput input)
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
                $"Profile picture of Partner {DateTime.UtcNow}");
            await _binaryObjectManager.SaveAsync(storedFile);

            return storedFile.Id;
        }

    }
}
