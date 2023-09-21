using Abp.AutoMapper;
using Zeta.AgentosCRM.MultiTenancy.Dto;

namespace Zeta.AgentosCRM.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
