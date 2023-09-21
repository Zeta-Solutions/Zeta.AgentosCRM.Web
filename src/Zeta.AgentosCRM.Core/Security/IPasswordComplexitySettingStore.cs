using System.Threading.Tasks;

namespace Zeta.AgentosCRM.Security
{
    public interface IPasswordComplexitySettingStore
    {
        Task<PasswordComplexitySetting> GetSettingsAsync();
    }
}
