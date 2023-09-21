using System.Globalization;

namespace Zeta.AgentosCRM.Localization
{
    public interface IApplicationCulturesProvider
    {
        CultureInfo[] GetAllCultures();
    }
}