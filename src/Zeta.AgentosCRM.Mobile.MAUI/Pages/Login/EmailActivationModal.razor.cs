using Microsoft.AspNetCore.Components;
using Zeta.AgentosCRM.Authorization.Accounts;
using Zeta.AgentosCRM.Authorization.Accounts.Dto;
using Zeta.AgentosCRM.Core.Dependency;
using Zeta.AgentosCRM.Core.Threading;
using Zeta.AgentosCRM.Mobile.MAUI.Models.Login;
using Zeta.AgentosCRM.Mobile.MAUI.Shared;

namespace Zeta.AgentosCRM.Mobile.MAUI.Pages.Login
{
    public partial class EmailActivationModal : ModalBase
    {
        public override string ModalId => "email-activation-modal";

        [Parameter] public EventCallback OnSave { get; set; }

        public EmailActivationModel emailActivationModel { get; set; } = new EmailActivationModel();

        private readonly IAccountAppService _accountAppService;

        public EmailActivationModal()
        {
            _accountAppService = DependencyResolver.Resolve<IAccountAppService>();
        }

        protected virtual async Task Save()
        {
            await SetBusyAsync(async () =>
            {
                await WebRequestExecuter.Execute(
                async () =>
                    await _accountAppService.SendEmailActivationLink(new SendEmailActivationLinkInput
                    {
                        EmailAddress = emailActivationModel.EmailAddress
                    }),
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
