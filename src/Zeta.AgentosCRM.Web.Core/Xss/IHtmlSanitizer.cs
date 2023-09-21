using Abp.Dependency;

namespace Zeta.AgentosCRM.Web.Xss
{
    public interface IHtmlSanitizer: ITransientDependency
    {
        string Sanitize(string html);
    }
}