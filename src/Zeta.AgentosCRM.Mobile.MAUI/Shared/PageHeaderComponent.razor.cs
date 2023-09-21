using Zeta.AgentosCRM.Core.Dependency;
using Zeta.AgentosCRM.Mobile.MAUI.Services.UI;

namespace Zeta.AgentosCRM.Mobile.MAUI.Shared
{
    public partial class PageHeaderComponent
    {
        protected PageHeaderService PageHeaderService { get; set; }

        public PageHeaderComponent()
        {
            PageHeaderService = DependencyResolver.Resolve<PageHeaderService>();
            PageHeaderService.TitleChanged += (s, e) => StateHasChanged();
            PageHeaderService.HeaderButtonChanged += (s, e) => StateHasChanged();
        }

        public async Task HandleButtonOnClick(HeaderButtonInfo HeaderButtonInfo)
        {
            if (HeaderButtonInfo == null)
            {
                return;
            }

            await HeaderButtonInfo.OnClick();
        }
    }
}
