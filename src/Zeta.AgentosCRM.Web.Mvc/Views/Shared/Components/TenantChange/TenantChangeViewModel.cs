using Abp.AutoMapper;
using Zeta.AgentosCRM.Sessions.Dto;

namespace Zeta.AgentosCRM.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}