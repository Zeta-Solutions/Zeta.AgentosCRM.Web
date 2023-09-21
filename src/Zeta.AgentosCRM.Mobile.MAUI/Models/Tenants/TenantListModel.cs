using Abp.AutoMapper;
using Zeta.AgentosCRM.MultiTenancy.Dto;

namespace Zeta.AgentosCRM.Mobile.MAUI.Models.Tenants
{
    [AutoMapFrom(typeof(TenantListDto))]
    [AutoMapTo(typeof(TenantEditDto), typeof(CreateTenantInput))]
    public class TenantListModel : TenantListDto
    {
 
    }
}
