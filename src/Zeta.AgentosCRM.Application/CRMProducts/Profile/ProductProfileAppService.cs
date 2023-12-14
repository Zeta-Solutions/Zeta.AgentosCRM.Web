using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Abp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeta.AgentosCRM.Authorization.Users.Profile.Dto;
using Zeta.AgentosCRM.Authorization.Users.Profile;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.Configuration; 
using Zeta.AgentosCRM.Storage;
using Zeta.AgentosCRM.CRMProducts.Profile.Dtos;

namespace Zeta.AgentosCRM.CRMProducts.Profile
{
    public class ProductProfileAppService : AgentosCRMAppServiceBase, IProductProfileAppService
    {
        private readonly IRepository<Product, long> _productRepository;
        private const int MaxProfilePictureBytes = 5242880; //5MB
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ProfileImageServiceFactory _profileImageServiceFactory;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly LocalProfileImageService _localProfileImageService;

        public ProductProfileAppService(IBinaryObjectManager binaryObjectManager,
                                       ProfileImageServiceFactory profileImageServiceFactory,
                                       ITempFileCacheManager tempFileCacheManager,
                                       IRepository<Product, long> productRepository,
                                       LocalProfileImageService localProfileImageService)
        {
            _binaryObjectManager = binaryObjectManager;
            _profileImageServiceFactory = profileImageServiceFactory;
            _tempFileCacheManager = tempFileCacheManager;
            _productRepository = productRepository;
            _localProfileImageService = localProfileImageService;
        }

        [AbpAllowAnonymous]
        public async Task<GetProfilePictureOutput> GetProfilePictureByProduct(long productId)
        {
            var profileImage = await _localProfileImageService.GetProfilePictureContentForProduct(productId);
            return new GetProfilePictureOutput(profileImage);
        }

        [AbpAllowAnonymous]
        public async Task<GetProfilePictureOutput> GetProfilePictureByPictireId(string fileTokkenId)
        {
            Guid guid = Guid.Parse(fileTokkenId);
            var profileImage = await _localProfileImageService.GetProfilePictureContent(guid);
            return new GetProfilePictureOutput(profileImage);
        }


        public async Task UpdateProfilePicture(UpdateProductProfilePictureInput input)
        {
            if (input.ProductId.HasValue)
            {
                //await CheckUpdateProductsProfilePicturePermission(); 
                await UpdateProfilePictureForProduct(input.ProductId.Value, input);
            }
            //else
            //{
            //    await InsertProfilePictureForProduct(input);
            //}
        }

        private async Task CheckUpdateProductsProfilePicturePermission()
        {
            var permissionToChangeAnotherUsersProfilePicture = await PermissionChecker.IsGrantedAsync(
                AppPermissions.Pages_Products_ChangeProfilePicture
            );

            if (!permissionToChangeAnotherUsersProfilePicture)
            {
                var localizedPermissionName = L("UpdateProductsProfilePicture");
                throw new AbpAuthorizationException(
                    string.Format(
                        L("AllOfThesePermissionsMustBeGranted"),
                        localizedPermissionName
                    )
                );
            }
        }


        private async Task UpdateProfilePictureForProduct(long clientId, UpdateProductProfilePictureInput input)
        {
            var clientProfile = await _productRepository.FirstOrDefaultAsync(p => p.Id == clientId);
            if (clientProfile == null)
            {
                // Create a new client profile if it doesn't exist
                // clientProfile = new ClientProfile { ClientId = productId };
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

        public async Task<Guid> InsertProfilePictureForProduct(UpdateProductProfilePictureInput input)
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
                $"Profile picture of Product {DateTime.UtcNow}");
            await _binaryObjectManager.SaveAsync(storedFile);

            return storedFile.Id;
        }

    }
}
