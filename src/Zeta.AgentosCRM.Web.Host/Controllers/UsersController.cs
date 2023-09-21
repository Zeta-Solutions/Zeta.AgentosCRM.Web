using Abp.AspNetCore.Mvc.Authorization;
using Zeta.AgentosCRM.Authorization;
using Zeta.AgentosCRM.Storage;
using Abp.BackgroundJobs;

namespace Zeta.AgentosCRM.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}