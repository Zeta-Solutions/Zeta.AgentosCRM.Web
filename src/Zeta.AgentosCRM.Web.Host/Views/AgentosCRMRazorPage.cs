using Abp.AspNetCore.Mvc.Views;

namespace Zeta.AgentosCRM.Web.Views
{
    public abstract class AgentosCRMRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected AgentosCRMRazorPage()
        {
            LocalizationSourceName = AgentosCRMConsts.LocalizationSourceName;
        }
    }
}
