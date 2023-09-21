using Microsoft.AspNetCore.Components;
using Zeta.AgentosCRM.Authorization.Accounts;
using Zeta.AgentosCRM.Authorization.Accounts.Dto;
using Zeta.AgentosCRM.Core.Dependency;
using Zeta.AgentosCRM.Core.Threading;
using Zeta.AgentosCRM.Mobile.MAUI.Models.Login;
using Zeta.AgentosCRM.Mobile.MAUI.Shared;

namespace Zeta.AgentosCRM.Mobile.MAUI.Pages.Login
{
    public partial class ForgotPasswordModal : ModalBase
    {
        public override string ModalId => "forgot-password-modal";
       
        [Parameter] public EventCallback OnSave { get; set; }
        
        public ForgotPasswordModel forgotPasswordModel { get; set; } = new ForgotPasswordModel();

        private readonly IAccountAppService _accountAppService;

        public ForgotPasswordModal()
        {
            _accountAppService = DependencyResolver.Resolve<IAccountAppService>();
        }

        protected virtual async Task Save()
        {
            await SetBusyAsync(async () =>
            {
                await WebRequestExecuter.Execute(
                async () =>
                    await _accountAppService.SendPasswordResetCode(new SendPasswordResetCodeInput { EmailAddress = forgotPasswordModel.EmailAddress }),
                    async () =>
                    {
                        await OnSave.InvokeAsync();
                    }
                );
            });
        }

        protected virtual async Task Cancel()
        {
            await Hide();
        }
    }
}
