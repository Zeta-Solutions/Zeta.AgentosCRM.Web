using System.Threading.Tasks;

namespace Zeta.AgentosCRM.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}