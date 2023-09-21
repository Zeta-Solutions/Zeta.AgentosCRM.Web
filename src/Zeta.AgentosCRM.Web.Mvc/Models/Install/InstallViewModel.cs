using System.Collections.Generic;
using Abp.Localization;
using Zeta.AgentosCRM.Install.Dto;

namespace Zeta.AgentosCRM.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
