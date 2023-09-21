using Abp.AspNetCore.Mvc.ViewComponents;

namespace Zeta.AgentosCRM.Web.Public.Views
{
    public abstract class AgentosCRMViewComponent : AbpViewComponent
    {
        protected AgentosCRMViewComponent()
        {
            LocalizationSourceName = AgentosCRMConsts.LocalizationSourceName;
        }
    }
}