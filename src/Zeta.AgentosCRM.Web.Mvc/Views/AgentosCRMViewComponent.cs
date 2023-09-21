using Abp.AspNetCore.Mvc.ViewComponents;

namespace Zeta.AgentosCRM.Web.Views
{
    public abstract class AgentosCRMViewComponent : AbpViewComponent
    {
        protected AgentosCRMViewComponent()
        {
            LocalizationSourceName = AgentosCRMConsts.LocalizationSourceName;
        }
    }
}