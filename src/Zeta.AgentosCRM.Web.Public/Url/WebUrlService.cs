using Abp.Dependency;
using Zeta.AgentosCRM.Configuration;
using Zeta.AgentosCRM.Url;
using Zeta.AgentosCRM.Web.Url;

namespace Zeta.AgentosCRM.Web.Public.Url
{
    public class WebUrlService : WebUrlServiceBase, IWebUrlService, ITransientDependency
    {
        public WebUrlService(
            IAppConfigurationAccessor appConfigurationAccessor) :
            base(appConfigurationAccessor)
        {
        }

        public override string WebSiteRootAddressFormatKey => "App:WebSiteRootAddress";

        public override string ServerRootAddressFormatKey => "App:AdminWebSiteRootAddress";
    }
}