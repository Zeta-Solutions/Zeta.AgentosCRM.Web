using Zeta.AgentosCRM.Core.Dependency;
using Zeta.AgentosCRM.Mobile.MAUI.Services.UI;

namespace Zeta.AgentosCRM.Mobile.MAUI.Shared
{
    public abstract class ModalBase : AgentosCRMComponentBase
    {
        protected ModalManagerService ModalManager { get; set; }

        public abstract string ModalId { get; }

        public ModalBase()
        {
            ModalManager = DependencyResolver.Resolve<ModalManagerService>();
        }

        public virtual async Task Show()
        {
            await ModalManager.Show(JS, ModalId);
            StateHasChanged();
        }

        public virtual async Task Hide()
        {
            await ModalManager.Hide(JS, ModalId);
        }
    }
}
