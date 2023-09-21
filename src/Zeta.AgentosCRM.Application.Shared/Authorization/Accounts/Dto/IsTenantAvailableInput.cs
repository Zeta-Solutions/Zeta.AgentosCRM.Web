using System.ComponentModel.DataAnnotations;
using Abp.MultiTenancy;

namespace Zeta.AgentosCRM.Authorization.Accounts.Dto
{
    public class IsTenantAvailableInput
    {
        [Required]
        [MaxLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }
    }
}