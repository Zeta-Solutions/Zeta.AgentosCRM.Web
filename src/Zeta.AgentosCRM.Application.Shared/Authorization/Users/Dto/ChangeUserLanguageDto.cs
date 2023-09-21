using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
