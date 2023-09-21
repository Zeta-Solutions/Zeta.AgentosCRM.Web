using Abp.AspNetCore.Mvc.Authorization;
using Zeta.AgentosCRM.Authorization.Users.Profile;
using Zeta.AgentosCRM.Graphics;
using Zeta.AgentosCRM.Storage;

namespace Zeta.AgentosCRM.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : ProfileControllerBase
    {
        public ProfileController(
            ITempFileCacheManager tempFileCacheManager,
            IProfileAppService profileAppService,
            IImageValidator imageValidator) :
            base(tempFileCacheManager, profileAppService, imageValidator)
        {
        }
    }
}