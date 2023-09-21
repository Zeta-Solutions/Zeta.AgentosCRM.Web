using Abp.Auditing;
using Zeta.AgentosCRM.Configuration.Dto;

namespace Zeta.AgentosCRM.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}