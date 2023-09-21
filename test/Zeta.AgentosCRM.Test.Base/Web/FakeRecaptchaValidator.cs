using System.Threading.Tasks;
using Zeta.AgentosCRM.Security.Recaptcha;

namespace Zeta.AgentosCRM.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
