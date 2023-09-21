using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Zeta.AgentosCRM.Web.Public.Views
{
    public abstract class AgentosCRMRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected AgentosCRMRazorPage()
        {
            LocalizationSourceName = AgentosCRMConsts.LocalizationSourceName;
        }
    }
}
