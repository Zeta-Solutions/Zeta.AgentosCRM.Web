using Abp.AutoMapper;
using Zeta.AgentosCRM.MultiTenancy;
using Zeta.AgentosCRM.MultiTenancy.Dto;
using Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Common;

namespace Zeta.AgentosCRM.Web.Areas.AppAreaName.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}