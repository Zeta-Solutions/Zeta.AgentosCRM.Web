using Abp.AutoMapper;
using Zeta.AgentosCRM.MultiTenancy.Dto;

namespace Zeta.AgentosCRM.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(RegisterTenantOutput))]
    public class TenantRegisterResultViewModel : RegisterTenantOutput
    {
        public string TenantLoginAddress { get; set; }
    }
}