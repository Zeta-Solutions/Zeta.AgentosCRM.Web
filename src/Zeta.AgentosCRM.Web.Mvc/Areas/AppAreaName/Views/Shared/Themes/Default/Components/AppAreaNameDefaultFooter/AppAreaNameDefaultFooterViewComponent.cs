using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Layout;
using Zeta.AgentosCRM.Web.Session;
using Zeta.AgentosCRM.Web.Views;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Views.Shared.Themes.Default.Components.AppAreaNameDefaultFooter
{
    public class AppAreaNameDefaultFooterViewComponent : AgentosCRMViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppAreaNameDefaultFooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
