using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Zeta.AgentosCRM.Localization
{
    public static class AgentosCRMLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    AgentosCRMConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AgentosCRMLocalizationConfigurer).GetAssembly(),
                        "Zeta.AgentosCRM.Localization.AgentosCRM"
                    )
                )
            );
        }
    }
}