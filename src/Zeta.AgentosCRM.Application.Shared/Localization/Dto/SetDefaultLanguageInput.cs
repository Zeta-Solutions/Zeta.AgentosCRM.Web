using System.ComponentModel.DataAnnotations;
using Abp.Localization;

namespace Zeta.AgentosCRM.Localization.Dto
{
    public class SetDefaultLanguageInput
    {
        [Required]
        [StringLength(ApplicationLanguage.MaxNameLength)]
        public virtual string Name { get; set; }
    }
}