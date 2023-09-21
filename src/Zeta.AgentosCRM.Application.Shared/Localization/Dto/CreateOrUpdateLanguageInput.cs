using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}