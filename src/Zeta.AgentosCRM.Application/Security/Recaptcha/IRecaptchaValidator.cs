using System.Threading.Tasks;

namespace Zeta.AgentosCRM.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}