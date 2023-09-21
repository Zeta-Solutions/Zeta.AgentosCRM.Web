using System.ComponentModel.DataAnnotations;

namespace Zeta.AgentosCRM.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}