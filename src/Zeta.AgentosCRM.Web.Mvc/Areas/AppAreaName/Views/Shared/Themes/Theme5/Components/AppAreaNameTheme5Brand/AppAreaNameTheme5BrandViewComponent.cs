using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Layout;
using Zeta.AgentosCRM.Web.Session;
using Zeta.AgentosCRM.Web.Views;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Views.Shared.Themes.Theme5.Components.AppAreaNameTheme5Brand
{
    public class AppAreaNameTheme5BrandViewComponent : AgentosCRMViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppAreaNameTheme5BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(headerModel);
        }
    }
}
